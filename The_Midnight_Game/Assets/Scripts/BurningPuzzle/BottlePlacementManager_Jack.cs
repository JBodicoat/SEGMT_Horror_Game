// Jack : 02/03/2020 Created script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void PlaceBottles()
    {
        ushort numToPlace = inventoryScript.GetNumOf(ItemType.bottles);

        for (int i = currentNumBottles; i < currentNumBottles + numToPlace; i++)
        {
            placedBottles[i].SetActive(true);
        }

        currentNumBottles += numToPlace;
        inventoryScript.RemoveItems(ItemType.bottles, numToPlace);
    }

    private IEnumerator StartFire()
    {
        inventoryScript.RemoveItems(ItemType.matches, 1);
        fireParticleSystem.Play();
        burning = true;

        yield return new WaitForSeconds(burnDuration);
        
        Destroy(woodenPanelObject);
    }
}
