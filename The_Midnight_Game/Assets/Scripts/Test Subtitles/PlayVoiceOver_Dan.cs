//Dan - Created 25/02/2020
// Jack 25/02/2020 Reviewed
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayVoiceOver_Dan : MonoBehaviour
{
    private AudioSource audioSource;
    private const string playerTag = "Player";

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
        //Display current language in console
        print(Application.systemLanguage);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            audioSource.Play();
        }
    }
}
