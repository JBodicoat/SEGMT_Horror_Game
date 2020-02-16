using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock_3D_LW : MonoBehaviour
{
    //Constants for watch hand movement
    private const int degreesPerGameMinute = 6;
    private const int secondsPerGameMinute = 1;
    private const int degreesPerHourHandMove = 2;
    private const int minutesPerHourHandMove = 4;

    private bool isClockOn;
    private float timer;
    private int minutesTicked;

    private Transform minutePivot;
    private Transform hourPivot;

    private Animator anim;
    private bool isClockOnScreen;
    void Start()
    {
        isClockOn = true;
        minutePivot = GameObject.Find("MinutePivot").transform;
        hourPivot = GameObject.Find("HourPivot").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Animation Testing -- Press C to toggle on or off the clock on screen
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleClockAnim();
        }



        if (isClockOn)
        {
            //start/add to timer
            timer += Time.deltaTime;

            //if timer reaches seconds per in game minute (3 seconds)
            if (timer >= secondsPerGameMinute)
            {
                //move minute hand once           
                minutePivot.Rotate(new Vector3(0, 0, degreesPerGameMinute));
                minutesTicked++;
                timer = 0;
            }

            //every 4 minutes moves the hour hand 2 degrees
            if (minutesTicked == minutesPerHourHandMove)
            { 
                hourPivot.Rotate(new Vector3(0, 0, degreesPerHourHandMove));
                minutesTicked = 0;
            }
        }
    }
    
    // Used to toggle on the clock
    void ClockOn()
    {
        isClockOn = true;
    }

    //Used to toggle off the clock (useful for pausing the game)
    void ClockOff()
    {
        isClockOn = false;
    }

    //Toggles the clock animations
    void ToggleClockAnim()
    {
        //if the clock is on the screen, then enable out animation and update boolean
        if (isClockOnScreen)
        {
            anim.SetTrigger("Out");
            isClockOnScreen = false;
        }
        //if the clock is NOT on the screen, then enable in animation and update boolean
        else
        {
            anim.SetTrigger("In");
            isClockOnScreen = true;
        }

    }

}
