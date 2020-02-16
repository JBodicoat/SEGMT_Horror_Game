using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SaltCircle_Jack : MonoBehaviour
{
    private SaltPouring_Jack playerSaltScript;
    private const string playerTag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        playerSaltScript = FindObjectOfType<SaltPouring_Jack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            playerSaltScript.SetInSaltCircle(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            playerSaltScript.SetInSaltCircle(false);
        }
    }
}
