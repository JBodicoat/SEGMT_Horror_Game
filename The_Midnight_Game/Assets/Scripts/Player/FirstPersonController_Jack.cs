// Jack
// Jack : 05/02/2020 ~ 15:30 Implemented picking up objects
//                   ~ 18:30 Finished implementing interaction with tablets for puzzle 1
// Jack : 06/02/2020 Changed holding objects to use collisions
//                   Implemented the salt circle
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using InControl;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    [System.Serializable]
    /// Controls the player character.
    /// 
    /// Controls players movement, inventory system & candle.
    public class FirstPersonController_Jack : MonoBehaviour
    {
        // Unity's variables
        [SerializeField] private float m_WalkSpeed = 0;
        [SerializeField] private float m_JumpSpeed = 0;
        [SerializeField] private float m_StickToGroundForce = 0;
        [SerializeField] private float m_GravityMultiplier = 0;
        [SerializeField] private MouseLook_Jack m_MouseLook = null;
        [SerializeField] private bool m_UseHeadBob = false;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval = 0;
        [SerializeField] private AudioClip[] m_FootstepSounds = null;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound = null;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound = null;           // the sound played when character touches back on ground.

        private Camera m_Camera;
        private bool m_Jump;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;

        // ===== My variables ===== //
        // Saving game data
        public FirstPersonControllerSaveData_Jack saveData = new FirstPersonControllerSaveData_Jack();

        // Input
        private const string jumpAxis = "Jump";
        private const string horizontalAxis = "Horizontal";
        private const string verticalAxis = "Vertical";
        private const string lightCandleButton = "Light Candle";
        private const string interactButton = "Interact";
        private const string pickupButton = "Pickup";
        private const string saltButton = "Salt";

        // Movement
        public Rigidbody rigidBody;
        private bool usingController = true;
        private InputDevice inputDevice = InputManager.ActiveDevice; // InControl input

        // Health
        public bool dead = false;

        // Inventory
        private ushort[] inventory = new ushort[(ushort)ItemType.sizeOf];

        // Candle
        public GameObject candleFlame;

        // Salt circle
        public GameObject saltCirclePrefab;
        //private readonly Quaternion identityQuaternion = new Quaternion(1, 1, 1, 1);
        private float halfPlayerHeight;
        public bool inSaltCircle = false;
        private bool pouringSalt = false;

        // Picking up objects
        private readonly LayerMask moveableObjectsLayer = 1 << 8;
        private const ushort interactDistance = 10;
        private GameObject heldObject = null;
        private Collider heldObjectCollider = null;
        private Rigidbody heldObjectRigidbody = null;
        private const RigidbodyConstraints freezeRotationConstraints = RigidbodyConstraints.FreezeRotation;
        private RigidbodyConstraints heldObjectConstraints = RigidbodyConstraints.None;
        private const float dropObjectDist = 20f;
        private const float objectMoveDeadZone = 0.3f;
        private const float objectMoveSpeed = 8f;
        private readonly Vector3 zeroVector3 = new Vector3(0, 0, 0);

        // Interactable objects
        private readonly LayerMask interactableObjectsLayer = 1 << 9;

        // Animation
        public Animator animator;

        // Use this for initialization
        private void Start()
        {
            // Unity Setup
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);

            // ===== My Setup ===== //
            // Save data
            saveData.xPos = transform.position.x;
            saveData.yPos = transform.position.y;
            saveData.zPos = transform.position.z;
            saveData.inventory = inventory;
            saveData.inSaltCirlce = inSaltCircle;

            // Movement
            if(!rigidBody )
            {
                rigidBody = GetComponent<Rigidbody>();
            }
            // Inventory
            inventory[(ushort)ItemType.matches] = 3;
            inventory[(ushort)ItemType.salt] = 2;

            // Salt circle
            halfPlayerHeight = m_CharacterController.height / 2f;
            if(!animator)
            {
                animator = GetComponent<Animator>();
            }            
        }


        // Update is called once per frame
        private void Update() 
        {
            if (!dead)
            {
                // the jump state needs to read here to make sure it is not missed
                if (!m_Jump)
                {
                    if(usingController)
                    {
                        m_Jump = inputDevice.Action1.WasPressed;
                    }
                    else
                    {
                        m_Jump = CrossPlatformInputManager.GetButtonDown(jumpAxis);
                    }
                }

                if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
                {
                    StartCoroutine(m_JumpBob.DoBobCycle());
                    PlayLandingSound();
                    m_MoveDir.y = 0f;
                    m_Jumping = false;
                }
                if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
                {
                    m_MoveDir.y = 0f;
                }

                m_PreviouslyGrounded = m_CharacterController.isGrounded;
            }
        }


        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
            if (!dead)
            {
                GetInput();

                RotateView();

                // always move along the camera forward as it is the direction that it being aimed at
                Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

                // get a normal for the surface that is being touched to move along it
                Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out RaycastHit hitInfo,
                                   m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
                desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

                m_MoveDir.x = desiredMove.x * m_WalkSpeed;
                m_MoveDir.z = desiredMove.z * m_WalkSpeed;


                if (m_CharacterController.isGrounded)
                {
                    m_MoveDir.y = -m_StickToGroundForce;

                    if (m_Jump)
                    {
                        m_MoveDir.y = m_JumpSpeed;
                        PlayJumpSound();
                        m_Jump = false;
                        m_Jumping = true;
                    }
                }
                else
                {
                    m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
                }
                m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

                ProgressStepCycle();
                UpdateCameraPosition();

                m_MouseLook.UpdateCursorLock();

                // Held object
                if (heldObject)
                {
                    Vector3 targetPosition = m_Camera.transform.position + m_Camera.transform.forward * 2f;
                    Vector3 distanceToTarget = targetPosition - heldObject.transform.position;
                    if(distanceToTarget.sqrMagnitude > dropObjectDist)
                    {
                        DropObject();
                    }
                    else if(distanceToTarget.sqrMagnitude > objectMoveDeadZone)
                    {
                        distanceToTarget.Normalize();
                        heldObjectRigidbody.velocity = distanceToTarget * objectMoveSpeed;
                    }
                    else
                    {
                        heldObjectRigidbody.velocity = zeroVector3;
                    }
                }

            }
        }

        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }

        private void ProgressStepCycle()
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + m_WalkSpeed) *
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }

        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }

        private void UpdateCameraPosition()
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      m_WalkSpeed);
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }

        private void GetInput()
        {
            // Read input
            float horizontal = 0f;
            float vertical = 0f;

            // InControl input
            inputDevice = InputManager.ActiveDevice;
            if(InputManager.Devices.Count > 0)
            {
                // Game pad inputs
                usingController = true;

                if(inputDevice.Action3.WasPressed)
                {
                    PourSaltCirlce();
                }

                if (!pouringSalt)
                {
                    horizontal = inputDevice.LeftStick.X;
                    vertical = inputDevice.LeftStick.Y;

                    if (inputDevice.RightTrigger)
                    {
                        LightCandle();
                    }

                    if (inputDevice.Action2.WasPressed)
                    {
                        Interact();
                    }

                    if (inputDevice.Action3.WasPressed)
                    {
                        if (heldObject)
                        {
                            DropObject();
                        }
                        else
                        {
                            PickupObject();
                        }
                    }
                }
            }
            else
            {
                // Keyboard & mouse inputs
                usingController = false;

                if (Input.GetButtonDown(saltButton))
                {
                    PourSaltCirlce();
                }

                if (!pouringSalt)
                {
                    horizontal = CrossPlatformInputManager.GetAxis(horizontalAxis);
                    vertical = CrossPlatformInputManager.GetAxis(verticalAxis);

                    if (Input.GetButtonDown(lightCandleButton))
                    {
                        LightCandle();
                    }

                    if (Input.GetButtonDown(interactButton))
                    {
                        Interact();
                    }

                    if (Input.GetButtonDown(pickupButton))
                    {
                        if (heldObject)
                        {
                            DropObject();
                        }
                        else
                        {
                            PickupObject();
                        }
                    }
                }
            }

            m_Input = new Vector2(horizontal, vertical);
            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }
        }

        /// Interacts with anything in front of the player.
        /// 
        /// Fires a ray forward from the camera. If it hits an interactable object
        /// it will be activated.
        private void Interact()
        {
            if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out RaycastHit hit, interactDistance, interactableObjectsLayer))
            {
                if (hit.transform.CompareTag("Tablet Slot"))
                {
                    hit.transform.gameObject.GetComponent<TabletSlot_Jack>().RotateTablet();
                }
            }
        }

        /// Picks up an object in front of the player.
        /// 
        /// Fires a ray from the camera in front of the player. If the ray hits an
        /// object that can be held it will be picked up.
        private void PickupObject()
        {
            Debug.DrawRay(m_Camera.transform.position, m_Camera.transform.forward * interactDistance, Color.red, 2f);
            if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out RaycastHit hit, interactDistance, moveableObjectsLayer))
            {
                heldObject = hit.transform.gameObject;

                heldObjectCollider = heldObject.GetComponent<Collider>();
                Physics.IgnoreCollision(heldObjectCollider, m_CharacterController);

                heldObjectRigidbody = heldObject.GetComponent<Rigidbody>();
                heldObjectRigidbody.useGravity = false;
                heldObjectConstraints = heldObjectRigidbody.constraints;
                heldObjectRigidbody.constraints = freezeRotationConstraints;
            }
        }

        /// Drops the held object if there is one.
        public void DropObject()
        {
            heldObjectRigidbody.constraints = heldObjectConstraints;
            heldObjectRigidbody.useGravity = true;
            heldObjectRigidbody = null;

            Physics.IgnoreCollision(heldObjectCollider, m_CharacterController, false);
            heldObjectCollider = null;

            heldObject = null;
        }

        /// Returns heldObject.
        public GameObject GetHeldObject()
        {
            return heldObject;
        }

        private void RotateView()
        {
            if(usingController)
            {
                m_MouseLook.LookRotation(transform, m_Camera.transform, inputDevice);
            }
            else
            {
                m_MouseLook.LookRotation(transform, m_Camera.transform);
            }
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }

        /// Returns usingController.
        public bool IsUsingController()
        {
            return usingController;
        }

        /// Adds items to the player's inventory.
        /// 
        /// Increases the number of passed itemType by passed quantity.
        /// <param name="itemType"></param>
        /// <param name="quantity"></param>
        public void AddItems(ItemType itemType, ushort quantity)
        {
            inventory[(ushort)itemType] += quantity;
        }

        /// Removes items to the player's inventory.
        /// 
        /// Decreases the number of passed itemType by passed quantity.
        /// <param name="itemType"></param>
        /// <param name="quantity"></param>
        public void RemoveItems(ItemType itemType, ushort quantity)
        {
            inventory[(ushort)itemType] -= quantity;
        }

        /// Attempts to pour a salt circle.
        /// 
        /// If the player has any salt it will be used to start pouring
        /// a salt circle.
        /// <returns>Returns false if the player has no salt.</returns>
        private bool PourSaltCirlce()
        {
            if(inventory[(ushort)ItemType.salt] > 0)
            {
                RemoveItems(ItemType.salt, 1);
                pouringSalt = true;
                animator.enabled = true;

                return true;
            }

            return false;
        }

        /// Creates a salt circle.
        /// 
        /// Creates a salt circle at the players feet. 
        /// This is called at the end of the salt pouring animation.
        public void InstantiateSaltCircle()
        {
            Instantiate(saltCirclePrefab,
                        new Vector3(transform.position.x, transform.position.y - halfPlayerHeight, transform.position.z),
                        Quaternion.identity);
            inSaltCircle = true;
            pouringSalt = false;
            animator.enabled = false;
        }

        /// Sets inSaltCircle to the passed value.
        /// <param name="isInSaltCircle"></param>
        public void SetInSaltCircle(bool isInSaltCircle)
        {
            inSaltCircle = isInSaltCircle;
        }

        /// Returns inSaltCircle.
        public bool IsInSaltCircle()
        {
            return inSaltCircle;
        }

        /// Attempts to light the player's candle.
        /// <returns>Returns false if the candle was already lit or the player doesn't have enouch matches.</returns>
        private bool LightCandle()
        {
            if(!candleFlame.activeSelf && inventory[(ushort)ItemType.matches] > 0)
            {
                candleFlame.SetActive(true);
                RemoveItems(ItemType.matches, 1);
            }

            return false;
        }

        /// Disables the player's candles light source.
        /// <returns>Returns false if the candle was already extinguished.</returns>
        public bool ExtinguishCandle()
        {
            if(candleFlame.activeSelf)
            {
                candleFlame.SetActive(false);
                return true;
            }

            return false;
        }

        /// Returns whether the candle is lit.
        public bool IsCandleLit()
        {
            return candleFlame.activeSelf;
        }


        // ===== Saving game data ===== //
        public void LoadSaveData(FirstPersonControllerSaveData_Jack loadData)
        {
            transform.position = new Vector3(loadData.xPos, loadData.yPos, loadData.zPos);
            inventory = loadData.inventory;
            inSaltCircle = loadData.inSaltCirlce;
        }

        public FirstPersonControllerSaveData_Jack GetSaveData()
        {
            saveData.xPos = transform.position.x;
            saveData.yPos = transform.position.y;
            saveData.zPos = transform.position.z;
            saveData.inventory = inventory;
            saveData.inSaltCirlce = inSaltCircle;

            return saveData;
        }
    }
}
