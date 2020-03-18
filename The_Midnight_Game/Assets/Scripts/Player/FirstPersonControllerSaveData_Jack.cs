﻿// Jack : 06/02/2020 Script created
// Jack 13/02/2020 - Added saving of player's rotation & candle
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
    public float xRot;
    public float yRot;
    public float zRot;
    public float cameraXRot;
    public float cameraYRot;
    public float cameraZRot;
    public ushort[] inventory;
    public bool inSaltCirlce;
    public bool candleLit;
}