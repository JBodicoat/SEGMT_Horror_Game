// Jack 16/03/2020 Created script// Jack 23/03/2020 Added saving supportusing System.Collections;using System.Collections.Generic;using UnityEngine;/// <summary>/// Controlls the doors animation in the valve puzzle./// </summary>public class ValvePuzzleDoor_Jack : MonoBehaviour{    public Animator animator;    public Vector3 doorPivotOpenRotation;    /// <summary>    /// Enables the doors animator beginning it's open animation.    /// </summary>    public void EnableAnimator()    {        animator.enabled = true;    }    /// <summary>    /// Disables the doors animator. Used by an animation trigger.    /// </summary>    public void EndOpenAnimation()    {        animator.enabled = false;    }    /// <summary>
    /// If true is passed the door is set to its open rotation.
    /// </summary>
    /// <param name="isDoorOpen"></param>    public void SetDoorOpen(bool isDoorOpen)
    {
        if(isDoorOpen)
        {
            gameObject.transform.localRotation = Quaternion.Euler(doorPivotOpenRotation.x, doorPivotOpenRotation.y, doorPivotOpenRotation.z);
        }
    }}