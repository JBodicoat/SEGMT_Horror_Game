//Louie 08/04/2020 - Created Script
//Louie 17/04/2020 - Added looking at player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all of the dog's behaviour in the animal puzzle.
/// </summary>
public class AnimalPuzzle_LW : MonoBehaviour
{
    //Created for the script
    private bool isAtAnimalPuzzle;
    private float DistanceToPlayer;
    private int numberOfMovements;
    private RaycastHit hit;
    private float timer;
    private bool isWaitingToBark;
    private Vector3 targetPosition;
    private float moveTimer;
    //Constants
    private const float minimumDistance = 15.0f;
    private const float minimumBarkingDistance = 25.0f;
    private const float rayDistance = 100.0f;
    private const int maxMovements = 4;
    private const float waitTime = 4.0f;

    //References
    public GameObject player;
    private string playerTag;
    public Transform[] animalNodes;
    private List<Transform> usedNodes = new List<Transform>();
    public Transform spawn;
    private AudioSource barking;
    public Transform ParentGO;

    void Start()
    {
        isWaitingToBark = false;
        isAtAnimalPuzzle = false;
        playerTag = player.tag;
        numberOfMovements = 0;
        moveTimer = 0;
        barking = GetComponent<AudioSource>();

        EnableAnimal();
    }

    // Update is called once per frame
    void Update()
    {
        DistanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if (isAtAnimalPuzzle)
        {
            LookAtPlayer();

            if (Physics.Raycast(gameObject.transform.position, player.transform.position - gameObject.transform.position, out hit, rayDistance))
            {
                //if the animal can see the player
                if (hit.transform.CompareTag(playerTag))
                {
                    if (barking.isPlaying && DistanceToPlayer <= minimumBarkingDistance)
                    {
                        barking.Pause();
                    }
                    else if (!barking.isPlaying && DistanceToPlayer > minimumBarkingDistance)
                    {
                        barking.Play();
                    }
                    //if the player is closer to the animal than minimum distance, move the animal and delay the barking sound
                    if (DistanceToPlayer <= minimumDistance)
                    {
                        moveTimer += Time.deltaTime;
                        if (moveTimer >= 3)
                        {
                            MoveAnimal();
                            isWaitingToBark = true;
                            barking.Pause();
                            moveTimer = 0;
                        }
                    }
                }
                //if the animal cant see the player
                else
                {
                    if (!barking.isPlaying)
                    {
                        barking.Play();
                    }
                }
            }
        }
        //This causes the delay between barking after the animal has bee moved.
        if (isWaitingToBark)
        {
            timer += Time.deltaTime;

            if (timer >= waitTime)
            {
                barking.Play();
                timer = 0;
                isWaitingToBark = false;
            }
        }       
    }

    /// <summary>
    /// This function ensure the dog is always lookng towards the player.
    /// </summary>
    private void LookAtPlayer()
    {
        targetPosition = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        ParentGO.LookAt(targetPosition);
    }

    /// <summary>
    /// Enables the animal gameobject and starts activates the puzzle.
    /// </summary>
    public void EnableAnimal()
    {
        isAtAnimalPuzzle = true;
        isWaitingToBark = true;
        ParentGO.position = spawn.position;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
    /// <summary>
    /// Moves animal to a new node.
    /// </summary>
    private void MoveAnimal()
    {
        if (numberOfMovements < maxMovements)
        {
            int node = GetNewNode();
            ParentGO.position = animalNodes[node].position;
            usedNodes.Add(animalNodes[node]);
            numberOfMovements++;
            //puff of smoke or something (could change into a household object) or play a SFX?
        }
        else
        {
            print("Moved Enough!");
            //move animal to final location and do the end of the puzzle/ lead to another puzzle/ reward
        }
    }
    /// <summary>
    /// Gets a new unique node.
    /// </summary>
    /// <returns></returns>
    private int GetNewNode()
    {
        int node = Random.Range(1, animalNodes.Length);

        while(CheckIfAlreadyUsed(animalNodes[node]))
        {            
            node = Random.Range(1, animalNodes.Length);
        }

        return node;
    }

    /// <summary>
    /// Returns true if the node(transform) passed to it has already been used by the animal.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private bool CheckIfAlreadyUsed(Transform node)
    {
        bool hasBeenUsed = false;

        if (numberOfMovements != 0)
        {
            for (int i = 0; i < usedNodes.Count; i++)
            {
                if (node == usedNodes[i])
                {
                    hasBeenUsed = true;
                }
            }
        }
        return hasBeenUsed;
    }
}
