//Created by Dan Downing - 03/03/2020
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedGlow_Dan : MonoBehaviour
{
    public GameObject selectedObject;
    public int redCol;
    public int greenCol;
    public int blueCol;
    public bool lookingAtObject = false;
    public bool flashingIn = true;
    public bool startedFlashing = false;

    void Update()
    {
        if(lookingAtObject == true)
        {
            selectedObject.GetComponent<Renderer>().material.color = new Color32((byte)redCol, (byte)greenCol, (byte)blueCol, 255);
        }
    }

    void OnMouseOver()
    {
        selectedObject = GameObject.Find(HighlightGlow_Dan.selectedObject);
        lookingAtObject = true;
        if(startedFlashing == false)
        {
            startedFlashing = true;
            StartCoroutine(FlashObject());
        }
    }

    void OnMouseExit()
    {
        startedFlashing = false;
        lookingAtObject = false;
        StopCoroutine(FlashObject());
        selectedObject.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
    }

    IEnumerator FlashObject()
    {
        while (lookingAtObject == true)
        {
            yield return new WaitForSeconds(0.05f);
            if (flashingIn == true)
            {
                if (redCol <= 30)
                {
                    flashingIn = false;
                }
                else
                {
                    redCol -= 25;
                    greenCol -= 1;
                }
            }
            if (flashingIn == false)
            {
                if (redCol >= 250)
                {
                    flashingIn = true;
                }
                else
                {
                    redCol += 25;
                    greenCol += 1;
                }
            }
        }
    }
}
