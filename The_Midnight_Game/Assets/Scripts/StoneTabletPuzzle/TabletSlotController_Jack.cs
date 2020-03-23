// Jack
// Jack 13/02/2020 Added support for saving data.
// Jack 23/03/2020 Removed the door.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used for the first puzzle. 
/// When all the tablets are in the correct orientation the door opens.
/// </summary>
public class TabletSlotController_Jack : MonoBehaviour
{   
    public TabletSlot_Jack tabletSlot1Script;
    public TabletSlot_Jack tabletSlot2Script;
    public TabletSlot_Jack tabletSlot3Script;

    private bool puzzleSolved = false;

    /// Tests if the tablets are orientated correctly to open the door.
    /// 
    /// Tests if the tablets in the tabletSlots are orientated correctly to open the door.
    /// If they are then the door opens.
    public void CheckSlots()
    {
        if (!puzzleSolved)
        {
            if (tabletSlot1Script.IsHoldingTablet() && tabletSlot1Script.GetOrientation() == Orientation.down)
            {
                if (tabletSlot2Script.IsHoldingTablet() && tabletSlot2Script.GetOrientation() == Orientation.left)
                {
                    if (tabletSlot3Script.IsHoldingTablet() && tabletSlot3Script.GetOrientation() == Orientation.up)
                    {
                        puzzleSolved = true;
                    }
                }
            }
        }
    }

    /// Returns open.
    public bool IsPuzzleSolved()
    {
        return puzzleSolved;
    }

    /// Sets open to the passed value.
    public void SetOpen(bool newOpen)
    {
        puzzleSolved = newOpen;
    }
}
