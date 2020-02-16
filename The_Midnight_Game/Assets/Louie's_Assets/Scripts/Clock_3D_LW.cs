// Louie
// Jack : 16/02/2020 - Reviewed. Cached minute & hour hand rotation Vector3s. Cached animation trigger strings.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the pocket watch's animation and hand rotations.
/// </summary>
public class Clock_3D_LW : MonoBehaviour
{
    //Constants for watch hand movement
    private const int degreesPerGameMinute = 6;
    private const int secondsPerGameMinute = 1;
    private const int degreesPerHourHandMove = 2;
    private const int minutesPerHourHandMove = 4;

    private readonly Vector3 minuteHandRoatation = new Vector3(0, 0, degreesPerGameMinute);
    private readonly Vector3 hourHandRotation = new Vector3(0, 0, degreesPerHourHandMove);

    private const string outTrigger = "Out";
    private const string inTrigger = "In";

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
                minutePivot.Rotate(minuteHandRoatation);
                minutesTicked++;
                timer = 0;
            }

            //every 4 minutes moves the hour hand 2 degrees
            if (minutesTicked >= minutesPerHourHandMove)
            { 
                hourPivot.Rotate(hourHandRotation);
                minutesTicked = 0;
            }
        }
    }
    

    /// Toggles the clock animations
    void ToggleClockAnim()
    {
        //if the clock is on the screen, then enable out animation and update boolean
        if (isClockOnScreen)
        {
            anim.SetTrigger(outTrigger);
            isClockOnScreen = false;
        }
        //if the clock is NOT on the screen, then enable in animation and update boolean
        else
        {
            anim.SetTrigger(inTrigger);
            isClockOnScreen = true;
        }

    }

}
