//Dan - Created 25/02/2020
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySubtitle_Dan : MonoBehaviour
{
    private AudioSource audioSource;
    private Scriptmanager_Dan scriptmanager;

    private void Awake()
    {
        scriptmanager = FindObjectOfType<Scriptmanager_Dan>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var script = scriptmanager.GetText(audioSource.clip.name);
            print(script);
        }
    }

}
