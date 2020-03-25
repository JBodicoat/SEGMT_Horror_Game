// Jack 06/03/2020 - Created script
// Morgan 24/03/2020 - added navmesh update

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls the ladders animation when falling out the attic.
/// </summary>
public class LadderAnimation_Jack : MonoBehaviour
{
    public Animator animator;
    //the navmesh
    public NavMeshSurface surface;

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
        //update mesh after everything has set post animation
        StartCoroutine(updateMesh());
    }

    IEnumerator updateMesh()
    {
        yield return new WaitForSeconds(0.2f);
        surface.BuildNavMesh();
    }
}
