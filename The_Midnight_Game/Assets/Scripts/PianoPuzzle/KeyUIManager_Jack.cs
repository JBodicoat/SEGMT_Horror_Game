using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUIManager_Jack : MonoBehaviour
{
    public GameObject keyUIObject;
    
    private const int numKeys = 7;
    private int currentKeyID = 1;

    public Image[] keyHighlights = new Image[numKeys];
    public AudioClip[] keyNotes = new AudioClip[numKeys];
    public AudioSource audioSource;

    private const int numTuneNotes = 5;
    private const float timeBetweenNotes = 1.0f;
    private const float timeBetweenTunes = 5.0f;
    public int[] tune = new int[numTuneNotes];
    private int currentTuneNote = 0;
    private float tuneTimer = 0.0f;
    private bool playingTune = false;

    private int numNotesCorrect = 0;

    private InputDevice inputDevice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(keyUIObject.activeSelf)
        {
            inputDevice = InputManager.ActiveDevice;
            if(InputManager.Devices.Count > 0)
            {
                if(inputDevice.Direction.Left.IsPressed)
                {
                    DecrementCurrentKeyID();
                }
                else if(inputDevice.Direction.Right.IsPressed)
                {
                    IncrementCurrentKeyID();
                }

                if(inputDevice.Action1.IsPressed)
                {
                    PressKey();
                }
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    DecrementCurrentKeyID();
                }
                else if(Input.GetKeyDown(KeyCode.RightArrow))
                {
                    IncrementCurrentKeyID();
                }

                if(Input.GetKeyDown(KeyCode.Return))
                {
                    PressKey();
                }
            }
        }
        else
        {
            // Play tune
            tuneTimer += Time.deltaTime;

            if(playingTune)
            {
                if(tuneTimer >= timeBetweenNotes)
                {
                    tuneTimer = 0.0f;
                    if(++currentTuneNote >= numTuneNotes)
                    {
                        playingTune = false;
                    }
                    else
                    {
                        PlayTuneNote();
                    }
                }
            }
            else if(tuneTimer >= timeBetweenTunes)
            {
                tuneTimer = 0.0f;
                playingTune = true;
                currentTuneNote = 0;
                PlayTuneNote();
            }
        }
    }

    private void PressKey()
    {
        audioSource.clip = keyNotes[currentKeyID];
        audioSource.Play();

        if (tune[numNotesCorrect] == currentKeyID)
        {
            if (++numNotesCorrect >= numTuneNotes)
            {
                // Puzzle complete
                print("YEAH BOI");
            }
        }
        else
        {
            numNotesCorrect = 0;
        }
    }

    private void PlayTuneNote()
    {
        audioSource.clip = keyNotes[tune[currentTuneNote]];
        audioSource.Play();
    }

    private void IncrementCurrentKeyID()
    {
        keyHighlights[currentKeyID].enabled = false;
        if (++currentKeyID >= numKeys)
        {
            currentKeyID = 0;
        }
        keyHighlights[currentKeyID].enabled = true;
    }

    private void DecrementCurrentKeyID()
    {
        keyHighlights[currentKeyID].enabled = false;
        if(--currentKeyID < 0)
        {
            currentKeyID = numKeys - 1;
        }
        keyHighlights[currentKeyID].enabled = true;
    }

    public void OpenClose()
    {
        keyUIObject.SetActive(!keyUIObject.activeSelf);
        tuneTimer = 0.0f;
        playingTune = false;

        keyHighlights[currentKeyID].enabled = false;
        currentKeyID = 0;
        keyHighlights[currentKeyID].enabled = true;
    }
}
