//Created by Dan Downing - 03/03/2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightGlow_Dan : MonoBehaviour
{
    public static string selectedObject;
    public string viewedObject;
    public RaycastHit theObject;


    void Update()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out theObject))
        {
            selectedObject = theObject.transform.gameObject.name;
            viewedObject = theObject.transform.gameObject.name;
        }
    }
}
