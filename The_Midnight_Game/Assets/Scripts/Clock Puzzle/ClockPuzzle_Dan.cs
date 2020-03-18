using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockPuzzle_Dan : MonoBehaviour
{

    // private int gameTime;
    // private bool clockMove;
    public Animator anim;

    void Start()
    {

       // Invoke("ClockDoorMove", 3.0f);


        anim = GetComponent<Animator>();

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

    //void ClockDoorMove()
    //{
    //    GameObject.Find("BodyDoor").GetComponent<Animator>().SetBool("isOpen", true);
    //}

    //void ToggleClockDoorMove()
    //{
    //    if (isOpen)
    //    {
    //        isOpen = false;
    //    }
    //}

    IEnumerator ClockSequence()
    {
        yield return new WaitForSeconds(2);
        anim.SetBool("isOpen", true);
        yield return new WaitForSeconds(6);
        anim.SetBool("isOpen", false);
        yield return new WaitForSeconds(10);
        anim.SetBool("isOpen", true);
        yield return new WaitForSeconds(5);
        anim.SetBool("isOpen", false);
    }
}

