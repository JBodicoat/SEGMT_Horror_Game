// Jack 23/03/2020 File created

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores the save data for the Midnight Man.
/// </summary>
[System.Serializable]
public class MMSaveData_Jack
{
    public float xPos;
    public float yPos;
    public float zPos;

    // ===== Controller Script ===== //
    public int targetNodeIndex;
    public bool isAtTarget;
    
    public float[] sqrDistanceFromNodesToTarget;

    public float speed;

    public bool isEnraged;
    public float currentEnrageTime;

    // ===== Targeting Script ===== //
    public string playerRoom;
    public string midnigthManRoom;

    public float sqrDistanceToPlayer;
    public bool isWithPlayer;
    public bool isSeen;

    public float currentTrackTime;
}
