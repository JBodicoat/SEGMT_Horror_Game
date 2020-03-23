// Jack
// Jack 13/02/2020 - Added saving of tablet puzzle, player's rotation & candle
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/// Stores the save data for level 1.
[System.Serializable]
public class LevelSaveData_Jack
{
    public static LevelSaveData_Jack current;
    public PlayerSaveData_Jack playerSaveData;

    // Stone Tablet Puzzle
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

    public bool tabletSlot1HoldingTablet;
    public string tabletSlot1HeldTabletName;
    public Orientation tabletSlot1TabletOrientation;

    public bool tabletSlot2HoldingTablet;
    public string tabletSlot2HeldTabletName;
    public Orientation tabletSlot2TabletOrientation;

    public bool tabletSlot3HoldingTablet;
    public string tabletSlot3HeldTabletName;
    public Orientation tabletSlot3TabletOrientation;

    // Piano Puzzle
    public bool pianoPuzzleSolved;

    // Clock Puzzle

    // Library Puzzle
    public bool libraryPuzzleSolved;

    public float book1LocalZPos;
    public float book2LocalZPos;
    public float book3LocalZPos;

    // Rabbit Puzzle

    // Safe Puzzle

    // Attic Puzzle
    public bool atticPuzzleSolved;
    
    public float ball1XPos;
    public float ball1YPos;
    public float ball1ZPos;

    public float ball2XPos;
    public float ball2YPos;
    public float ball2ZPos;

    public float ball3XPos;
    public float ball3YPos;
    public float ball3ZPos;

    // Valve Puzzle
    public bool valvePuzzleSolved;

    public bool valve1LightOn;
    public bool valve2LightOn;
    public bool valve3LightOn;
    public bool valve4LightOn;
    public bool valve5LightOn;

    // Burning Puzzle
    public bool woodenPanelDestroyed;

    public bool bottle1PickedUp;
    public bool bottle2PickedUp;
    public bool bottle3PickedUp;
    public bool bottle4PickedUp;
    public bool bottle5PickedUp;
    public bool bottle6PickedUp;

    public int bottlesPlaced;

    // Lantern Puzzle
    public bool lanternPuzzleSolved;

    public float lanternXPos;
    public float lanternZPos;
}
