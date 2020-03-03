//Dan
//Script to open/close doors

// Morgan 03/03/2020 - modified to work with the interaction key

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose_Dan : MonoBehaviour
{
    bool isOpen = false;

    public Animator DoorAnimator;

    public void DoorMechanism()
    {
        if (isOpen)
        {
            DoorAnimator.SetBool("open", false);
            isOpen = false;
        }
        else if (!isOpen)
        {
            DoorAnimator.SetBool("open", true);
            isOpen = true;
        }
    }
}