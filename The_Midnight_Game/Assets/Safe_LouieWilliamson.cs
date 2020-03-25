using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe_LouieWilliamson : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator safeAnim;
    public Camera dialCam;
    private bool isAttemptingSafe;
    public Transform dial;
    private Vector3 rotateRight;
    public GameObject tip;
    //1st Code is 15
    private const float DialCode1 = 235.0f;
    //2nd Code is 80
    private const float DialCode2 = 109.0f;
    //3rd Code is 45
    private const float DialCode3 = 342.0f;

    private float dialTimer;
    private const float clickTime = 1.0f;
    private const int dialDifference = 7;
    private int dialStage;

    private SFXManager_LW soundManager;
    private Transform player;
    private const float maxDistance = 4.0f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        safeAnim = GetComponent<Animator>();
        isAttemptingSafe = false;
        rotateRight = new Vector3(0, 0, 2.5f);
        dialStage = 1;
        dialTimer = 0;
        soundManager = FindObjectOfType<SFXManager_LW>();
    }

    // Update is called once per frame
    void Update()
    {
        print(Vector3.Distance(player.position, gameObject.transform.position));
        if (isAttemptingSafe)
        {
            if (Input.GetKey(KeyCode.E))
            {
                RotateDial(true);
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                RotateDial(false);
            }
            dialTimer += Time.deltaTime;
            CheckDial();
            CheckDistance();
        }
    }
    private void CheckDistance()
    {
        if (Vector3.Distance(player.position, gameObject.transform.position) > maxDistance)
        {
            CameraOff();
        }
    }
    /// <summary>
    /// Plays the open safe animation.
    /// </summary>
    private void OpenSafe()
    {
        CameraOff();
        safeAnim.SetBool("Unlocked", true);
    }

    /// <summary>
    /// Turns the dial camera on
    /// </summary>
    public void CameraOn()
    {
        dialCam.enabled = true;
        isAttemptingSafe = dialCam.enabled;
        tip.SetActive(true);
    }

    /// <summary>
    /// Turns the dial camera on
    /// </summary>
    private void CameraOff()
    {
        dialCam.enabled = false;
        isAttemptingSafe = dialCam.enabled;
        tip.SetActive(false);
    }

    /// <summary>
    /// Rotates the dial. If bool is true it moves right, if not it moves left.
    /// </summary>
    /// <param name="moveRight"></param>
    private void RotateDial(bool moveRight)
    {
        if (moveRight)
        {
            dial.Rotate(rotateRight);
        }
        else
        {
            dial.Rotate(-rotateRight);

        }
        dialTimer = 0;
    }
    /// <summary>
    /// If the dial isn't moved for 1 second, its rotation is compared to the current dial code.
    /// If they are close enough, then it moves on to the next code and plays a click sound.
    /// If its the last dial, it opens the safe.
    /// </summary>
    private void CheckDial()
    {
        if (dialTimer >= clickTime)
        {
            switch (dialStage)
            {
                case 1:
                    if (dial.rotation.eulerAngles.z <= DialCode1 + dialDifference && dial.rotation.eulerAngles.z >= DialCode1 - dialDifference)
                    {
                        dialStage++;
                        soundManager.PlaySFX(SFXManager_LW.SFX.Click);
                    }
                    break;
                case 2:
                    if (dial.rotation.eulerAngles.z <= DialCode2 + dialDifference && dial.rotation.eulerAngles.z >= DialCode2 - dialDifference)
                    {
                        dialStage++;
                        soundManager.PlaySFX(SFXManager_LW.SFX.Click);
                    }
                    break;
                case 3:
                    if (dial.rotation.eulerAngles.z <= DialCode3 + dialDifference && dial.rotation.eulerAngles.z >= DialCode3 - dialDifference)
                    {
                        OpenSafe();
                        soundManager.PlaySFX(SFXManager_LW.SFX.Click);
                    }
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// Used by the animation event to play the wheel spin SFX
    /// </summary>
    public void PlayWheelSound()
    {
        soundManager.PlaySFX(SFXManager_LW.SFX.Wheelspin);
    }

    /// <summary>
    /// Used by the animation event to play the door opening SFX
    /// </summary>
    public void PlayDoorOpenSound()
    {
        soundManager.PlaySFX(SFXManager_LW.SFX.SafeDoor);
    }
}
