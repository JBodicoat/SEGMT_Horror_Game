//Louie Williamson - 02/03/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSettings_Louie : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetAmbientVolume(float sliderVal)
    {
        mixer.SetFloat("AmbientVolume", Mathf.Log10(sliderVal)*20);
    }
    public void SetMasterVolume(float sliderVal)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderVal) * 20);
    }
    public void SetSFXVolume(float sliderVal)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sliderVal) * 20);
    }
}
