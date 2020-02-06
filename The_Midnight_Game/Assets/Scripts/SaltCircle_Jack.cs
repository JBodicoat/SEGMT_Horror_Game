using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SaltCircle_Jack : MonoBehaviour
{
    private FirstPersonController_Jack playerScript;
    private const string playerTag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<FirstPersonController_Jack>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            playerScript.SetInSaltCircle(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            playerScript.SetInSaltCircle(false);
        }
    }
}
