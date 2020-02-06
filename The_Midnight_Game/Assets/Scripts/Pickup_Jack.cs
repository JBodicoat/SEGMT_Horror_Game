// Jack
// Jack : 05/02/2020 cached player script & changed to CompareTag
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

    private FirstPersonController_Jack playerScript;
    private const string playerTag = "Player";

    private void Start()
    {
        playerScript = FindObjectOfType<FirstPersonController_Jack>();
    }

    /// On collision with player, add to player's inventory and destroy self.
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            playerScript.AddItems(itemType, quantity);
            Destroy(gameObject);
        }
    }
}
