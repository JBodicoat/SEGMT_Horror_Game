//Louie - 24/02 - Handles the game being paused and unpaused, aswell as the pause menu animations.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame_Louie : MonoBehaviour
{
    private bool isGamePaused;
    private Animator pauseAnim;

    private const string animPauseBool = "Paused";

    void Start()
    {
        isGamePaused = false;
        pauseAnim = gameObject.GetComponent<Animator>();
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //if game is not paused and escape is pressed, pause the game and display pause menu
            if (!isGamePaused)
            {
                PauseGame();
            }
            //if game is paused and escape is pressed, resume the game
            else
            {
                ResumeGame();
            }
        }

        //if the game is paused, show the cursor and make sure its not locked
        if (isGamePaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        //if its not paused, hide the cursor and lock it to the centre of the screen
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    /// <summary>
    /// Pauses the game and enables the pause menu "in" animation
    /// </summary>
    public void PauseGame()
    {
        pauseAnim.SetBool(animPauseBool, true);
        Time.timeScale = 0;
        isGamePaused = true;
    }

    /// <summary>
    /// Unpauses the game and enables the pause menu "out" animation
    /// </summary>
    public void ResumeGame()
    {
        pauseAnim.SetBool(animPauseBool, false);
        Time.timeScale = 1;
        isGamePaused = false;
    }

    /// <summary>
    /// Launch the options menu (used by the pause menu button)
    /// </summary>
    public void Options()
    {
        //insert launching options menu here
    }

    /// <summary>
    /// Save the Game (used by the pause menu button)
    /// </summary>
    public void SaveGame()
    {
        //insert saving game here
    }

    /// <summary>
    /// Quits the game (used by the pause menu button)
    /// </summary>
    public void QuitGame()
    {
        //insert quitting game here
    }
}
