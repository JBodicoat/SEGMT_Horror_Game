//Dan - Created 25/02/2020
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayVoiceOver_Dan : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
        //Display current language in console
        print(Application.systemLanguage);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.Play();
        }
    }
}
