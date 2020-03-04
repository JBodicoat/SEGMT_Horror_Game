using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BunnyAI_Louie : MonoBehaviour
{
    // Start is called before the first frame update
    public NavMeshAgent nav;
    public Transform[] nodes;
    private Transform targetNode;
    private bool isAtNode;
    private int targetNumber;
    private int speed = 3;
    void Start()
    {
        isAtNode = false;
        GetNewTarget();
        nav.speed = speed;
    }
    /// <summary>
    /// array of node positions
    /// check if bunny is at target position, if yes, new position
    /// make sure the new target isnt same as old target
    /// gl hf
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        if (isAtNode)
        {
            GetNewTarget();
        }
        nav.SetDestination(targetNode.position);
    }
    void GetNewTarget()
    {
        targetNumber = Random.Range(0, nodes.Length);

        targetNode = nodes[targetNumber];

    }
}
