// Jack 16/03/2020 Created script
// Jack 23/03/2020 Added saving support.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlls the valve puzzle.
/// </summary>
public class ValveController_Jack : MonoBehaviour
{
    public GameObject valveOne;
    public Valve_Jack valveOneScript;

    public GameObject valveTwo;
    public Valve_Jack valveTwoScript;

    public GameObject valveThree;
    public Valve_Jack valveThreeScript;

    public GameObject valveFour;
    public Valve_Jack valveFourScript;

    public GameObject valveFive;
    public Valve_Jack valveFiveScript;

    private ValvePuzzleDoor_Jack doorController;
    private bool puzzleSolved = false;

    private LayerMask defaultLayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        doorController = FindObjectOfType<ValvePuzzleDoor_Jack>();
    }

    /// <summary>
    /// Checks whether all the lights are turned on.
    /// If they are the door will open and valves will no longer be interactable.
    /// </summary>
    public void CheckLights()
    {
        if (!puzzleSolved)
        {
            if (valveOneScript.IsLightOn()
                && valveTwoScript.IsLightOn()
                && valveThreeScript.IsLightOn()
                && valveFourScript.IsLightOn()
                && valveFiveScript.IsLightOn())
            {
                doorController.EnableAnimator();

                valveOne.layer = defaultLayer;
                valveTwo.layer = defaultLayer;
                valveThree.layer = defaultLayer;
                valveFour.layer = defaultLayer;
                valveFive.layer = defaultLayer;

                puzzleSolved = true;
            }
        }
    }

    /// <summary>
    /// Sets puzzleSolved to the passed parameter.
    /// </summary>
    /// <param name="newPuzzleSolved"></param>
    public void SetPuzzleSolved(bool newPuzzleSolved)
    {
        puzzleSolved = newPuzzleSolved;
    }

    /// <summary>
    /// Returns puzzleSolved.
    /// </summary>
    /// <returns></returns>
    public bool GetPuzzleSolved()
    {
        return puzzleSolved;
    }
}
