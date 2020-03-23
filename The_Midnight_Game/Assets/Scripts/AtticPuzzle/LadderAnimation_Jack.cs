// Jack 06/03/2020 - Created script
// Jack 23/03/2020 Added saving support.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the ladders animation when falling out the attic.
/// </summary>
public class LadderAnimation_Jack : MonoBehaviour
{
    public Animator animator;
    public Vector3 ladderDownPosition;

    /// <summary>
    /// Enables the attached animator.
    /// </summary>
    public void BeginAnimation()
    {
        animator.enabled = true;
    }

    /// <summary>
    /// Disables the attached animator.
    /// </summary>
    public void EndAnimation()
    {
        animator.enabled = false;
    }

    /// <summary>
    /// If true is passed the ladder is set to it's down position.
    /// </summary>
    /// <param name="isLadderDown"></param>
    public void SetLadderDown(bool isLadderDown)
    {
        if(isLadderDown)
        {
            transform.localPosition = ladderDownPosition;
        }
    }
}
