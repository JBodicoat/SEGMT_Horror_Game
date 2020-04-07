// Jack 05/03/2020 Created script
// Jack 06/03/2020 Updated script to allow UI to be closed
// Jack 23/03/2020 Added saving support.
// Jack 01/04/2020 Added mouse support.

using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// Handles the piano puzzle.
/// </summary>
/// Displays key UI upon interaction with piano keys. Player must repeat the
/// sequence specified in "tune" to complete the puzzle.
public class KeyUIManager_Jack : Menu_Jack
{
    public GameObject keyUIObject;

    private const int numKeys = 7;
    private int currentKeyID = 0;

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
    private bool puzzleSolved = false;

    private InputDevice inputDevice;

    public FirstPersonController_Jack playerControllerScript;
    public Transform playerTransform;
    private const float maxSqrDistance = 10.0f;

    private bool wasClosed = false;

    // Start is called before the first frame update
    void Start()
    {
        if (!playerControllerScript)
        {
            playerControllerScript = FindObjectOfType<FirstPersonController_Jack>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        wasClosed = false;

        if(HasSelectionChanged())
        {
            for(int i = 0; i < buttons.Count; ++i)
            {
                if(buttons[i].IsSelected())
                {
                    currentKeyID = i;
                    break;
                }
            }
        }

        if (keyUIObject.activeSelf)
        {
            if ((playerTransform.position - gameObject.transform.position).sqrMagnitude > maxSqrDistance)
            {
                Close();
            }
            else
            {
                inputDevice = InputManager.ActiveDevice;
                if (InputManager.Devices.Count > 0)
                {
                    if (inputDevice.Action2.WasPressed)
                    {
                        Close();
                    }
                    else
                    {
                        if (inputDevice.Direction.Left.WasPressed)
                        {
                            SelectLeftKey();
                        }
                        else if (inputDevice.Direction.Right.WasPressed)
                        {
                            SelectRightKey();
                        }

                        if (inputDevice.Action1.WasPressed)
                        {
                            PressKey();
                        }
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        Close();
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.LeftArrow))
                        {
                            SelectLeftKey();
                        }
                        else if (Input.GetKeyDown(KeyCode.RightArrow))
                        {
                            SelectRightKey();
                        }

                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            PressKey();
                        }
                    }
                }
            }
        }
        else
        {
            // Play tune
            if (!puzzleSolved)
            {
                tuneTimer += Time.deltaTime;

                if (playingTune)
                {
                    if (tuneTimer >= timeBetweenNotes)
                    {
                        tuneTimer = 0.0f;
                        if (++currentTuneNote >= numTuneNotes)
                        {
                            playingTune = false;
                        }
                        else
                        {
                            PlayTuneNote();
                        }
                    }
                }
                else if (tuneTimer >= timeBetweenTunes)
                {
                    tuneTimer = 0.0f;
                    playingTune = true;
                    currentTuneNote = 0;
                    PlayTuneNote();
                }
            }
        }
    }

    /// <summary>
    /// Plays the keys corresponding audio clip and checks if it's the next correct key in the sequence.
    /// </summary>
    private void PressKey()
    {
        audioSource.clip = keyNotes[currentKeyID];
        audioSource.Play();

        if (!puzzleSolved)
        {
            if (tune[numNotesCorrect] == currentKeyID)
            {
                if (++numNotesCorrect >= numTuneNotes)
                {
                    puzzleSolved = true;
                    print("YEAH BOI");
                }
            }
            else
            {
                numNotesCorrect = 0;
            }
        }
    }

    /// <summary>
    /// Plays the current note of the set tune.
    /// </summary>
    private void PlayTuneNote()
    {
        audioSource.clip = keyNotes[tune[currentTuneNote]];
        audioSource.Play();
    }

    /// <summary>
    /// Unhighlights the current key and highlights the key to the right.
    /// </summary>
    private void SelectRightKey()
    {
        DeSelectAllButtons();
        if (++currentKeyID >= numKeys)
        {
            currentKeyID = 0;
        }
        buttons[currentKeyID].Select();
    }

    /// <summary>
    /// Unhighlights the current key and highlights the key to the left.
    /// </summary>
    private void SelectLeftKey()
    {
        DeSelectAllButtons();
        if (--currentKeyID < 0)
        {
            currentKeyID = numKeys - 1;
        }
        buttons[currentKeyID].Select();
    }

    /// <summary>
    /// Opens the key UI.
    /// </summary>
    public void Open()
    {
        keyUIObject.SetActive(true);
        playingTune = false;

        DeSelectAllButtons();
        currentKeyID = 0;
        buttons[currentKeyID].Select();

        playerControllerScript.SetUsingPiano(true);
    }

    /// <summary>
    /// Closes the key UI.
    /// </summary>
    public void Close()
    {
        keyUIObject.SetActive(false);
        tuneTimer = 0.0f;
        playingTune = true;

        wasClosed = true;

        playerControllerScript.SetUsingPiano(false);
    }

    /// <summary>
    /// Returns whether the key UI was closed this frame. Used for interaction.
    /// </summary>
    /// <returns></returns>
    public bool WasClosed()
    {
        return wasClosed;
    }

    /// <summary>
    /// Returns puzzleSolved.
    /// </summary>
    /// <returns></returns>
    public bool GetPuzzleSolved()
    {
        return puzzleSolved;
    }

    /// <summary>
    /// Sets puzzleSolved to the passed parameter.
    /// </summary>
    /// <param name="newPuzzleSolved"></param>
    public void SetPuzzleSolved(bool newPuzzleSolved)
    {
        puzzleSolved = newPuzzleSolved;
    }
}
