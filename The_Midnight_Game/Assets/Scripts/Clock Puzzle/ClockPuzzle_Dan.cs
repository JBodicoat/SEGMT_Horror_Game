//Created by Dan - 17/03/2020
// Jack 31/03/2020 - Reviewed
// Jack 05/04/2020 - Added saving & loading support
//                   Wrote Get and Set functions for required variables.
//                   Moved initialisation out of Start
//                   Created OpenDoor() to be used by LevelDataManager

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class header comment describing what it is used for.
/// </summary>
public class ClockPuzzle_Dan : MonoBehaviour
{
    public Animator anim;
    private const string animIsOpen = "isOpen";

    private const int degreesPerGameMinute = 6;
    private const int secondsPerGameMinute = 3;
    private const int degreesPerHourHandMove = 2;
    private const int minutesPerHourHandMove = 4;

    private readonly Vector3 minuteHandRoatation = new Vector3(0, 0, degreesPerGameMinute);
    private readonly Vector3 hourHandRotation = new Vector3(0, 0, degreesPerHourHandMove);

    private float timer = 0;
    private int minutesTicked = 0;
    private int minsPassed = 0;
    private Transform minutePivot;
    private Transform hourPivot;

    private bool doorOpen = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
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
            ++minutesTicked;
            ++minsPassed;
            timer = 0;
            CheckTime();
        }   

        //every 4 minutes moves the hour hand 2 degrees
        if (minutesTicked >= minutesPerHourHandMove)
        {
            hourPivot.Rotate(hourHandRotation);
            minutesTicked = 0;
        }
    }

    /// <summary>
    /// Function header comment describing what the function does.
    /// </summary>
    private void CheckTime()
    {
        if (minsPassed >= 60)
        {
            minsPassed = 0;
            OpenDoor();
        }

        else if (minsPassed >= 2)
        {
            anim.SetBool(animIsOpen, false);
            doorOpen = false;
        }
    }

    /// <summary>
    /// Opens the clocks door.
    /// </summary>
    public void OpenDoor()
    {
        anim.SetBool(animIsOpen, true);
        doorOpen = true;
    }

    /// <summary>
    /// Returns doorOpen.
    /// </summary>
    /// <returns></returns>
    public bool IsDoorOpen()
    {
        return doorOpen;
    }

    /// <summary>
    /// Returns minsPassed.
    /// </summary>
    /// <returns></returns>
    public int GetMinsPassed()
    {
        return minsPassed;
    }

    /// <summary>
    /// Sets minsPassed to the passed paramater.
    /// </summary>
    /// <param name="newMinsPassed"></param>
    public void SetMinsPassed(int newMinsPassed)
    {
        minsPassed = newMinsPassed;
    }

    /// <summary>
    /// Returns minutesTicked.
    /// </summary>
    /// <returns></returns>
    public int GetMinsTicked()
    {
        return minutesTicked;
    }

    /// <summary>
    /// Sets minutesTicked to the passed parameter.
    /// </summary>
    /// <param name="newMinutesTicked"></param>
    public void SetMinsTicked(int newMinutesTicked)
    {
        minutesTicked = newMinutesTicked;
    }

    /// <summary>
    /// Returns timer.
    /// </summary>
    /// <returns></returns>
    public float GetClockTimer()
    {
        return timer;
    }

    /// <summary>
    /// Sets timer to the passed parameter.
    /// </summary>
    /// <param name="newTimer"></param>
    public void SetClockTimer(float newTimer)
    {
        timer = newTimer;
    }
}
