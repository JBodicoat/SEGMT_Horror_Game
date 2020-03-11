// Jack
// Jack 13/02/2020 Added support for saving data.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Used for the first puzzle. 
/// 
/// When all the tablets are in the correct orientation the door opens.
public class TabletDoor_Jack : MonoBehaviour
{
    private readonly Vector3 moveAmount = new Vector3(0, -4, 0);
    
    public TabletSlot_Jack tabletSlot1Script;
    public TabletSlot_Jack tabletSlot2Script;
    public TabletSlot_Jack tabletSlot3Script;

    private bool open = false;

    /// Tests if the tablets are orientated correctly to open the door.
    /// 
    /// Tests if the tablets in the tabletSlots are orientated correctly to open the door.
    /// If they are then the door opens.
    public void CheckSlots()
    {
        if (!open)
        {
            if (tabletSlot1Script.IsHoldingTablet() && tabletSlot1Script.GetOrientation() == Orientation.down)
            {
                if (tabletSlot2Script.IsHoldingTablet() && tabletSlot2Script.GetOrientation() == Orientation.left)
                {
                    if (tabletSlot3Script.IsHoldingTablet() && tabletSlot3Script.GetOrientation() == Orientation.up)
                    {
                        // Open door
                        transform.position -= moveAmount;
                        open = true;
                    }
                }
            }
        }
    }

    /// Returns open.
    public bool IsOpen()
    {
        return open;
    }

    /// Sets open to the passed value.
    public void SetOpen(bool newOpen)
    {
        open = newOpen;
    }
}
