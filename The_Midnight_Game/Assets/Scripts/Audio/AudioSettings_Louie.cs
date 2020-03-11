//Louie Williamson - 02/03/2020

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

    /// <summary>
    /// Description of the function here.
    /// </summary>
    /// <param name="sliderVal"></param>
    public void SetAmbientVolume(float sliderVal)
    {
        volumeLevel = Mathf.Log10(sliderVal) * volumeMultiplier;
        mixer.SetFloat("AmbientVolume", volumeLevel);
    }
    public void SetMasterVolume(float sliderVal)
    {
        volumeLevel = Mathf.Log10(sliderVal) * volumeMultiplier;
        mixer.SetFloat("MasterVolume", volumeLevel);
    }
    public void SetSFXVolume(float sliderVal)
    {
        volumeLevel = Mathf.Log10(sliderVal) * volumeMultiplier;
        mixer.SetFloat("SFXVolume", volumeLevel);
    }
    
}
