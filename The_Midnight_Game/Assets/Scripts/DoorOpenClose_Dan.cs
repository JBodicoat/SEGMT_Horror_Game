//Dan
//Script to open/close doors

// Morgan 03/03/2020 - modified to work with the interaction key

// Jack 11/03/2020 - Reviewed. Cached strings and removed unnecessary if statements.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Description of class here.
/// </summary>
public class DoorOpenClose_Dan : MonoBehaviour
{
    private bool isOpen = false;
    private const string animOpenBool = "open";

    public Animator DoorAnimator;

    /// <summary>
    /// Description of function here.
    /// </summary>
    public void DoorMechanism()
    {
        isOpen = !isOpen;
        DoorAnimator.SetBool(animOpenBool, isOpen);
    }
}