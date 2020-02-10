// Morgan : 04/02/2020
///
/// detects the midnightman arriving at a node
/// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHit_Morgan : MonoBehaviour
{
    MMController_Morgan controllerScript;

    private void Awake()
    {
        controllerScript = FindObjectOfType<MMController_Morgan>(); //GetComponent<MMTargeting_Morgan>()
}
    private void OnTriggerEnter(Collider other)
    {
        controllerScript.isAtTarget = true;
    }
}
