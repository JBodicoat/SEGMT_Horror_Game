//Louie : 25/03/2020 - Created Script
// Jack : 30/03/2020 - Reviewed script. Optimized distance calculation.
// Jack : 05/04/2020 - Added saving & loading support.
//                     Created IsSafeOpen to be used by LevelDataManager
//                     Moved intitialisation out of Start
//                     Set OpenSafe to public to be used by LevelDataManager

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Safe_LouieWilliamson : MonoBehaviour
{
    //References
    private Animator safeAnim;
    public Camera dialCam;
    private SFXManager_LW soundManager;
    private FirstPersonController_Jack playerScript;
    private Transform playerTransform;
    public Transform dial;
    public GameObject tip;

    //Script Variables
    private bool isAttemptingSafe = false;
    private bool safeOpen = false;
    private Vector3 rotateRight = new Vector3(0, 0, 2.5f);
    private float dialTimer = 0;
    private int dialStage = 1;

    //constants
    private const float clickTime = 1.0f;
    private const int dialDifference = 7;
    private const float maxDistance = 4.0f;
    private const float maxSqrDistance = maxDistance * maxDistance;
    
    //1st Code is 15
    private const float DialCode1 = 235.0f;
    //2nd Code is 80
    private const float DialCode2 = 109.0f;
    //3rd Code is 45
    private const float DialCode3 = 342.0f;

    private void Awake()
    {
        safeAnim = GetComponent<Animator>();
        playerScript = FindObjectOfType<FirstPersonController_Jack>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        soundManager = FindObjectOfType<SFXManager_LW>();
    }

    // Update is called once per frame
    void Update()
    {
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
    /// <summary>
    /// Checks the distance between the player and the safe.
    /// If the player is too far, the puzzle UI will disable.
    /// </summary>
    private void CheckDistance()
    {
        float xDifference = playerTransform.position.x - transform.position.x;
        float zDifference = playerTransform.position.z - transform.position.z;
        float sqrDistance = xDifference * xDifference + zDifference * zDifference;
        if (sqrDistance > maxSqrDistance)
        {
            CameraOff();
        }
    }
    /// <summary>
    /// Plays the open safe animation.
    /// </summary>
    public void OpenSafe()
    {
        CameraOff();
        safeOpen = true;
        safeAnim.SetBool("Unlocked", true);
    }

    /// <summary>
    /// Turns the dial camera on
    /// </summary>
    public void CameraOn()
    {
        if (!safeOpen)
        {
            playerScript.SetUsingSafe(true);
            dialCam.enabled = true;
            isAttemptingSafe = dialCam.enabled;
            tip.SetActive(true);
        }
    }

    /// <summary>
    /// Turns the dial camera off
    /// </summary>
    private void CameraOff()
    {
        playerScript.SetUsingSafe(false);
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
                        ++dialStage;
                        soundManager.PlaySFX(SFXManager_LW.SFX.Click);
                    }
                    break;
                case 2:
                    if (dial.rotation.eulerAngles.z <= DialCode2 + dialDifference && dial.rotation.eulerAngles.z >= DialCode2 - dialDifference)
                    {
                        ++dialStage;
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

    /// <summary>
    /// Returns safeOpen.
    /// </summary>
    /// <returns></returns>
    public bool IsSafeOpen()
    {
        return safeOpen;
    }
}
