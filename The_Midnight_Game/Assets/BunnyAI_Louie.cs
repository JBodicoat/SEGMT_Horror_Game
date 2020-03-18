﻿//Louie - Created script - 06/03/2020
//Louie - Improved Script - 10/03/2020
// Jack 18/03/2020 - Reviewed. Optimized distance calculation. Set speed to const. Reformatting.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public enum BunnyState { patrolling, idle, running, caught };
public class BunnyAI_Louie : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent nav;
    public Transform[] nodes;
    private Transform targetNode;
    private int targetNumber;
    private const int speed = 3;
    private BunnyState bunnyState;
    private float sqrDistance;

    void Start()
    {
        bunnyState = BunnyState.patrolling;
        nav = GetComponent<NavMeshAgent>();
        GetNewTarget();
        nav.SetDestination(targetNode.position);
        nav.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        switch (bunnyState)
        {
            case BunnyState.patrolling:
                //move to target node, then get new target and repeat
                Patrolling();
                break;
            case BunnyState.idle:
                //stand still at current target node
                Idle();
                break;
            case BunnyState.running:
                //run to nearest node and then go idle
                Running();
                break;
            case BunnyState.caught:
                //parent to player(so theyre holding it) no movement
                Caught();
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
        return (nav.remainingDistance <= nav.stoppingDistance);
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
        float currentMaxSqrDistance = 0;
        Transform closest = null;

        for (int i = 0; i < nodes.Length; i++)
        {
            float xDifference = transform.position.x - nodes[i].transform.position.x;
            float zDifference = transform.position.z - nodes[i].transform.position.z;
            sqrDistance = xDifference * xDifference + zDifference * zDifference;

            if (sqrDistance > currentMaxSqrDistance)
            {
                closest = nodes[i];
                currentMaxSqrDistance = sqrDistance;
            }
        }
        targetNode = closest;
    }

    /// <summary>
    /// Used in the interaction script when the rabbit is caught
    /// </summary>
    public void ChangeRabbitState(BunnyState b)
    {
        bunnyState = b;
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
            nav.SetDestination(targetNode.position);
        }
        print("Target" + targetNode.gameObject.name);
    }
}
