// Jack 16/02/2020 - Abstracted inventory system from FirstPersonController
// Louie 24/03/2020 - Added functionality with Journal UI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    matches,
    salt,
    dolls,
    bottles,
    clockKey,
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
    private InventoryMenu_Jack inventoryMenuScript;
    private ushort[] inventory = new ushort[(ushort)ItemType.sizeOf];

    public JournalManager_Louie journalScript;
    private void Awake()
    {
        inventory[(ushort)ItemType.matches] = 9999;
        inventory[(ushort)ItemType.salt] = 2;
        inventoryMenuScript = FindObjectOfType<InventoryMenu_Jack>();
    }

    private void Start()
    {
        inventoryMenuScript.UpdateItem(ItemType.matches, 3);
        inventoryMenuScript.UpdateItem(ItemType.salt, 99);

        journalScript.UpdateValues(GetNumOf(ItemType.matches), GetNumOf(ItemType.salt), GetNumOf(ItemType.bottles), GetNumOf(ItemType.dolls));
    }

    /// Increases the number of passed itemType by passed quantity.
    /// <param name="itemType"></param>
    /// <param name="quantity"></param>
    public void AddItems(ItemType itemType, ushort quantity)
    {
        ushort index = (ushort)itemType;
        inventory[index] += quantity;
        inventoryMenuScript.UpdateItem(itemType, inventory[index]);

        journalScript.UpdateValues(GetNumOf(ItemType.matches), GetNumOf(ItemType.salt), GetNumOf(ItemType.bottles), GetNumOf(ItemType.dolls));
    }

    /// Decreases the number of passed itemType by passed quantity.
    /// <param name="itemType"></param>
    /// <param name="quantity"></param>
    public void RemoveItems(ItemType itemType, ushort quantity)
    {
        ushort index = (ushort)itemType;
        inventory[index] -= quantity;
        inventoryMenuScript.UpdateItem(itemType, inventory[index]);

        journalScript.UpdateValues(GetNumOf(ItemType.matches), GetNumOf(ItemType.salt), GetNumOf(ItemType.bottles), GetNumOf(ItemType.dolls));
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
