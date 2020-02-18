// Morgan : 04/02/2020
// Jack : 05/02/2020 Minor quality changes. Removed unnecessary Vector2
// Morgan : 10/02/2020 changed node detection to use triggers, ref NodeHit script.
// Jack : 11/02/2020 Optimized TargetLost function removing use of Mathf.Min and altering the for loop.
//                   Minor tweaks to declerations and naming to follow good practice.
//                   Fixed issue where MM would get stuck at one node.
///
/// This is the AI for the Midnight Man
/// 
/// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MMController_Morgan : MonoBehaviour
{
    public MMTargeting_Morgan targetScript;

    public NavMeshAgent agent;
    //player target
    public Transform target;
    //places MM will nav to
    public Transform[] patrolPoints;
    private int targetNodeIndex;
    //change target
    internal bool isAtTarget = false;

    //node data disection
    private float[] sqrDistanceFromNodeToTarget;
    private float minValue;
    private float maxValue;
    private int maxValueIndex;

    // Start is called before the first frame update
    void Start()
    {
        if (!agent)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        //random patrol target
        targetNodeIndex = Random.Range(0, patrolPoints.Length);
        sqrDistanceFromNodeToTarget = new float[patrolPoints.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetScript.isSeen)
        {
            agent.SetDestination(patrolPoints[targetNodeIndex].position);
        }
        else
        {
            agent.SetDestination(target.position);
        }
        
        if (isAtTarget)
        {
            int newTarget;
            do
            {
                newTarget = Random.Range(0, patrolPoints.Length);
            } while (newTarget == targetNodeIndex);
            targetNodeIndex = newTarget;
            isAtTarget = false;
        }


    }

    /// This should be commented.
    public void TargetLost()
    {
        //get all values
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float xDistance = patrolPoints[i].transform.position.x - targetScript.player.transform.position.x;
            float zDistance = patrolPoints[i].transform.position.z - targetScript.player.transform.position.z;
            sqrDistanceFromNodeToTarget[i] = xDistance * xDistance + zDistance * zDistance;
        }

        //store min value
        minValue = sqrDistanceFromNodeToTarget[0];
        targetNodeIndex = 0;

        //find min value in array
        for (ushort i = 1; i < sqrDistanceFromNodeToTarget.Length; i++)
        {
            if(sqrDistanceFromNodeToTarget[i] < minValue)
            {
                minValue = sqrDistanceFromNodeToTarget[i];
                //makes the "random target" fixed to the closet node
                targetNodeIndex = i;
            }
        }
    }

    public void TeleportMidnightManAway()
    {
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float xDistance = patrolPoints[i].transform.position.x - targetScript.player.transform.position.x;
            float zDistance = patrolPoints[i].transform.position.z - targetScript.player.transform.position.z;
            sqrDistanceFromNodeToTarget[i] = xDistance * xDistance + zDistance * zDistance;
        }

        //store min value
        maxValue = sqrDistanceFromNodeToTarget[0];
        targetNodeIndex = 0;

        //find min value in array
        for (ushort i = 1; i < sqrDistanceFromNodeToTarget.Length; i++)
        {
            if (sqrDistanceFromNodeToTarget[i] > maxValue)
            {
                maxValue = sqrDistanceFromNodeToTarget[i];
                maxValueIndex = i;
                //teleport the mm to the furthest node
            }
        }
        //gameObject.transform.position = new Vector3(patrolPoints[maxValueIndex].transform.position.x, gameObject.transform.position.y, patrolPoints[maxValueIndex].transform.position.z);
        agent.Warp(patrolPoints[maxValueIndex].position);
        gameObject.transform.position = patrolPoints[maxValueIndex].position;
    }
}

