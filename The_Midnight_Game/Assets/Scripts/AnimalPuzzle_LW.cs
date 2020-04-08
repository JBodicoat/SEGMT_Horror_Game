//Louie 08/04/2020 - Created Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalPuzzle_LW : MonoBehaviour
{
    private bool isAtAnimalPuzzle;

    public GameObject player;
    private RaycastHit hit;
    private string playerTag;
    private float DistanceToPlayer;
    private int numberOfMovements;

    private const float minimumDistance = 15.0f;
    private const float rayDistance = 100.0f;
    private const int maxMovements = 4;

    public Transform[] animalNodes;
    private List<Transform> usedNodes = new List<Transform>();

    private float timer;
    private const float waitTime = 4.0f;
    private bool isWaitingToBark;
    private AudioSource audio;
    void Start()
    {
        isWaitingToBark = false;
        isAtAnimalPuzzle = true;
        playerTag = player.tag;
        numberOfMovements = 0;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        DistanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);

        if (isAtAnimalPuzzle)
        {
            if (Physics.Raycast(gameObject.transform.position, player.transform.position - gameObject.transform.position, out hit, rayDistance))                                  // (storing this layer mask here for future refrence in case I need it)  1 << LayerMask.NameToLayer("layer"))
            {
                Debug.DrawLine(transform.position, hit.point, Color.cyan);

                if (hit.transform.CompareTag(playerTag))
                {
                    if (audio.isPlaying)
                    {
                        print("PausingBark");
                        audio.Pause();
                    }
                    if (DistanceToPlayer <= minimumDistance)
                    {
                        MoveAnimal();
                        isWaitingToBark = true;
                        audio.Pause();
                    }
                }
                else
                {
                    if (!audio.isPlaying)
                    {
                        print("PlayingBark");
                        audio.Play();
                    }
                }
            }
        }

        if (isWaitingToBark)
        {
            timer += Time.deltaTime;

            if (timer >= waitTime)
            {
                audio.Play();
                isWaitingToBark = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            MoveAnimal();
        }
    }
    /// <summary>
    /// Enables the animal gameobject and starts activates the puzzle.
    /// </summary>
    private void EnableAnimal()
    {
        isAtAnimalPuzzle = true;
        //spawn animal
    }
    /// <summary>
    /// Moves animal to a new node.
    /// </summary>
    private void MoveAnimal()
    {
        if (numberOfMovements < maxMovements)
        {
            int node = GetNewNode();
            print("Moving to " + animalNodes[node].name);
            transform.position = animalNodes[node].position;
            usedNodes.Add(animalNodes[node]);
            numberOfMovements++;
            //puff of smoke or something (could change into a household object)
        }
        else
        {
            print("Moved Enough!");
            //move animal to final location and do the end of the puzzle/ lead to another puzzle
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
                    print("Error: " + node.name + " already used");
                    hasBeenUsed = true;
                }
            }
        }
        return hasBeenUsed;
    }
}
