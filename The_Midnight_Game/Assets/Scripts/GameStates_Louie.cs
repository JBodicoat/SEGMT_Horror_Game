//Louie Williamson
// 24/02 - Handles the Win and Lose states of the game.
// Morgan pryor - 26/02/2020 - Added eggs for bart
// Jack 09/03/2020 - Reviewed Jump-Scare
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
    float count;
    Candle_Jack candleScript;
    SaltPouring_Jack saltScript;
    bool gameOver = false;

    // Start is called before the first frame update
    private const string endTime = "03 : 33";
    private GameObject watch;
    private const int maxSecondsPassed = 10;

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
        if (!candleScript.IsCandleLit())
        { 
            CaughtByMidnightMan();
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
    /// This function is used when the players candle is extinguished.
    /// It runs the game over function if the player doesn't relight the candle/ use the salt in time.
    /// </summary>
    /// <param name="minutesPassed"></param>
    /// <param name="isSaltDown"></param>
    public void CaughtByMidnightMan()//float secondsPassed, bool isSaltDown, bool isCandleRelit)
    {
        if (!gameOver)
        {
            count += Time.deltaTime;

            if (count >= maxSecondsPassed && !saltScript.IsInSaltCircle())
            {
                GameOver();
            }
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
        gameOver = true;

        mainCamera.enabled = false;
        scareCamera.enabled = true;
        jumpScare.Play();
    }
}
