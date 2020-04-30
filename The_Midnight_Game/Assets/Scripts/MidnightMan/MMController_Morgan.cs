// Morgan : 04/02/2020
// Jack : 05/02/2020 Minor quality changes. Removed unnecessary Vector2
// Morgan : 10/02/2020 changed node detection to use triggers, ref NodeHit script.
// Jack : 11/02/2020 Optimized TargetLost function removing use of Mathf.Min and altering the for loop.
//                   Minor tweaks to declerations and naming to follow good practice.
//                   Fixed issue where MM would get stuck at one node.
// Jack : 23/02 Fixed loop sorting nodes based on distance by initializing i to 0 instead of 1.
//              Implemented MM speed being based on distance to player.
// Morgan : ? Implemented enraged mechanic.
// Jack 23/03/2020 Optimized enraged mechanic, speed changes and isAtTarget check.
//                 Cleaned up script - comments, naming, spacing etc.
//                 Added saving support.
// Morgan added path checking.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This is the AI for the Midnight Man
/// </summary>
[System.Serializable]
public class MMController_Morgan : MonoBehaviour
{
    // Saving game data
    public MMSaveData_Jack mmSaveData = new MMSaveData_Jack();

    public MMTargeting_Morgan mmTargetingScript;
    public NavMeshAgent agent;

    public Transform playerTransform;

    //places MM will nav to
    public Transform[] patrolPoints;
    private int targetNodeIndex;
    internal bool isAtTargetNode = false;

    //node data disection
    private float[] sqrDistanceFromNodeToTarget;

    //speed smoothing
    const float maxSpeed = 4f;
    const float minSpeed = 2f;

    const float maxDist = 400.0f;
    const float minDist = 100.0f;

    private const float enragedSpeedMultiplier = 2.0f;
    private const float enragedSpeed = maxSpeed * enragedSpeedMultiplier;

    private float speed;

    //enrage logic
    private bool isEnraged = false;
    private const int standardEnrageTime = 5;
    private float currentEnrageTime = 0;
    private bool isOrderingNodes = false;
    private Transform tempPatrolPoint;
    private float tempPatrolDist;

    // Start is called before the first frame update
    void Awake()
    {
        if (!agent)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        //random patrol target
        targetNodeIndex = Random.Range(0, patrolPoints.Length);
        sqrDistanceFromNodeToTarget = new float[patrolPoints.Length];
        speed = enragedSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            EnrageMidnightMan();
        }

        if (!mmTargetingScript.isSeen)
        {
            agent.SetDestination(patrolPoints[targetNodeIndex].position);
        }
        else
        {
            agent.SetDestination(playerTransform.position);
        }

        if (isAtTargetNode)
        {
            isAtTargetNode = false;

            float xDistance = patrolPoints[targetNodeIndex].transform.position.x - gameObject.transform.position.x;
            float zDistance = patrolPoints[targetNodeIndex].transform.position.z - gameObject.transform.position.z;
            float sqrDistanceFromTargetNode = xDistance * xDistance + zDistance * zDistance;

            if (sqrDistanceFromTargetNode < 2)
            {
                FindNewNode();
            }
        }

        //penalties logic
        if (isEnraged)
        {
            currentEnrageTime += Time.deltaTime;
            if (currentEnrageTime > standardEnrageTime)
            {
                isEnraged = false;
                currentEnrageTime = 0;
            }
        }
        else
        {
            // At minDist and lower MM speed = minSpeed. At maxDist and higher MM speed = maxSpeed
            if (mmTargetingScript.sqrDistanceToPlayer > maxDist)
            {
                speed = maxSpeed;
            }
            else if (mmTargetingScript.sqrDistanceToPlayer < minDist)
            {
                speed = minSpeed;
            }
            else
            {
                float t = (mmTargetingScript.sqrDistanceToPlayer - minDist) / (maxDist - minDist);
                speed = minSpeed * (1 - t) + maxSpeed * t;
            }

            agent.speed = speed;
        }
    }

    private void FindNewNode()
    {
        int newTarget;
        do
        {
            newTarget = Random.Range(0, patrolPoints.Length);
        } while (newTarget == targetNodeIndex);
        targetNodeIndex = newTarget;

        StartCoroutine(PathCheck());
    }

    /// <summary>
    /// Use this function to set the target to the closest node to the player.
    /// </summary>
    public void TargetLost()
    {
        //get all values
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float xDistance = patrolPoints[i].transform.position.x - mmTargetingScript.player.transform.position.x;
            float zDistance = patrolPoints[i].transform.position.z - mmTargetingScript.player.transform.position.z;
            sqrDistanceFromNodeToTarget[i] = xDistance * xDistance + zDistance * zDistance;
        }

        //store min value
        float minValue = sqrDistanceFromNodeToTarget[0];
        targetNodeIndex = 0;

        //find min value in array
        for (ushort i = 1; i < sqrDistanceFromNodeToTarget.Length; i++)
        {
            if (sqrDistanceFromNodeToTarget[i] < minValue)
            {
                minValue = sqrDistanceFromNodeToTarget[i];
                //makes the "random target" fixed to the closet node
                targetNodeIndex = i;
            }
        }
    }

    /// <summary>
    /// Use this function to teleport the MM away after the candle is blown out.
    /// </summary>
    public void TeleportMidnightManAway()
    {
        //get all values
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float xDistance = patrolPoints[i].transform.position.x - mmTargetingScript.player.transform.position.x;
            float zDistance = patrolPoints[i].transform.position.z - mmTargetingScript.player.transform.position.z;
            sqrDistanceFromNodeToTarget[i] = xDistance * xDistance + zDistance * zDistance;
        }

        //store max value
        float maxValue = sqrDistanceFromNodeToTarget[0];
        int maxValueIndex = 0;
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

        FindNewNode();
    }

    /// <summary>
    /// Use this function to teleport the MM close to the player and trigger a chase sequence.
    /// </summary>
    public void EnrageMidnightMan()
    {
        //get all values
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float xDistance = patrolPoints[i].transform.position.x - mmTargetingScript.player.transform.position.x;
            float zDistance = patrolPoints[i].transform.position.z - mmTargetingScript.player.transform.position.z;
            sqrDistanceFromNodeToTarget[i] = xDistance * xDistance + zDistance * zDistance;
        }

        //looping until a value is never changed in the loop
        do
        {
            isOrderingNodes = false;
            //sort algorithm?
            for (ushort i = 0; i < sqrDistanceFromNodeToTarget.Length - 1; i++)
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
        agent.Warp(patrolPoints[0].transform.position);
        targetNodeIndex = 0;

        speed = enragedSpeed;
        agent.speed = speed;

        //function reused to make MM go to where the player is
        isEnraged = true;
    }

    /// <summary>
    /// Checks if the target has a complete path, otherwise changes target
    /// </summary>
    /// <returns></returns>
    IEnumerator PathCheck()
    {
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(patrolPoints[targetNodeIndex].position, path);

        //give time for path to be calculated
        yield return new WaitForSeconds(0.1f);

        if (path.status != NavMeshPathStatus.PathComplete)
        {
            FindNewNode();
        }
    }

    /// <summary>
    /// Returns all the 1ave data needed for the Midnight Man.
    /// </summary>
    /// <returns></returns>
    public MMSaveData_Jack GetSaveData()
    {
        mmSaveData.xPos = transform.position.x;
        mmSaveData.yPos = transform.position.y;
        mmSaveData.zPos = transform.position.z;

        mmSaveData.targetNodeIndex = targetNodeIndex;
        mmSaveData.isAtTarget = isAtTargetNode;

        mmSaveData.sqrDistanceFromNodesToTarget = sqrDistanceFromNodeToTarget;

        mmSaveData.speed = speed;

        mmSaveData.isEnraged = isEnraged;
        mmSaveData.currentEnrageTime = currentEnrageTime;

        mmSaveData = mmTargetingScript.GetSaveData(mmSaveData);

        return mmSaveData;
    }

    /// <summary>
    /// Loads in all the passed save data.
    /// </summary>
    /// <param name="loadData"></param>
    public void LoadSaveData(MMSaveData_Jack loadData)
    {
        agent.Warp(new Vector3(loadData.xPos, loadData.yPos, loadData.zPos));

        targetNodeIndex = loadData.targetNodeIndex;
        isAtTargetNode = loadData.isAtTarget;

        sqrDistanceFromNodeToTarget = loadData.sqrDistanceFromNodesToTarget;

        speed = loadData.speed;

        isEnraged = loadData.isEnraged;
        currentEnrageTime = loadData.currentEnrageTime;

        mmTargetingScript.LoadSaveData(loadData);
    }
}
