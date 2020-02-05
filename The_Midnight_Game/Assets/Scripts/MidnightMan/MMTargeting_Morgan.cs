// Morgan Pryor : 03/02/2020
///
/// This script works out how close to the player the midnight man is and checks if the midnight man is in the same room
///
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

    const int highNumber = 1000;

    //I need the player and the midnightmans exact position whenever the player and the midnight man are in the same room, It would be wasteful to consider the Y pos in calculations
    Vector2 differenceVector;
    internal float distanceToPlayer;
    

    //true if player room = midnightman room
    internal bool isWithPlayer = false;
    //adjusted value is so it doesnt run the check constantly
    bool isAdjustedWithPlayer = false;


    void Start()
    {
        
    }

    void Update()
    {
        // This is here so you can gage distance from player visually
        Debug.Log(distanceToPlayer);

        //defining if the player is in the same room as the midnight man
        {
            if (playerRoom == midnightManRoom && !isAdjustedWithPlayer)
            {
                isWithPlayer = true;
                isAdjustedWithPlayer = true;
            }
            else if (playerRoom != midnightManRoom && isAdjustedWithPlayer)
            {
                isWithPlayer = false;
                isAdjustedWithPlayer = false;
            }
        }

        if (isWithPlayer)
        {
            {
                //calculate distance to player
                /// differenceVector = new Vector2
                ///    ((midnightMan.transform.position.x - player.transform.position.x),
                ///     (midnightMan.transform.position.z - player.transform.position.z));
                //pythag giving distance from midnightMan to player
                /// distanceToPlayer = Mathf.Sqrt((differenceVector.x * differenceVector.x) + (differenceVector.y * differenceVector.y));
            }
            //turns out all the math is done for me in this function
            distanceToPlayer = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(midnightMan.transform.position.x, midnightMan.transform.position.z));

        }
        else if (distanceToPlayer != highNumber)
        {
            //if not in same room as player, distance to player is set to a arbitrary high number
            distanceToPlayer = highNumber;
        }
    }
}
