using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MMController_Morgan : MonoBehaviour
{
    public MMTargeting_Morgan targetScript;

    NavMeshAgent agent;
    //player target
    public Transform target;
    //MM move speed
    //places MM will nav to
    public Transform[] patrolPoints;
    int randomTarget;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
        if (new Vector2(gameObject.transform.position.x, gameObject.transform.position.z) == new Vector2(patrolPoints[randomTarget].position.x,patrolPoints[randomTarget].position.z) )
        {
            randomTarget = Random.Range(0, patrolPoints.Length);
        }
    }
}
