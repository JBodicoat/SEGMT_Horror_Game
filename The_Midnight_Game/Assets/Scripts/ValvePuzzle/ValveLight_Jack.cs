// Jack 31/03/2020 Script created to support changes in Valve_Jack

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveLight_Jack : MonoBehaviour
{
    public Material lightOnMaterial;
    public Material lightOffMaterial;
    private Renderer lightRenderer;

    private bool lightOn = false;

    private void Awake()
    {
        lightRenderer = GetComponent<Renderer>();
        
        lightOn = (lightRenderer.material == lightOnMaterial);
    }

    /// <summary>
    /// Returns lightOn.
    /// </summary>
    /// <returns></returns>
    public bool IsLightOn()
    {
        return lightOn;
    }

    /// <summary>
    /// Alternates whether the valves associated light is on or off, changing its material.
    /// </summary>
    public void SwitchLight()
    {
        if (lightOn)
        {
            lightOn = false;
            lightRenderer.material = lightOffMaterial;
        }
        else
        {
            lightOn = true;
            lightRenderer.material = lightOnMaterial;
        }
    }

    /// <summary>
    /// If true is passed the valves light is turned on.
    /// </summary>
    /// <param name="newLightOn"></param>
    public void SetLightOn(bool newLightOn)
    {
        lightOn = newLightOn;
        if (lightOn)
        {
            lightRenderer.material = lightOnMaterial;
        }
    }
}
