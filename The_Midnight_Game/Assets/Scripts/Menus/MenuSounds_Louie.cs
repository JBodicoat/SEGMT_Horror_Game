using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the audio of the while the menu is paused
/// </summary>
public class MenuSounds_Louie : MonoBehaviour
{
    // Start is called before the first frame update
    private SFXManager_LW soundManager;

    void Start()
    {
        soundManager = gameObject.GetComponent<SFXManager_LW>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonClick()
    {
        soundManager.PlayMenuSFX(SFXManager_LW.SFX.UIClick);
    }
    public void PlaySaving()
    {
        soundManager.PlayMenuSFX(SFXManager_LW.SFX.Saving);
    }
    public void PlayOptionsApply()
    {
        soundManager.PlayMenuSFX(SFXManager_LW.SFX.OptionApply);
    }
}
