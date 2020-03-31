// Jack 16/03/2020 Created script
// Jack 23/03/2020 Added saving support.
// Jack 31/03/2020 Now checks ValveLight scripts to see if lights are on rather than Valve scripts.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlls the valve puzzle.
/// </summary>
public class ValveController_Jack : MonoBehaviour
{
    private const int numberOfValves = 5;
    public GameObject[] valveMeshObjects = new GameObject[numberOfValves];
    public ValveLight_Jack[] valveLights = new ValveLight_Jack[numberOfValves];

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
            int i;
            for(i = 0; i < numberOfValves; ++i)
            {
                if(!valveLights[i].IsLightOn())
                {
                    break;
                }
            }
            if (i == numberOfValves)
            {
                CompletePuzzle();
            }
        }
    }

    private void CompletePuzzle()
    {
        doorController.EnableAnimator();

        foreach (GameObject valve in valveMeshObjects)
        {
            valve.layer = defaultLayer;
        }

        puzzleSolved = true;
    }

    /// <summary>
    /// Sets puzzleSolved to the passed parameter.
    /// </summary>
    /// <param name="newPuzzleSolved"></param>
    public void SetPuzzleSolved(bool newPuzzleSolved)
    {
        if(newPuzzleSolved)
        {
            CompletePuzzle();
        }
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
