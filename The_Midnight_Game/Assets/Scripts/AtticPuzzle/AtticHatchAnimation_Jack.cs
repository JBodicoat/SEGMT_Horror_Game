// Jack 06/03/2020 Created script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the attic hatches opening animaton.
/// </summary>
public class AtticHatchAnimation_Jack : MonoBehaviour
{
    public LadderAnimation_Jack ladderAnimationScript;
    public Animator animator;

    /// <summary>
    /// Enables the attached animator.
    /// </summary>
    public void BeginAnimation()
    {
        animator.enabled = true;
    }

    /// <summary>
    /// Disables the attached animator and begins the ladders fall animator.
    /// </summary>
    public void EndAnimation()
    {
        animator.enabled = false;
        ladderAnimationScript.BeginAnimation();
    }
}
