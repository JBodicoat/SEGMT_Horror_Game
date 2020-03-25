//Created by Dan - 17/03/2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPuzzle_Dan : MonoBehaviour
{
    public Animator anim;

    private const int degreesPerGameMinute = 6;
    private const int secondsPerGameMinute = 3;
    private const int degreesPerHourHandMove = 2;
    private const int minutesPerHourHandMove = 4;

    private readonly Vector3 minuteHandRoatation = new Vector3(0, 0, degreesPerGameMinute);
    private readonly Vector3 hourHandRotation = new Vector3(0, 0, degreesPerHourHandMove);

    private float timer;
    private int minutesTicked;
    private int minsPassed;
    private int hoursPassed;
    private Transform minutePivot;
    private Transform hourPivot;

    void Start()
    {
        anim = GetComponent<Animator>();
        minsPassed = 0;
        hoursPassed = 0;
        minutePivot = GameObject.Find("GFCMinutePivot").transform;
        hourPivot = GameObject.Find("GFCHourPivot").transform;
    }

    void Update()
    {
        timer += Time.deltaTime;

        //if timer reaches seconds per in game minute (3 seconds)
        if (timer >= secondsPerGameMinute)
        {
            //move minute hand once           
            minutePivot.Rotate(minuteHandRoatation);
            minutesTicked++;
            minsPassed++;
            timer = 0;
            TrackTime();
        }   

        //every 4 minutes moves the hour hand 2 degrees
        if (minutesTicked >= minutesPerHourHandMove)
        {
            hourPivot.Rotate(hourHandRotation);
            minutesTicked = 0;
        }
    }

    void TrackTime()
    {
        if (minsPassed == 60)
        {
            //add 1 to hours gone
            minsPassed = 0;
            hoursPassed++;
            anim.SetBool("isOpen", true);
        }

        else if (minsPassed >= 2)
        {
            anim.SetBool("isOpen", false);
        }
    }

}

