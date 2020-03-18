using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPuzzle_Dan : MonoBehaviour
{

    // private int gameTime;
    // private bool clockMove;
    // private Animator clockPuzzle;


    private bool isOpen = false;

    void Start()
    {

       // Invoke("ClockDoorMove", 3.0f);


        //clockPuzzle = GetComponent<Animator>();

        StartCoroutine(ClockSequence());
    }

    void Update()
    {
       // if (isOpen)
       // {
       //     //Invoke("ClockDoorMove", 3.0f);
       //     isOpen = false;
       // }

    }

    void ClockDoorMove()
    {
        GameObject.Find("BodyDoor").GetComponent<Animator>().SetBool("isOpen", true);
    }

    //void ToggleClockDoorMove()
    //{
    //    if (isOpen)
    //    {
    //        isOpen = false;
    //    }
    //}

    IEnumerator ClockSequence()
    {
        Invoke("ClockDoorMove", 3.0f);
        isOpen = false;
        yield return new WaitForSeconds(3);
    }
}

