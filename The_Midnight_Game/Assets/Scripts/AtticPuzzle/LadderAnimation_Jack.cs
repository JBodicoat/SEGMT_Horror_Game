// Jack 06/03/2020 - Created script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the ladders animation when falling out the attic.
/// </summary>
public class LadderAnimation_Jack : MonoBehaviour
{
    public Animator animator;

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
}
