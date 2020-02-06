// Morgan Pryor : 04/02/2020
// Jack : 05/02/2020 minor optimization changes - CompareTag, const strings and removed empty awake & update
///
/// This will be a part of a prefab which defines the rooms the player and midnightman are in
/// 
/// this can be revised later but its a quick system for aggro
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoom_Morgan : MonoBehaviour
{
    public MMTargeting_Morgan targetScript;
    //set name of room manually
    //doesnt matter, as long as no 2 rooms are named the same
    public string roomName;
    private const string playerTag = "Player";
    private const string midnightManTag = "MidnightMan";

    ///set room when room trigger entered
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            targetScript.playerRoom = roomName;
        }
        else if (other.CompareTag(midnightManTag))
        {
            targetScript.midnightManRoom = roomName;
        }
    }
}
