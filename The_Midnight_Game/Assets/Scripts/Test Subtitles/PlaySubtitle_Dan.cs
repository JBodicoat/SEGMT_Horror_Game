//Dan - Created 25/02/2020
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySubtitle_Dan : MonoBehaviour
{
    private AudioSource audioSource;
    private Scriptmanager_Dan scriptmanager;
    private SubtitleGUIManager_Dan guiManager;

    private void Awake()
    {
        scriptmanager = FindObjectOfType<Scriptmanager_Dan>();
        guiManager = FindObjectOfType<SubtitleGUIManager_Dan>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PlaySubtitle());
        }
    }

    private IEnumerator PlaySubtitle()
    {
        //Get and set text
        var script = scriptmanager.GetText(audioSource.clip.name);
        guiManager.SetText(script);

        //Get Value
        yield return new WaitForSeconds(audioSource.clip.length);
        guiManager.SetText(string.Empty);

    }
}
