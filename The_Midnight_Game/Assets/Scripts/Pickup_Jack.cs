// Jack
// Jack : 05/02/2020 cached player script & changed to CompareTag
// Jack 23/03/2020 Changed implementation for pickup upon interaction.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// Attatch this script to any items that can be picked up by the player.
/// Assign the itemType and quantity of the item.
/// The item must have a trigger collider.
/// </summary>
public class Pickup_Jack : MonoBehaviour
{
    public ItemType itemType;
    public ushort quantity;

    private Inventory_Jack playerInventoryScript;
    private const string playerTag = "Player";

    private void Start()
    {
        playerInventoryScript = FindObjectOfType<Inventory_Jack>();
    }

    /// <summary>
    /// Add this item to the player's inventory and destroy this object.
    /// </summary>
    public void Pickup()
    {
        playerInventoryScript.AddItems(itemType, quantity);
        Destroy(gameObject);
    }
}
