//Louie Williamson - 02/03/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings_Louie : MonoBehaviour
{
    public AudioMixer mixer;
    private float volumeLevel;

    public void SetAmbientVolume(float sliderVal)
    {
        volumeLevel = Mathf.Log10(sliderVal) * 20;
        mixer.SetFloat("AmbientVolume", volumeLevel);
    }
    public void SetMasterVolume(float sliderVal)
    {
        volumeLevel = Mathf.Log10(sliderVal) * 20;
        mixer.SetFloat("MasterVolume", volumeLevel);
    }
    public void SetSFXVolume(float sliderVal)
    {
        volumeLevel = Mathf.Log10(sliderVal) * 20;
        mixer.SetFloat("SFXVolume", volumeLevel);
    }
    
}
