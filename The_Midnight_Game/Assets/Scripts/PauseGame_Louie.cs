using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame_Louie : MonoBehaviour
{
    private bool isGamePaused;
    private Animator pauseAnim;
    void Start()
    {
        isGamePaused = false;
        pauseAnim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
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
    }
    /// <summary>
    /// Pauses the game and enables the pause menu "in" animation
    /// </summary>
    public void PauseGame()
    {
        pauseAnim.SetTrigger("In");
        Time.timeScale = 0;
        isGamePaused = true;
        Cursor.visible = true;
        print("PAUSE");
    }

    /// <summary>
    /// Unpauses the game and enables the pause menu "out" animation
    /// </summary>
    public void ResumeGame()
    {
        pauseAnim.SetTrigger("Out");
        Time.timeScale = 1;
        isGamePaused = false;
        Cursor.visible = true;
        print("UNPAUSE");
    }
}
