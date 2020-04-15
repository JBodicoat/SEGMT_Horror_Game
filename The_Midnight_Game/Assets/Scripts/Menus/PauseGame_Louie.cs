//Louie - 24/02 - Handles the game being paused and unpaused, aswell as the pause menu animations.
//Louie - 15/04 - Added menu sounds

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PauseGame_Louie : MonoBehaviour
{
    private bool isGamePaused;
    private Animator pauseAnim;

    public GameObject OptionsMenu;

    private VideoSettings_LouieWilliamson vSettings;
    private SFXManager_LW soundManager;
    public AudioSource PauseMusic;

    void Start()
    {
        PauseMusic.ignoreListenerPause = true;
        soundManager = GameObject.Find("SFX_Manager").GetComponent<SFXManager_LW>();
        isGamePaused = false;
        pauseAnim = gameObject.GetComponent<Animator>();
        Cursor.visible = false;
        vSettings = gameObject.GetComponent<VideoSettings_LouieWilliamson>();
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
            else if (isGamePaused)
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
        pauseAnim.SetBool("Paused", true);
        Time.timeScale = 0;
        AudioListener.pause = true;   
        isGamePaused = true;
        soundManager.PlayMenuSFX(SFXManager_LW.SFX.OnPause);
        PauseMusic.Play();

    }

    /// <summary>
    /// Unpauses the game and enables the pause menu "out" animation
    /// </summary>
    public void ResumeGame()
    {
        pauseAnim.SetBool("Paused", false);
        Time.timeScale = 1;
        isGamePaused = false;
        AudioListener.pause = false;
        soundManager.PlayMenuSFX(SFXManager_LW.SFX.UIClick);
        PauseMusic.Pause();
    }

    /// <summary>
    /// Launch the options menu (used by the pause menu button)
    /// </summary>
    public void Options()
    {   
        OptionsMenu.SetActive(true);
        soundManager.PlayMenuSFX(SFXManager_LW.SFX.Swoosh);
    }

    /// <summary>
    /// Used by the back button in the options menu to go back to the airport
    /// </summary>
    public void backOptions()
    {
        soundManager.PlayMenuSFX(SFXManager_LW.SFX.Swoosh2);
        OptionsMenu.SetActive(false);
    }

    /// <summary>
    /// Save the Game (used by the pause menu button)
    /// </summary>
    public void SaveGame()
    {
        soundManager.PlayMenuSFX(SFXManager_LW.SFX.Saving);
        //insert saving game here
    }

    /// <summary>
    /// Quits the game (used by the pause menu button)
    /// </summary>
    public void QuitGame()
    {
        soundManager.PlayMenuSFX(SFXManager_LW.SFX.UIClick);
        Application.Quit();
    }
}
