//Louie Williamson - 02/03/2020
// Jack - 11/03/2020 Reviewed. Added placeholder header comments, changed the magic numbers to a constant and cached the strings.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Description of the class here.
/// </summary>
public class AudioSettings_Louie : MonoBehaviour
{
    public AudioMixer mixer;
    private float volumeLevel;
    private const float volumeMultiplier = 20;
    private const string ambientChannel = "AmbientVolume";
    private const string masterChannel = "MasterVolume";
    private const string sfxChannel = "SFXVolume";

    /// <summary>
    /// Description of the function here.
    /// </summary>
    /// <param name="sliderVal"></param>
    public void SetAmbientVolume(float sliderVal)
    {
        volumeLevel = Mathf.Log10(sliderVal) * volumeMultiplier;
        mixer.SetFloat(ambientChannel, volumeLevel);
    }
    public void SetMasterVolume(float sliderVal)
    {
        volumeLevel = Mathf.Log10(sliderVal) * volumeMultiplier;
        mixer.SetFloat(masterChannel, volumeLevel);
    }
    public void SetSFXVolume(float sliderVal)
    {
        volumeLevel = Mathf.Log10(sliderVal) * volumeMultiplier;
        mixer.SetFloat(sfxChannel, volumeLevel);
    }
    
}
