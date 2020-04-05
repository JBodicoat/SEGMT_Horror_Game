// Jack : 02/03/2020 Created script
// Jack 23/03/2020 Added saving support

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the placement of bottles infront of the flamable wooden panel for the "Burning the way" puzzle.
/// </summary>
public class BottlePlacementManager_Jack : MonoBehaviour
{
    private const ushort maxBottles = 6;
    private ushort currentNumBottles = 0;

    private const float burnDuration = 5.0f;
    private bool burning = false;

    public ParticleSystem fireParticleSystem;
    public GameObject woodenPanelObject;

    private Inventory_Jack inventoryScript;

    private void Start()
    {
        inventoryScript = FindObjectOfType<Inventory_Jack>();
    }

    // Placements
    public GameObject[] placedBottles = new GameObject[maxBottles];

    /// <summary>
    /// Returns true if all the bottles needed have been placed.
    /// </summary>
    /// <returns></returns>
    public bool AllBottlesPlaced()
    {
        return (currentNumBottles >= maxBottles);
    }

    /// <summary>
    /// Places all the players bottles in front of the panel.
    /// If enough bottles have already been placed, a match is used to set the bottles on fire.
    /// </summary>
    public void Interact()
    {
        if (currentNumBottles < maxBottles)
        {
            PlaceBottles();
        }
        else if(!burning)
        {
            StartCoroutine(StartFire());
        }
    }

    /// <summary>
    /// Enables the corresponding number of bottle objects and removes them from the players inventory.
    /// </summary>
    private void PlaceBottles()
    {
        ushort numToPlace = inventoryScript.GetNumOf(ItemType.bottles);

        for (int i = currentNumBottles; i < currentNumBottles + numToPlace; ++i)
        {
            placedBottles[i].SetActive(true);
        }

        currentNumBottles += numToPlace;
        inventoryScript.RemoveItems(ItemType.bottles, numToPlace);
    }

    /// <summary>
    /// Returns currentNumBottles.
    /// </summary>
    /// <returns></returns>
    public int GetNumPlacedBottles()
    {
        return currentNumBottles;
    }

    /// <summary>
    /// Enables the passed number of bottle objects.
    /// </summary>
    /// <param name="numPlaced"></param>
    public void SetPlacedBottles(int numPlaced)
    {
        for (int i = 0; i < numPlaced; ++i)
        {
            placedBottles[i].SetActive(true);
        }
        currentNumBottles = (ushort)numPlaced;
    }

    /// <summary>
    /// Returns burning.
    /// </summary>
    /// <returns></returns>
    public bool IsBurning()
    {
        return burning;
    }

    /// <summary>
    /// A coroutine that sets the wooden panel on fire and after a set time destroying it.
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartFire()
    {
        inventoryScript.RemoveItems(ItemType.matches, 1);
        fireParticleSystem.Play();
        burning = true;

        yield return new WaitForSeconds(burnDuration);
        
        Destroy(woodenPanelObject);
    }
}
