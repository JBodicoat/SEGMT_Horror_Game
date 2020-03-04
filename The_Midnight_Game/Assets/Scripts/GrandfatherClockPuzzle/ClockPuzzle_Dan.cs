using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPuzzle_Dan : MonoBehaviour
{

    public int gameTime;
    private Animator keyAnim;

    private void Start()
    {
        GameObject gameTime = GameObject.Find("hoursGone");
    }

    void Update()
    {
        if (gameTime != 0)
        {
            keyAnim.SetTrigger("Clock Puzzle Trigger");
        }
    }
}

