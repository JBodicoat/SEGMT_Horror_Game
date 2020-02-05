// Morgan Pryor : 04/02/2020
// Jack : 05/02/2020 Minor quality changes. Removed unnecessary Vector2
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
    //MM move speed
    //places MM will nav to
    public Transform[] patrolPoints;
    private int randomTarget;
    // Start is called before the first frame update
    void Start()
    {
        if (!agent)
        {
            agent = GetComponent<NavMeshAgent>();
        }
        //random patrol target
        randomTarget = Random.Range(0, patrolPoints.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetScript.isWithPlayer)
        {
            agent.SetDestination(patrolPoints[randomTarget].position);
        }
        else
        {
            agent.SetDestination(target.position);
        }
        if (gameObject.transform.position.x == patrolPoints[randomTarget].position.x && transform.position.z == patrolPoints[randomTarget].position.z)
        {
            randomTarget = Random.Range(0, patrolPoints.Length);
        }
    }
}
