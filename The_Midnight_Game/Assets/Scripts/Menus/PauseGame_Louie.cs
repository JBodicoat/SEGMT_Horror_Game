//Louie - 24/02 - Handles the game being paused and unpaused, aswell as the pause menu animations.
// Jack 01/04/2020 - Added support for controller and keyboard input.
//                   Script now inherits from Menu, and all the code in 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.UI;

public class PauseGame_Louie : Menu
{
    public GameObject optionsMenu;

    private Animator pauseAnim;

    private bool isGamePaused;
    private bool inPauseMenu = false;

    private const int numButtons = 5;
    public Button[] unityButtons = new Button[numButtons];

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
            else if (isGamePaused)
            {
                ResumeGame();
            }
        }

        if(inPauseMenu)
        {
            if(HasSelectionChanged())
            {
                for(int i = 0; i < numButtons; ++i)
                {
                    if(buttons[i].IsSelected())
                    {
                        DeSelectAllButtons();
                        buttons[i].Select();
                        selectedButton = i;
                        break;
                    }
                }
            }

            InputDevice inputDevice = InputManager.ActiveDevice;
            if(InputManager.Devices.Count > 0)
            {
                if(inputDevice.Action2.WasPressed)
                {
                    ResumeGame();
                }
                else
                {
                    if (inputDevice.Direction.Down.WasPressed)
                    {
                        IncrementSelect();
                    }
                    if (inputDevice.Direction.Up.WasPressed)
                    {
                        DecrementSelect();
                    }

                    if(inputDevice.Action1.WasPressed)
                    {
                        unityButtons[selectedButton].onClick.Invoke();
                    }
                }
            }

            if(Input.GetKeyDown(KeyCode.Backspace))
            {
                ResumeGame();
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    IncrementSelect();
                }
                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    DecrementSelect();
                }

                if(Input.GetKeyDown(KeyCode.Return))
                {
                    unityButtons[selectedButton].onClick.Invoke();
                }
            }
        }
    }

    /// <summary>
    /// Pauses the game and enables the pause menu "in" animation
    /// </summary>
    public void PauseGame()
    {
        inPauseMenu = true;

        pauseAnim.SetBool("Paused", true);
        Time.timeScale = 0;
        isGamePaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Unpauses the game and enables the pause menu "out" animation
    /// </summary>
    public void ResumeGame()
    {
        inPauseMenu = false;

        pauseAnim.SetBool("Paused", false);
        Time.timeScale = 1;
        isGamePaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Launch the options menu (used by the pause menu button)
    /// </summary>
    public void OpenOptions()
    {
        inPauseMenu = false;
        optionsMenu.SetActive(true);
    }

    /// <summary>
    /// Used by the back button in the options menu to go back to the airport
    /// </summary>
    public void CloseOptions()
    {
        inPauseMenu = true;
        optionsMenu.SetActive(false);
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
        Application.Quit();
    }
}
