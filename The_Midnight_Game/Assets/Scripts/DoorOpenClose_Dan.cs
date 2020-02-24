﻿//Dan
//Script to open/close doors

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose_Dan : MonoBehaviour
{
    private bool isDoorTrigger = false;

    public Animator DoorAnimator;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isDoorTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isDoorTrigger = false;
            DoorAnimator.SetBool("open", false);
        }
    }


    // Update is called once per frame
    void Update()
    {

            if (Input.GetKeyDown(KeyCode.E))
            {
                DoorAnimator.SetBool("open", true);
            }
    }
}