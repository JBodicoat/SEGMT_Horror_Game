// Jack 23/02/2020 Script created.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for the doors animation trigger.
/// </summary>
public class LibraryDoor_Jack : MonoBehaviour
{
    public BookController_Jack bookControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        if(!bookControllerScript)
        {
            bookControllerScript = FindObjectOfType<BookController_Jack>();
        }
    }

    /// <summary>
    /// Calls DoorOpened() in bookControllerScript.
    /// </summary>
    public void DoorOpened()
    {
        bookControllerScript.DoorOpened();
    }
}
