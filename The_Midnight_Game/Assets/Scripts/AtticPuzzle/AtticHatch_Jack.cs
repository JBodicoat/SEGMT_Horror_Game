// Jack 06/03/2020 Created script

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

    private bool open = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(!open && collision.gameObject.CompareTag(ballTag) && collision.gameObject.GetComponent<Rigidbody>().velocity.sqrMagnitude >= 10)
        {
            hatchAnimationScript.BeginAnimation();
            open = true;
        }
    }
}
