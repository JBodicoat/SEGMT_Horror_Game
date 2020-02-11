// Morgan Pryor : 03/02/2020
// Jack : 05/02/2020 optimized distance calculation, square root calculations are expensive.
// Morgan : 10/02/2020 script will use a line of sight method of finding the player for AI navigation
// Jack : 11/02/2020 Tweaks to variable names & optimization on raycast
//                   
///
/// This script works out how close to the player the midnight man is and checks if the midnight man is in the same room
///
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MMTargeting_Morgan : MonoBehaviour
{
    public GameObject player;
    public FirstPersonController_Jack playerScript;
    private MMController_Morgan controllerScript;
    public GameObject midnightMan;

    //current room for player and midnight man
    //aquired in currentRoom script
    internal string playerRoom;
    internal string midnightManRoom;

    private const int highNumber = 1000000;

    //I need the player and the midnightmans exact position whenever the player and the midnight man are in the same room, 
    //It would be wasteful to consider the Y pos in calculations
    internal float sqrDistanceToPlayer;

    //true if player room = midnightman room
    internal bool isWithPlayer = false;
    //adjusted value is so it doesnt run the check constantly
    private bool isAdjustedWithPlayer = false;

    //Line Of Sight Code
    internal bool isSeen = false;
    //if the player is seen he must hold aggro for X amount of time;
    private const float trackTime = 5f;
    private float currentTrackTime = 0f;

    //store data of raycasts
    private RaycastHit hit;
    private const float rayDistance = 150f;

    private const string playerTag = "Player";

    private void Awake()
    {
        controllerScript = FindObjectOfType<MMController_Morgan>();
    }

    void Update()
    {
        //defining if the player is in the same room as the midnight man
        {
            if (string.Compare(playerRoom, midnightManRoom) == 0 && !isAdjustedWithPlayer)
            {
                isWithPlayer = true;
                isAdjustedWithPlayer = true;
            }
            else if (string.Compare(playerRoom, midnightManRoom) != 0 && isAdjustedWithPlayer)
            {
                isWithPlayer = false;
                isAdjustedWithPlayer = false;
            }
        }

        if (!isSeen)
        {
            //the raycast to "look" for player
            if (Physics.Raycast(gameObject.transform.position, player.transform.position - gameObject.transform.position, out hit, rayDistance))                                  // (storing this layer mask here for future refrence in case I need it)  1 << LayerMask.NameToLayer("layer"))
            {
                if (hit.transform.CompareTag(playerTag))
                {
                    isSeen = true;
                    currentTrackTime = 0f;
                }
            }
        }

        if (isSeen)
        {
            currentTrackTime += Time.deltaTime;
        }

        if (currentTrackTime > trackTime)
        {
            controllerScript.TargetLost();
            isSeen = false;
            currentTrackTime = 0f;
        }

        //is with player case
        if (isWithPlayer && isSeen)
        {
            //calculate distance to player
            //pythag giving distance from midnightMan to player
            float xDistance = midnightMan.transform.position.x - player.transform.position.x;
            float zDistance = midnightMan.transform.position.z - player.transform.position.z;
            sqrDistanceToPlayer = xDistance * xDistance + zDistance * zDistance;

            if(sqrDistanceToPlayer < 10)
            {
                playerScript.ExtinguishCandle();
            }
        }
        else if (sqrDistanceToPlayer != highNumber)
        {
            //if not in same room as player, distance to player is set to a arbitrary high number
            sqrDistanceToPlayer = highNumber;
        }
    }
}
