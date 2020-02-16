// Jack
// Jack : 05/02/2020 ~ 15:30 Implemented picking up objects
//                   ~ 18:30 Finished implementing interaction with tablets for puzzle 1
// Jack : 06/02/2020 Changed holding objects to use collisions
//                   Implemented the salt circle
// Jack 13/02/2020 - Added saving of player's rotation & candle
// Jack 15/02/2020 - Added support for changing input bindings for controller & keyboard & mouse.
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using InControl;
using System.Collections.Generic;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    [System.Serializable]
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
        private const string horizontalAxis = "Horizontal";
        private const string verticalAxis = "Vertical";
        private const string interactButton = "Interact";

        private InputControlType jumpControlType = InputControlType.Action1;
        private InputControlType saltControlType = InputControlType.Action2;
        private InputControlType candleControlType = InputControlType.Action3;
        private InputControlType grabControlType = InputControlType.LeftTrigger;
        private InputControlType throwControlType = InputControlType.RightTrigger;

        private KeyCode jumpKey = KeyCode.Space;
        private KeyCode saltKey = KeyCode.Q;
        private KeyCode candleKey = KeyCode.F;
        private KeyCode interactKey = KeyCode.E;
        private KeyCode grabKey = KeyCode.Mouse0;
        private KeyCode throwKey = KeyCode.Mouse1;

        // Movement
        public Rigidbody rigidBody;
        private bool usingController = true;
        private InputDevice inputDevice = InputManager.ActiveDevice; // InControl input

        // Health
        public bool dead = false;

        private Inventory_Jack inventoryScript;
        private Candle_Jack candleScript;
        private SaltPouring_Jack saltPouringScript;
        private Interaction_Jack interactionScript;

        // Use this for initialization
        private void Awake()
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

            inventoryScript = FindObjectOfType<Inventory_Jack>();
            candleScript = FindObjectOfType<Candle_Jack>();
            saltPouringScript = FindObjectOfType<SaltPouring_Jack>();
            interactionScript = FindObjectOfType<Interaction_Jack>();

            // Save data
            saveData.xPos = transform.position.x;
            saveData.yPos = transform.position.y;
            saveData.zPos = transform.position.z;
            saveData.inventory = inventoryScript.GetInventory();
            saveData.inSaltCirlce = saltPouringScript.IsInSaltCircle();

            // Movement
            if(!rigidBody )
            {
                rigidBody = GetComponent<Rigidbody>();
            }
        }


        // Update is called once per frame
        private void Update() 
        {
            if (!dead)
            {
                GetInput();

                // the jump state needs to read here to make sure it is not missed
                if (!m_Jump)
                {
                    if(usingController)
                    {
                        m_Jump = inputDevice.GetControl(jumpControlType).WasPressed;
                    }
                    else
                    {
                        m_Jump = Input.GetKeyDown(jumpKey);
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
            float horizontal = 0f;
            float vertical = 0f;

            // InControl input
            inputDevice = InputManager.ActiveDevice;
            if(InputManager.Devices.Count > 0)
            {
                // Game pad inputs
                usingController = true;

                if(inputDevice.GetControl(saltControlType).WasPressed)
                {
                    saltPouringScript.PourSaltCirlce();
                }

                if (!saltPouringScript.IsPouringSalt())
                {
                    horizontal = inputDevice.LeftStick.X;
                    vertical = inputDevice.LeftStick.Y;

                    if (inputDevice.GetControl(candleControlType).WasPressed)
                    {
                        candleScript.LightCandle();
                    }

                    if (inputDevice.Action2.WasPressed)
                    {
                        interactionScript.Interact();
                    }

                    if(inputDevice.GetControl(throwControlType).WasPressed && interactionScript.GetHeldObject())
                    {
                        interactionScript.ThrowObject();
                    }
                    else if (inputDevice.GetControl(grabControlType).WasPressed)
                    {
                        if (interactionScript.GetHeldObject())
                        {
                            interactionScript.DropObject();
                        }
                        else
                        {
                            interactionScript.PickupObject();
                        }
                    }
                }
            }
            else
            {
                // Keyboard & mouse inputs
                usingController = false;

                if (Input.GetKeyDown(saltKey))
                {
                    saltPouringScript.PourSaltCirlce();
                }

                if (!saltPouringScript.IsPouringSalt())
                {
                    horizontal = CrossPlatformInputManager.GetAxis(horizontalAxis);
                    vertical = CrossPlatformInputManager.GetAxis(verticalAxis);

                    if (Input.GetKeyDown(candleKey))
                    {
                        candleScript.LightCandle();
                    }

                    if (Input.GetKeyDown(interactKey))
                    {
                        interactionScript.Interact();
                    }

                    if(Input.GetKeyDown(throwKey) && interactionScript.GetHeldObject())
                    {
                        interactionScript.ThrowObject();
                    }
                    else if (Input.GetKeyDown(grabKey))
                    {
                        if (interactionScript.GetHeldObject())
                        {
                            interactionScript.DropObject();
                        }
                        else
                        {
                            interactionScript.PickupObject();
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

        /// Returns usingController.
        public bool IsUsingController()
        {
            return usingController;
        }

        // ===== Saving game data ===== //
        /// Loads the currently saved player data.
        public void LoadSaveData(FirstPersonControllerSaveData_Jack loadData)
        {
            transform.position = new Vector3(loadData.xPos, loadData.yPos, loadData.zPos);
            transform.localRotation = Quaternion.Euler(loadData.xRot, loadData.yRot, loadData.zRot);
            m_Camera.transform.localRotation = Quaternion.Euler(loadData.cameraXRot, loadData.cameraYRot, loadData.cameraZRot);
            m_MouseLook.Init(transform, m_Camera.transform);
            inventoryScript.SetInventory(loadData.inventory);
            saltPouringScript.SetInSaltCircle(loadData.inSaltCirlce);
            candleScript.SetCandleLit(loadData.candleLit);
        }

        /// Returns the data of the player that needs saving.
        public FirstPersonControllerSaveData_Jack GetSaveData()
        {
            saveData.xPos = transform.position.x;
            saveData.yPos = transform.position.y;
            saveData.zPos = transform.position.z;
            saveData.xRot = transform.eulerAngles.x;
            saveData.yRot = transform.eulerAngles.y;
            saveData.zRot = transform.eulerAngles.z;
            saveData.cameraXRot = m_Camera.transform.localEulerAngles.x;
            saveData.cameraYRot = m_Camera.transform.localEulerAngles.y;
            saveData.cameraZRot = m_Camera.transform.localEulerAngles.z;
            saveData.inventory = inventoryScript.GetInventory();
            saveData.inSaltCirlce = saltPouringScript.IsInSaltCircle();
            saveData.candleLit = candleScript.IsCandleLit();

            return saveData;
        }

        // ===== Input Settings ===== //
        /// Sets the jump InputControlType.
        public void SetJumpControlType(InputControlType newJumpControlType)
        {
            jumpControlType = newJumpControlType;
        }

        /// Sets the pour salt InputControlType.
        public void SetSaltControlType(InputControlType newSaltControlType)
        {
            saltControlType = newSaltControlType;
        }

        /// Sets the light candle InputControlType.
        public void SetCandleControlType(InputControlType newCandleControlType)
        {
            candleControlType = newCandleControlType;
        }

        /// Sets the grab/drop InputControlType.
        public void SetGrabControlType(InputControlType newGrabControlType)
        {
            grabControlType = newGrabControlType;
        }

        /// Sets the throw InputControlType.
        public void SetThrowControlType(InputControlType newThrowControlType)
        {
            throwControlType = newThrowControlType;
        }

        /// <summary>
        /// Sets the jump KeyCode.
        /// </summary>
        /// <param name="newJumpKey"></param>
        public void SetJumpKey(KeyCode newJumpKey)
        {
            jumpKey = newJumpKey;
        }

        /// <summary>
        /// Sets the salt KeyCode.
        /// </summary>
        /// <param name="newSaltKey"></param>
        public void SetSaltKey(KeyCode newSaltKey)
        {
            saltKey = newSaltKey;
        }

        /// <summary>
        /// Sets the candle KeyCode.
        /// </summary>
        /// <param name="newCandleKey"></param>
        public void SetCandleKey(KeyCode newCandleKey)
        {
            candleKey = newCandleKey;
        }

        /// <summary>
        /// Sets the grab/drop KeyCode.
        /// </summary>
        /// <param name="newGrabKey"></param>
        public void SetGrabKey(KeyCode newGrabKey)
        {
            grabKey = newGrabKey;
        }

        /// <summary>
        /// Sets the throw KeyCode.
        /// </summary>
        /// <param name="newThrowKey"></param>
        public void SetThrowKey(KeyCode newThrowKey)
        {
            throwKey = newThrowKey;
        }
    }
}
