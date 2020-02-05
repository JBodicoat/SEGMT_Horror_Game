// Jack

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Used for the first puzzle. 
/// 
/// When all the tablets are in the correct orientation the door opens.
public class Door_Jack : MonoBehaviour
{
    private readonly Vector3 moveAmount = new Vector3(0, -4, 0);
    
    public TabletSlot_Jack tabletSlot1Script;
    public TabletSlot_Jack tabletSlot2Script;
    public TabletSlot_Jack tabletSlot3Script;

    public void CheckSlots()
    {
        if(tabletSlot1Script.IsHoldingTablet() && tabletSlot1Script.GetOrientation() == Orientation.down)
        {
            if(tabletSlot2Script.IsHoldingTablet() && tabletSlot2Script.GetOrientation() == Orientation.left)
            {
                if(tabletSlot3Script.IsHoldingTablet() && tabletSlot3Script.GetOrientation() == Orientation.up)
                {
                    // Open door
                    transform.position -= moveAmount;
                }
            }
        }
    }
}
