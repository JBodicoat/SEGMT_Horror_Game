// Jack 16/02/2020 - Abstracted inventory system from FirstPersonController

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    matches,
    salt,
    dolls,
    bottles,
    item5,
    item6,
    item7,
    item8,
    item9,
    item10,
    item11,
    item12,
    sizeOf
}


/// <summary>
/// Contains inventory logic and data. Is used for the players inventory.
/// </summary>
public class Inventory_Jack : MonoBehaviour
{
    public InventoryMenu_Jack inventoryMenuScript;
    private ushort[] inventory = new ushort[(ushort)ItemType.sizeOf];

    private void Awake()
    {
        inventory[(ushort)ItemType.matches] = 9999;
        inventory[(ushort)ItemType.salt] = 2;
    }

    private void Start()
    {
        if (!inventoryMenuScript)
        {
            inventoryMenuScript = FindObjectOfType<InventoryMenu_Jack>();
        }
        inventoryMenuScript.UpdateItem(ItemType.matches, 3);
        inventoryMenuScript.UpdateItem(ItemType.salt, 2);

    }

    /// Increases the number of passed itemType by passed quantity.
    /// <param name="itemType"></param>
    /// <param name="quantity"></param>
    public void AddItems(ItemType itemType, ushort quantity)
    {
        ushort index = (ushort)itemType;
        inventory[index] += quantity;
        inventoryMenuScript.UpdateItem(itemType, inventory[index]);
    }

    /// Decreases the number of passed itemType by passed quantity.
    /// <param name="itemType"></param>
    /// <param name="quantity"></param>
    public void RemoveItems(ItemType itemType, ushort quantity)
    {
        ushort index = (ushort)itemType;
        inventory[index] -= quantity;
        inventoryMenuScript.UpdateItem(itemType, inventory[index]);
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

    /// <summary>
    /// Replaces the entire inventory and updates the inventory menu.
    /// </summary>
    /// <param name="newInventory"></param>
    public void SetInventory(ushort[] newInventory)
    {
        inventory = newInventory;
        for(ushort i = 0; i < inventory.Length; ++i)
        {
            inventoryMenuScript.UpdateItem((ItemType)i, inventory[i]);
        }
    }
}
