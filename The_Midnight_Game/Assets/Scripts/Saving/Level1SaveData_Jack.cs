// Jack
// Jack 13/02/2020 - Added saving of tablet puzzle, player's rotation & candle
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/// Stores the save data for level 1.
[System.Serializable]
public class Level1SaveData_Jack
{
    public static Level1SaveData_Jack current;
    public FirstPersonControllerSaveData_Jack playerSaveData;

    public float tablet1xPos;
    public float tablet1yPos;
    public float tablet1zPos;
    public float tablet1xRot;
    public float tablet1yRot;
    public float tablet1zRot;
    public int tablet1Layer;

    public float tablet2xPos;
    public float tablet2yPos;
    public float tablet2zPos;
    public float tablet2xRot;
    public float tablet2yRot;
    public float tablet2zRot;
    public int tablet2Layer;

    public float tablet3xPos;
    public float tablet3yPos;
    public float tablet3zPos;
    public float tablet3xRot;
    public float tablet3yRot;
    public float tablet3zRot;
    public int tablet3Layer;

    public float doorXPos;
    public float doorYPos;
    public float doorZPos;
    public bool doorOpen;

    public bool tabletSlot1HoldingTablet;
    public string tabletSlot1HeldTabletName;
    public Orientation tabletSlot1TabletOrientation;

    public bool tabletSlot2HoldingTablet;
    public string tabletSlot2HeldTabletName;
    public Orientation tabletSlot2TabletOrientation;

    public bool tabletSlot3HoldingTablet;
    public string tabletSlot3HeldTabletName;
    public Orientation tabletSlot3TabletOrientation;

}
