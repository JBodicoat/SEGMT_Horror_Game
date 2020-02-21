// Morgan : 04/02/2020
// Jack : 11/02/2020 added check so only midnight man can activate OnTriggerEnter
///
/// detects the midnightman arriving at a node
/// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHit_Morgan : MonoBehaviour
{
    private MMController_Morgan controllerScript;
    private const string midnightManTag = "MidnightMan";

    private void Awake()
    {
        controllerScript = FindObjectOfType<MMController_Morgan>(); //GetComponent<MMTargeting_Morgan>()
}
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(midnightManTag))
        {
            controllerScript.isAtTarget = true;
        }
    }
}

