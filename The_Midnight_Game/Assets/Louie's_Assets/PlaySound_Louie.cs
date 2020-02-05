using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound_Louie : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audio;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        PlaySound();
    }
    private void OnTriggerExit(Collider other)
    {
        PauseAudio();
    }
    void PlaySound()
    {
        audio.Play();
    }
    void PauseAudio()
    {
        audio.Pause();
    }
}
