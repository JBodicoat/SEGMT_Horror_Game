//Louie - Created script - 06/03/2020
//Louie - Improved Script - 10/03/2020

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BunnyAI_Louie : MonoBehaviour
{
    public enum BunnyState { patrolling, idle, running, caught };

    // Start is called before the first frame update
    public NavMeshAgent nav;
    public Transform[] nodes;
    private Transform targetNode;
    private int targetNumber;
    private int speed = 3;
    private BunnyState bunnyState;
    private float distance;
    void Start()
    {
        bunnyState = BunnyState.patrolling;
        nav = GetComponent<NavMeshAgent>();
        GetNewTarget();
        nav.speed = speed;
    }
    // Update is called once per frame
    void Update()
    {
        switch (bunnyState)
        {
            case BunnyState.patrolling:
                //move to target node, then get new target and repeat
                break;
            case BunnyState.idle:
                //stand still at current target node
                break;
            case BunnyState.running:
                //run to nearest node and then go idle
                break;
            case BunnyState.caught:
                //parent to player(so theyre holding it) no movement
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Returns true if rabbit gets to its target
    /// </summary>
    /// <returns></returns>
    bool IsAtTarget()
    {
        if (nav.remainingDistance <= nav.stoppingDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /// <summary>
    /// Randomly picks a new target out of the array of nodes
    /// </summary>
    void GetNewTarget()
    {
        targetNumber = Random.Range(0, nodes.Length);
        targetNode = nodes[targetNumber];
    }

    /// <summary>
    /// Assigns the nearest node as the new target
    /// </summary>
    void GetNearestTarget()
    {
        float currentMaxDistance = 0;
        Transform closest = null;

        for (int i = 0; i < nodes.Length; i++)
        {
            distance = Vector3.Distance(transform.position, nodes[i].transform.position);

            if (distance > currentMaxDistance)
            {
                closest = nodes[i];
                currentMaxDistance = distance;
            }
        }
        targetNode = closest;
    }
    /// <summary>
    /// Used in the interaction script when the rabbit is caught
    /// </summary>
    public void CatchRabbit()
    {
        bunnyState = BunnyState.caught;
    }
    private void Caught()
    {

    }

    private void Idle()
    {

    }

    private void Running()
    {

    }

    private void Patrolling()
    {
        if (IsAtTarget())
        {
            GetNewTarget();
        }
        nav.SetDestination(targetNode.position);
    }
}
