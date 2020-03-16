// Jack 16/03/2020 Created script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlls individual valves.
/// </summary>
public class Valve_Jack : MonoBehaviour
{
    private ValveController_Jack valveController;

    public GameObject valveLightObject;

    public Valve_Jack leftValve;
    public Valve_Jack rightValve;

    public Material lightOnMaterial;
    public Material lightOffMaterial;
    private Renderer lightRenderer;

    public Animator animator;
    private bool turning = false;

    private bool lightOn = false;

    // Start is called before the first frame update
    void Start()
    {
        valveController = FindObjectOfType<ValveController_Jack>();
        lightRenderer = valveLightObject.GetComponent<Renderer>();
    }

    /// <summary>
    /// Begins the valves turn animation.
    /// </summary>
    public void StartTurn()
    {
        if (!turning)
        {
            animator.enabled = true;
            turning = true;
        }
    }

    /// <summary>
    /// Toggles the valve and adjascent valve lights.
    /// Called by the valves animation trigger.
    /// </summary>
    public void EndTurn()
    {
        leftValve.SwitchLight();
        SwitchLight();
        rightValve.SwitchLight();

        valveController.CheckLights();

        animator.enabled = false;
        turning = false;
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
        if(lightOn)
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
}
