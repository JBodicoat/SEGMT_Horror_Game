//Louie Williamson
// 24/02 - Handles the Win and Lose states of the game.
// Morgan pryor - 26/02/2020 - Added eggs for bart
// Jack 09/03/2020 - Reviewed Jump-Scare
// Jack 24/03/2020 - Altered check for whether the players candle is lit to better support salt circles and to fix
//                   a bug where the timer is not reset after relighting the candle.
// Dan 08/04/2020 - Added opening cinematic functionality

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class GameStates_Louie : MonoBehaviour
{
    //eggs for bart functionality
    public Camera mainCamera;
    public Camera scareCamera;
    private VideoPlayer jumpScare;
    float timeSincePlayerNotSafe;
    Candle_Jack candleScript;
    SaltPouring_Jack saltScript;
    bool gameOver = false;

    // Start is called before the first frame update
    private const string endTime = "03 : 33";
    private GameObject watch;
    private const int maxSecondsPassed = 10;
    private bool playerIsSafe = false;

    void Awake()
    {
        watch = GameObject.Find("WatchPrefab");
        candleScript = FindObjectOfType<Candle_Jack>();
        saltScript = FindObjectOfType<SaltPouring_Jack>();
        jumpScare = FindObjectOfType<VideoPlayer>();
        scareCamera.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && !playerIsSafe && !candleScript.IsCandleLit())
        {
            timeSincePlayerNotSafe += Time.deltaTime;

            if (timeSincePlayerNotSafe >= maxSecondsPassed)
            {
                GameOver();
            }
        }
        else
        {
            timeSincePlayerNotSafe = 0;
        }
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
    /// Sets playerIsSafe to true protecting them from the Midnight Man.
    /// </summary>
    public void SetPlayerSafe()
    {
        playerIsSafe = true;
    }

    /// <summary>
    /// This function is used when the players candle is extinguished or exits a salt circle with the candle unlit.
    /// It runs the game over function if the player doesn't relight the candle/ use the salt in time.
    /// </summary>
    /// <param name="minutesPassed"></param>
    /// <param name="isSaltDown"></param>
    public void SetPlayerUnsafe()//float secondsPassed, bool isSaltDown, bool isCandleRelit)
    {
        timeSincePlayerNotSafe = 0;
        playerIsSafe = false;
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
        gameOver = true;

        mainCamera.enabled = false;
        scareCamera.enabled = true;
        jumpScare.Play();
    }
}
