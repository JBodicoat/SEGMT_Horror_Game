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
    
    //check correct node


    //node data disection
    private float[] sqrDistanceFromNodeToTarget;
    private float minValue;
    private float maxValue;
    private int maxValueIndex;

    //speed smoothing
    private int baseSpeed = 2;
    private float speed;

    //enrage logic
    private bool isEnraged = false;
    private bool isAdjustedForEnrage = false;
    private const int standardEnrageTime = 5;
    private float currentEnrageTime = 0;
    private bool isOrderingNodes = false;
    private Transform tempPatrolPoint;
    private float tempPatrolDist;

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
        speed = baseSpeed;
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
            float xDistance = patrolPoints[targetNodeIndex].transform.position.x - gameObject.transform.position.x;
            float zDistance = patrolPoints[targetNodeIndex].transform.position.z - gameObject.transform.position.z;
            float SqrDistanceFromPlayerToNode = xDistance * xDistance + zDistance * zDistance;
            Debug.Log(SqrDistanceFromPlayerToNode);
            if (SqrDistanceFromPlayerToNode < 2)
            {
                int newTarget;
                do
                {

                    newTarget = Random.Range(0, patrolPoints.Length);
                } while (newTarget == targetNodeIndex);
                targetNodeIndex = newTarget;
                isAtTarget = false;
            }
            else { isAtTarget = false; }
        }

        /// save these comments, Im working on making the MM slow down as he gets closer
        //speed = (Mathf.Sqrt(targetScript.sqrDistanceToPlayer));
        //speed = ((defaultSpeed * targetScript.sqrDistanceToPlayer)/targetScript.sqrDistanceToPlayer);
        agent.speed = speed;

        //penalties logic
        if (isEnraged && !isAdjustedForEnrage)
        {
            baseSpeed *= 2;
            isAdjustedForEnrage = true;
        }
        if(isEnraged)
        {
            agent.speed = baseSpeed;
            currentEnrageTime += Time.deltaTime;
            if (currentEnrageTime > standardEnrageTime)
            {
                isEnraged = false;
                currentEnrageTime = 0;
            }
        }
        if (!isEnraged && isAdjustedForEnrage)
        {
            baseSpeed /= 2;
            isAdjustedForEnrage = false;
        }

        //debugging penalties
        if(Input.GetKeyDown(KeyCode.K))
        {
            EnrageMidnightMan();
        }

    }

    //use this function to set the target to the closest node to the player

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

    // use this function to teleport the MM away after the candle is blown out

    public void TeleportMidnightManAway()
    {
        //get all values
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float xDistance = patrolPoints[i].transform.position.x - targetScript.player.transform.position.x;
            float zDistance = patrolPoints[i].transform.position.z - targetScript.player.transform.position.z;
            sqrDistanceFromNodeToTarget[i] = xDistance * xDistance + zDistance * zDistance;
        }

        //store max value
        maxValue = sqrDistanceFromNodeToTarget[0];
        targetNodeIndex = 0;
        //find max value in array
        for (ushort i = 1; i < sqrDistanceFromNodeToTarget.Length; i++)
        {
            if (sqrDistanceFromNodeToTarget[i] > maxValue)
            {
                maxValue = sqrDistanceFromNodeToTarget[i];
                maxValueIndex = i;
                //teleport the mm to the furthest node
            }
        }
        agent.Warp(patrolPoints[maxValueIndex].transform.position);
    }

    //use this function to teleport the MM close to the player and trigger a chase sequence
    public void EnrageMidnightMan()
    {
        //get all values
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float xDistance = patrolPoints[i].transform.position.x - targetScript.player.transform.position.x;
            float zDistance = patrolPoints[i].transform.position.z - targetScript.player.transform.position.z;
            sqrDistanceFromNodeToTarget[i] = xDistance * xDistance + zDistance * zDistance;
        }

        //looping until a value is never changed in the loop
        do
        {
            isOrderingNodes = false;
            //sort algorithm?
            for (ushort i = 1; i < sqrDistanceFromNodeToTarget.Length - 1; i++)
            {
                if (sqrDistanceFromNodeToTarget[i] > sqrDistanceFromNodeToTarget[i + 1])
                {
                    //flip values if x > x + 1 for both nodes and distancesToNodes
                    tempPatrolDist = sqrDistanceFromNodeToTarget[i];
                    sqrDistanceFromNodeToTarget[i] = sqrDistanceFromNodeToTarget[i + 1];
                    sqrDistanceFromNodeToTarget[i + 1] = tempPatrolDist;

                    tempPatrolPoint = patrolPoints[i];
                    patrolPoints[i] = patrolPoints[i + 1];
                    patrolPoints[i + 1] = tempPatrolPoint;


                    //continue sort
                    isOrderingNodes = true;
                }
            }
        } while (isOrderingNodes);
        agent.Warp(patrolPoints[1].transform.position);
        targetNodeIndex = 0;

        //function reused to make MM go to where the player is
        isEnraged = true;
    }
}



