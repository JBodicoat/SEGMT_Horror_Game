// Morgan Pryor : 03/02/2020
// Jack : 05/02/2020 optimized distance calculation, square root calculations are expensive.
// Morgan : 10/02/2020 script will use a line of sight method of finding the player for AI navigation
// Jack : 11/02/2020 Tweaks to variable names & optimization on raycast
//                   
//Louie : 16/02/2020 added Candle blow SFX to the same place its extinguished and added ice cracking and breathing
// Jack 16/03/2020 removed unused audio variables
// Jack 23/03/2020 Removed isAdjustedWithPlayer as it wasn't doing anything different than isWithPlayer
//                 Added saving support.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// This script works out how close to the player the midnight man is and checks 
/// if the midnight man is in the same room
/// </summary>
[System.Serializable]
public class MMTargeting_Morgan : MonoBehaviour
{
    public GameObject player;
    public Candle_Jack playerCandleScript;
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
    internal float sqrDistanceToPlayer = highNumber;

    const float extinguishDistance = 10.0f;

    //true if player room = midnightman room
    internal bool isWithPlayer = false;

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
            if (string.Compare(playerRoom, midnightManRoom) == 0 && !isWithPlayer)
            {
                isWithPlayer = true;
            }
            else if (string.Compare(playerRoom, midnightManRoom) != 0 && isWithPlayer)
            {
                isWithPlayer = false;
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

            if (sqrDistanceToPlayer < extinguishDistance)
            {
                playerCandleScript.ExtinguishCandle();
                controllerScript.TeleportMidnightManAway();
            }
        }
        else if (sqrDistanceToPlayer != highNumber)
        {
            //if not in same room as player, distance to player is set to a arbitrary high number
            sqrDistanceToPlayer = highNumber;
        }
    }

    /// <summary>
    /// Updates the passed in save data with the relevant data from the Midnight Man's
    /// targeting script. Used by MMController.GetSaveData().
    /// </summary>
    /// <param name="mmSaveData"></param>
    /// <returns> Returns the updated save data. </returns>
    public MMSaveData_Jack GetSaveData(MMSaveData_Jack mmSaveData)
    {
        mmSaveData.playerRoom = playerRoom;
        mmSaveData.midnigthManRoom = midnightManRoom;

        mmSaveData.sqrDistanceToPlayer = sqrDistanceToPlayer;
        mmSaveData.isWithPlayer = isWithPlayer;
        mmSaveData.isSeen = isSeen;

        mmSaveData.currentTrackTime = currentTrackTime;

        return mmSaveData;
    }

    /// <summary>
    /// Loads the passed in save data for the Midnight Man's targeting script.
    /// </summary>
    /// <param name="loadData"></param>
    public void LoadSaveData(MMSaveData_Jack loadData)
    {
        playerRoom = loadData.playerRoom;
        midnightManRoom = loadData.midnigthManRoom;

        sqrDistanceToPlayer = loadData.sqrDistanceToPlayer;
        isWithPlayer = loadData.isWithPlayer;
        isSeen = loadData.isSeen;

        currentTrackTime = loadData.currentTrackTime;
    }
}
