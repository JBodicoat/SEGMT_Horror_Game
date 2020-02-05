// Morgan Pryor : 03/02/2020
// Jack : 05/02/2020 optimized distance calculation, square root calculations are expensive.
///
/// This script works out how close to the player the midnight man is and checks if the midnight man is in the same room
///
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMTargeting_Morgan : MonoBehaviour
{
    public GameObject player;
    public GameObject midnightMan;

    //current room for player and midnight man
    //aquired in currentRoom script
    internal string playerRoom;
    internal string midnightManRoom;

    private const int highNumber = 1000000;

    //I need the player and the midnightmans exact position whenever the player and the midnight man are in the same room, It would be wasteful to consider the Y pos in calculations
    internal float distanceToPlayerSquared;

    //true if player room = midnightman room
    internal bool isWithPlayer = false;
    //adjusted value is so it doesnt run the check constantly
    private bool isAdjustedWithPlayer = false;

    void Update()
    {
        // This is here so you can gage distance from player visually
        Debug.Log(distanceToPlayerSquared);

        //defining if the player is in the same room as the midnight man
        {
            if (String.Compare(playerRoom, midnightManRoom) == 0 && !isAdjustedWithPlayer)
            {
                isWithPlayer = true;
                isAdjustedWithPlayer = true;
            }
            else if (String.Compare(playerRoom, midnightManRoom) != 0 && isAdjustedWithPlayer)
            {
                isWithPlayer = false;
                isAdjustedWithPlayer = false;
            }
        }

        if (isWithPlayer)
        {
            //calculate distance to player
            //pythag giving distance from midnightMan to player
            float xDistance = midnightMan.transform.position.x - player.transform.position.x;
            float zDistance = midnightMan.transform.position.z - player.transform.position.z;
            distanceToPlayerSquared = xDistance * xDistance + zDistance * zDistance;
        }
        else if (distanceToPlayerSquared != highNumber)
        {
            //if not in same room as player, distance to player is set to a arbitrary high number
            distanceToPlayerSquared = highNumber;
        }
    }
}
