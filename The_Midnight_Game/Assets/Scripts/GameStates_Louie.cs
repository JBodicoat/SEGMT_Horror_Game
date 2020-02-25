//Louie Williamson
// 24/02 - Handles the Win and Lose states of the game.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates_Louie : MonoBehaviour
{
    // Start is called before the first frame update
    private const string endTime = "03 : 33";
    private GameObject watch;
    private const int maxSecondsPassed = 10;
    void Start()
    {
        watch = GameObject.Find("WatchPrefab");
    }

    /// <summary>
    /// This functions checks the current time and compare it to the end time set previously in the script.
    /// If they are the same, the player has finised the game and the Game Complete function is called.
    /// </summary>
    public void CheckTime()
    {
        if (watch.GetComponent<Clock_3D_LW>().textTime == endTime)
        {
            GameComplete();
        }
    }

    /// <summary>
    /// This function is used when the players candle is extinguished.
    /// It runs the game over function if the player doesn't relight the candle/ use the salt in time.
    /// </summary>
    /// <param name="minutesPassed"></param>
    /// <param name="isSaltDown"></param>
    public void CaughtByMidnightMan(int secondsPassed, bool isSaltDown)
    {
        if (secondsPassed >= maxSecondsPassed && !isSaltDown)
        {
            GameOver();
        }
    }

    /// <summary>
    /// This function will start the end game sequence for when the player completes the game.
    /// This could include starting a new scene/ dispalying cinematics etc.
    /// </summary>
    public void GameComplete()
    {
        print("GAME COMPLETE");
    }

    /// <summary>
    /// This fuction will start the end game sequence for if the player loses the game.
    /// This would include a cinematic and restarting the game.
    /// </summary>
    public void GameOver()
    {
        print("GAME OVER");
    }
}
