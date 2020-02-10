// Morgan : 04/02/2020
// Jack : 05/02/2020 Minor quality changes. Removed unnecessary Vector2
// Morgan : 10/02/2020 changed node detection to use triggers, ref NodeHit script.
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
    int randomTarget;
    //change target
    internal bool isAtTarget = false;

    //node data disection
    float[] distanceFromNodeToTarget;
    float minValue;

    // Start is called before the first frame update
    void Start()
    {
        if (!agent)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        //random patrol target
        randomTarget = Random.Range(0, patrolPoints.Length);
        distanceFromNodeToTarget = new float[patrolPoints.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetScript.isSeen)
        {
            agent.SetDestination(patrolPoints[randomTarget].position);
        }
        else
        {
            agent.SetDestination(target.position);
        }
        //if (gameObject.transform.position.x == patrolPoints[randomTarget].position.x && transform.position.z == patrolPoints[randomTarget].position.z)
        if (isAtTarget)
        {
            randomTarget = Random.Range(0, patrolPoints.Length);
            isAtTarget = false;
        }


    }
    public void targetLost()
    {
        //get all values
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float xDistance = patrolPoints[i].transform.position.x - targetScript.player.transform.position.x;
            float zDistance = patrolPoints[i].transform.position.z - targetScript.player.transform.position.z;
            distanceFromNodeToTarget[i] = xDistance * xDistance + zDistance * zDistance;
        }
        //store min value
        minValue = Mathf.Min(distanceFromNodeToTarget);

        //find min value in array
        for (int i = 0; i < distanceFromNodeToTarget.Length; i++)
        {
            if (distanceFromNodeToTarget[i] == minValue)
            {
                //makes the "random target" fixed to the closet node
                randomTarget = i;
                //changes the target
                //agent.SetDestination(patrolPoints[randomTarget].position);
                //exits loop
                break;
            }
        }
    }
}

