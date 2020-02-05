// Jack

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
        // Input
        private const string jumpAxis = "Jump";
        private const string horizontalAxis = "Horizontal";
        private const string verticalAxis = "Vertical";
        private const string lightCandleButton = "Light Candle";
        private const string interactButton = "Interact";

        // Movement
        public Rigidbody rigidBody;
        private bool usingController = true;
        InputDevice inputDevice = InputManager.ActiveDevice; // InControl input

        // Health
        public bool dead = false;

        // Inventory
        private ushort[] inventory = new ushort[(ushort)ItemType.sizeOf];

        // Candle
        public GameObject candleFlame;

        // Handling objects
        private const string interactableTag = "Interactable Object";
        private const ushort interactDistance = 10;
        private GameObject heldObject = null;

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

            // My Setup
            if(!rigidBody )
            {
                rigidBody = GetComponent<Rigidbody>();
            }

            inventory[(ushort)ItemType.matches] = 3;
        }


        // Update is called once per frame
        private void Update()
        {
            if (!dead)
            {
                // the jump state needs to read here to make sure it is not missed
                if (!m_Jump)
                {
                    m_Jump = CrossPlatformInputManager.GetButtonDown(jumpAxis);
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
            float horizontal;
            float vertical;

            // InControl input
            inputDevice = InputManager.ActiveDevice;
            if(InputManager.Devices.Count > 0)
            {
                usingController = true;

                horizontal = inputDevice.LeftStick.X;
                vertical = inputDevice.LeftStick.Y;

                if(inputDevice.RightTrigger)
                {
                    LightCandle();
                }

                if(inputDevice.Action1)
                {

                }
            }
            else
            {
                usingController = false;

                horizontal = CrossPlatformInputManager.GetAxis(horizontalAxis);
                vertical = CrossPlatformInputManager.GetAxis(verticalAxis);

                if (Input.GetButtonDown(lightCandleButton))
                {
                    LightCandle();
                }

                if(Input.GetButtonDown(interactButton))
                {
                    if (heldObject)
                    {
                        // Drop held object
                    }
                    else
                    {
                        Physics.Raycast(m_Camera.transform.position, transform.forward, out RaycastHit hit, interactDistance);
                        if (hit.transform.CompareTag(interactableTag))
                        {
                            heldObject = hit.transform.gameObject;
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

        // ============== My Functions ============== //

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
    }
}
