// Louie
// Jack 05/02/2020 - optimized tag comparison
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound_Louie : MonoBehaviour
{
    public AudioSource audioSource;

    private const string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            PlaySound();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            PauseAudio();
        }
    }
    void PlaySound()
    {
        audioSource.Play();
    }
    void PauseAudio()
    {
        audioSource.Pause();
    }
}
