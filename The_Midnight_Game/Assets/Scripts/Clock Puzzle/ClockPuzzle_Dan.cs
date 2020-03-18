// Dan - Created script
// Jack 18/03/2020 - Reviewed script. Cached strings, put in placeholder comments.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Description of class.
/// </summary>
public class ClockPuzzle_Dan : MonoBehaviour
{

    // private int gameTime;
    // private bool clockMove;
    public Animator anim;

    private const string animOpenBool = "isOpen";

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
    //    GameObject.Find("BodyDoor").GetComponent<Animator>().SetBool(animOpenBool, true);
    //}

    //void ToggleClockDoorMove()
    //{
    //    if (isOpen)
    //    {
    //        isOpen = false;
    //    }
    //}

        /// <summary>
        /// Description of function.
        /// </summary>
        /// <returns></returns>
    IEnumerator ClockSequence()
    {
        yield return new WaitForSeconds(2);
        anim.SetBool(animOpenBool, true);
        yield return new WaitForSeconds(6);
        anim.SetBool(animOpenBool, false);
        yield return new WaitForSeconds(10);
        anim.SetBool(animOpenBool, true);
        yield return new WaitForSeconds(5);
        anim.SetBool(animOpenBool, false);
    }
}

