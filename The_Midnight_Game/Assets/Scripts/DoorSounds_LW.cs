//Louie 07/04/2020 - Created script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle's the door sounds, functions are used by events in the animations.
/// </summary>
public class DoorSounds_LW : MonoBehaviour
{
    // Start is called before the first frame update
    private SFXManager_LW soundManager;

    void Start()
    {
        soundManager = GameObject.Find("SFX_Manager").GetComponent<SFXManager_LW>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Called by the animation event in the door open animation and plays the door opening sound.
    /// </summary>
    public void OpenDoorSound()
    {
        soundManager.PlaySFX(SFXManager_LW.SFX.DoorOpen);
    }

    /// <summary>
    /// Called by the animation event in the door close animation and plays the door closing sound.
    /// </summary>
    public void CloseDoorSound()
    {
        soundManager.PlaySFX(SFXManager_LW.SFX.DoorClose);
    }

    /// <summary>
    /// Called by the animation event in the door close animation and plays the door shut sound.
    /// </summary>
    public void CloseDoorSound2()
    {
        soundManager.PlaySFX(SFXManager_LW.SFX.DoorShut);
    }
}
