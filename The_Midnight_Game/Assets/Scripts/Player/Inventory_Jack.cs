// Jack 16/02/2020 - Abstracted inventory system from FirstPersonController

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Jack : MonoBehaviour
{
    private ushort[] inventory = new ushort[(ushort)ItemType.sizeOf];

    private void Awake()
    {
        inventory[(ushort)ItemType.matches] = 3;
        inventory[(ushort)ItemType.salt] = 2;
    }

    /// Increases the number of passed itemType by passed quantity.
    /// <param name="itemType"></param>
    /// <param name="quantity"></param>
    public void AddItems(ItemType itemType, ushort quantity)
    {
        inventory[(ushort)itemType] += quantity;
    }

    /// Decreases the number of passed itemType by passed quantity.
    /// <param name="itemType"></param>
    /// <param name="quantity"></param>
    public void RemoveItems(ItemType itemType, ushort quantity)
    {
        inventory[(ushort)itemType] -= quantity;
    }

    /// <summary>
    /// Gets the number of items the player has of the passed type.
    /// </summary>
    /// <param name="itemType"></param>
    /// <returns></returns>
    public ushort GetNumOf(ItemType itemType)
    {
        return inventory[(ushort)itemType];
    }

    /// <summary>
    /// Returns the inventory.
    /// </summary>
    /// <returns></returns>
    public ushort[] GetInventory()
    {
        return inventory;
    }

    public void SetInventory(ushort[] newInventory)
    {
        inventory = newInventory;
    }
}
