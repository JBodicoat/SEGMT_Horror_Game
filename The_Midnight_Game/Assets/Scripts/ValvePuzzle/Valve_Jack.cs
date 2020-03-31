// Jack 16/03/2020 Created script
// Jack 23/03/2020 Added saving support.
// Jack 31/03/2020 Changed the valve puzzle so that valves are connected to set lights on each turn.
//                 E.g. on turn one valve one toggles lights a, b, c
//                      on turn two valve one toggles lights b, c, e

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls individual valves.
/// </summary>
public class Valve_Jack : MonoBehaviour
{
    private ValveController_Jack valveController;

    private const int maxConnectedLights = 5;
    private const int maxSequences = 3;
    private int currentSequence = 0;
    public int numSequences;

    public ValveLight_Jack[] sequenceOne = new ValveLight_Jack[maxConnectedLights];
    public ValveLight_Jack[] sequenceTwo = new ValveLight_Jack[maxConnectedLights];
    public ValveLight_Jack[] sequenceThree = new ValveLight_Jack[maxConnectedLights];

    private readonly ValveLight_Jack[,] connectedLights = new ValveLight_Jack[maxSequences, maxConnectedLights];

    public Animator animator;
    private bool turning = false;

    // Start is called before the first frame update
    void Awake()
    {
        valveController = FindObjectOfType<ValveController_Jack>();

        // Initialise connectedLights with existing sequences.
        for(int i = 0; i < maxConnectedLights; ++i)
        {
            connectedLights[0, i] = sequenceOne[i];
            connectedLights[1, i] = sequenceTwo[i];
            connectedLights[2, i] = sequenceThree[i];
        }
    }

    /// <summary>
    /// Begins the valves turn animation.
    /// </summary>
    public void StartTurn()
    {
        if (!valveController.GetPuzzleSolved() && !turning)
        {
            animator.enabled = true;
            turning = true;
        }
    }

    /// <summary>
    /// Toggles the valve and any connected valve lights.
    /// Called by the valves animation trigger.
    /// </summary>
    public void EndTurn()
    {
        if (!valveController.GetPuzzleSolved())
        {
            for(int i = 0; i < maxConnectedLights; ++i)
            {
                if(connectedLights[currentSequence, i])
                {
                    connectedLights[currentSequence, i].SwitchLight();
                }
            }
            IncrementCurrentSequence();

            valveController.CheckLights();
        }

        animator.enabled = false;
        turning = false;
    }

    /// <summary>
    /// Increments currentSequence, setting i to 0 if it reaches numSequences.
    /// </summary>
    private void IncrementCurrentSequence()
    {
        if(++currentSequence >= numSequences)
        {
            currentSequence = 0;
        }
    }
}
