// Jack

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// Tests if the player is inside the salt circle or not.
/// </summary>
public class SaltCircle_Jack : MonoBehaviour
{
    private SaltPouring_Jack playerSaltScript;
    private const string playerTag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        playerSaltScript = FindObjectOfType<SaltPouring_Jack>();
    }

    /// <summary>
    /// Marks the player as being inside the salt circle on collison.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            playerSaltScript.SetInSaltCircle(true);
        }
    }

    /// <summary>
    /// Marks the player as being outside the salt circle on exiting the collider.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            playerSaltScript.SetInSaltCircle(false);
        }
    }
}
