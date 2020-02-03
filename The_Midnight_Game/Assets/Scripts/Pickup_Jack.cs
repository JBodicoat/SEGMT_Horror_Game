// Jack

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public enum ItemType
{
    matches,
    salt,
    sizeOf
}

/// Used for pickup items.
/// 
/// Attatch this script to any items that can be picked up by the player.
/// Assign the itemType and quantity of the item.
/// The item must have a trigger collider.
public class Pickup_Jack : MonoBehaviour
{
    public ItemType itemType;
    public ushort quantity;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<FirstPersonController_Jack>().AddItems(itemType, quantity);
            Destroy(gameObject);
        }
    }
}
