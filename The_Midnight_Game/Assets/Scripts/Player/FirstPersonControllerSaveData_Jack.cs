// Jack : 06/02/2020 Script created
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Stores the save data for the player.
/// 
/// Unity data types cannot be saved so they must be broken up into standard data type such as floats.
[System.Serializable]
public class FirstPersonControllerSaveData_Jack
{
    public float xPos;
    public float yPos;
    public float zPos;
    public ushort[] inventory;
    public bool inSaltCirlce;
}
