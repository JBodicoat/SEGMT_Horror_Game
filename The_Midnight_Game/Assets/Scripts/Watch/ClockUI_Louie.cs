using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI_Louie : MonoBehaviour
{
    //Constants
    private const int degreesPerGameMinute = 6;
    private const int secondsPerGameMinute = 3;
    
    private bool isClockOn;
    private float timer;
    private int minutesTicked;
    
    //UI
    private Transform hourHandTransform;
    private Transform minuteHandTransform;
    private Text timeTextDisplay;
    private int timeHour;

    //UI Text
    private string textMinutes;
    private string textHours;
    
    void Start()
    {
        //Initialise all variables
        timeHour = 0;
        isClockOn = true;
        timer = 0;
        minutesTicked = 0;
        hourHandTransform = GameObject.Find("HourHand").transform;
        minuteHandTransform = GameObject.Find("MinuteHand").transform;
        timeTextDisplay = GameObject.Find("TimeText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //E starts the clock ---- Test Purposes Only
        if (Input.GetKey(KeyCode.E))
        {
            SetClockOn();
        }

        //F pauses the clock ---- Test Purposes Only
        else if (Input.GetKey(KeyCode.F))
        {
            PauseClock();
        }

        //if the clock is on
        if (isClockOn)
        {
            //start/add to timer
            timer += Time.deltaTime;

            //if timer reaches seconds per in game minute (3 seconds)
            if (timer >= secondsPerGameMinute)
            {
                //move minute hand once           
                minuteHandTransform.Rotate(new Vector3(0, 0, -degreesPerGameMinute));
                minutesTicked++;
                timer = 0;
            }

            //every 12 minutes move hour hand 3 degrees (degreesPerGameMinute)

            //if 60 minutes passed
            if (minutesTicked == 60)
            {
                //move hour hand once
                hourHandTransform.Rotate(new Vector3(0, 0, -degreesPerGameMinute * 5));
                minutesTicked = 0;
                timeHour++;
            }

            //Sets HH:MM time format
            if (minutesTicked < 10)
            {
                textMinutes = "0" + minutesTicked;
            }
            else
            {
                textMinutes = "" + minutesTicked;
            }

            if (timeHour < 10)
            {
                textHours = "0" + timeHour;
            }
            else
            {
                textHours = "" + timeHour;
            }
            timeTextDisplay.text = textHours + " : " + textMinutes;
        }
    }

    void SetClockOn()
    {
        //set animation in?
        //clock starts at midnight
        isClockOn = true;
    }

    void PauseClock()
    {
        isClockOn = false;
    }
}
