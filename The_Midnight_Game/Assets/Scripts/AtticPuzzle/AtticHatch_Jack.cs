// Jack 06/03/2020 Created script
// Jack 23/03/2020 Added saving support.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Opens the attic hatch when the ball is thrown at the hatch.
/// </summary>
public class AtticHatch_Jack : MonoBehaviour
{
    public GameObject hatchPivot;
    public AtticHatchAnimation_Jack hatchAnimationScript;

    private const string ballTag = "Ball";

    private bool hatchOpen = false;
    public Vector3 hatchPivotOpenRotation;

    private void OnCollisionEnter(Collision collision)
    {
        if(!hatchOpen && collision.gameObject.CompareTag(ballTag) && collision.gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude >= 10)
        {
            hatchAnimationScript.BeginAnimation();
            hatchOpen = true;
        }
    }

    /// <summary>
    /// Returns hatchOpen.
    /// </summary>
    public bool IsHatchOpen()
    {
        return hatchOpen;
    }

    /// <summary>
    /// If true is passed the hatch is set to it's open position.
    /// </summary>
    /// <param name="isHatchOpen"></param>
    public void SetHatchOpen(bool isHatchOpen)
    {
        if(isHatchOpen)
        {
            hatchOpen = true;
            hatchPivot.transform.localRotation = Quaternion.Euler(hatchPivotOpenRotation.x, hatchPivotOpenRotation.y, hatchPivotOpenRotation.z);
        }
    }
}
