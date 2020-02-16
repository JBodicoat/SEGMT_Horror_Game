// Jack 16/02/2020 - Abstracted candle system from FirstPersonController
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle_Jack : MonoBehaviour
{
    public GameObject candleFlame;
    public Light candlePrimaryLight;

    private Inventory_Jack inventoryScript;

    private void Start()
    {
        inventoryScript = FindObjectOfType<Inventory_Jack>();
    }

    /// Attempts to light the player's candle.
    /// <returns>Returns false if the candle was already lit or the player doesn't have enouch matches.</returns>
    public bool LightCandle()
    {
        if (!candleFlame.activeSelf && inventoryScript.GetNumOf(ItemType.matches) > 0)
        {
            candleFlame.SetActive(true);
            candlePrimaryLight.enabled = true;
            inventoryScript.RemoveItems(ItemType.matches, 1);
        }

        return false;
    }

    /// Disables the player's candles light source.
    /// <returns>Returns false if the candle was already extinguished.</returns>
    public bool ExtinguishCandle()
    {
        if (candleFlame.activeSelf)
        {
            candleFlame.SetActive(false);
            candlePrimaryLight.enabled = false;
            return true;
        }

        return false;
    }

    /// Returns whether the candle is lit.
    public bool IsCandleLit()
    {
        return candleFlame.activeSelf;
    }

    /// <summary>
    /// Directly sets the candle to be lit or unlit without using a match.
    /// </summary>
    /// <param name="candleLit"></param>
    public void SetCandleLit(bool candleLit)
    {
        candleFlame.SetActive(candleLit);
        candlePrimaryLight.enabled = candleLit;
    }
}
