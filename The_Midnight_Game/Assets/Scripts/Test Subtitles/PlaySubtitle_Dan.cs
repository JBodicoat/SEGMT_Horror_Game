//Dan - Created 25/02/2020
// Jack 25/02/2020 Reviewed - cached playerTag
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySubtitle_Dan : MonoBehaviour
{
    private AudioSource audioSource;
    private Scriptmanager_Dan scriptmanager;
    private SubtitleGUIManager_Dan guiManager;
    private const string playerTag = "Player";

    private void Awake()
    {
        scriptmanager = FindObjectOfType<Scriptmanager_Dan>();
        guiManager = FindObjectOfType<SubtitleGUIManager_Dan>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            StartCoroutine(PlaySubtitle());
        }
    }

    private IEnumerator PlaySubtitle()
    {
        //Get and set text
        string script = scriptmanager.GetText(audioSource.clip.name);
        guiManager.SetText(script);

        //Get Value
        yield return new WaitForSeconds(audioSource.clip.length);
        guiManager.SetText(string.Empty);

    }
}
