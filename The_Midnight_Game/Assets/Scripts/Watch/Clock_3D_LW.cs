// Louie 14/02 - Controls the pocket watch's animation and hand rotations.
// Jack : 16/02/2020 - Reviewed. Cached minute & hour hand rotation Vector3s. Cached animation trigger strings.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock_3D_LW : MonoBehaviour
{
    //Constants for watch hand movement
    private const int degreesPerGameMinute = 6;
    private const int secondsPerGameMinute = 3;
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

    //Variables to calculate/store the current time
    private string time;
    private int minutesGone;
    private int hoursGone;
    private string textMinutes;
    private string textHours;
    public string textTime;

    private GameObject gameStateManager;
    void Start()
    {
        gameStateManager = GameObject.FindGameObjectWithTag("Manager");
        minutesGone = 0;
        hoursGone = 0;
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
                minutesGone++;
                timer = 0;
                gameStateManager.GetComponent<GameStates_Louie>().CheckTime();
                TrackTime();
            }

            //every 4 minutes moves the hour hand 2 degrees
            if (minutesTicked >= minutesPerHourHandMove)
            { 
                hourPivot.Rotate(hourHandRotation);
                minutesTicked = 0;
            }
        }
    }

    ///Toggles the clock animations
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

    /// <summary>
    /// Keeps track of the current time and stores it in a string using the HH : MM format
    /// </summary>
    void TrackTime()
    {
        //if 60 minutes passed
        if (minutesGone == 60)
        {
            //add 1 to hours gone
            minutesGone = 0;
            hoursGone++;
        }

        //Sets HH:MM time format
        if (minutesGone < 10)
        {
            textMinutes = "0" + minutesGone;
        }
        else
        {
            textMinutes = "" + minutesGone;
        }

        if (hoursGone < 10)
        {
            textHours = "0" + hoursGone;
        }
        else
        {
            textHours = "" + hoursGone;
        }

        //store actual time in textTime
        textTime = textHours + " : " + textMinutes;
    }
}
