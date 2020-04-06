// Jack 06/04/2020 - Created script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Adds the OnHoverGlow_Dan script to all objects on the "Moveable Object" and "Interactable Object" layers.
/// </summary>
public class ApplyGlowShaderScripts_Jack : MonoBehaviour
{
    private int moveableObjectsLayer;
    private int interactableObjectsLayer;

    // Start is called before the first frame update
    void Start()
    {
        moveableObjectsLayer = LayerMask.NameToLayer("Moveable Object");
        interactableObjectsLayer = LayerMask.NameToLayer("Interactable Object");

        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        List<GameObject> interactableObjects = new List<GameObject>();

        for (int i = 0; i < gameObjects.Length; ++i)
        {
            if (gameObjects[i].layer == moveableObjectsLayer || gameObjects[i].layer == interactableObjectsLayer)
            {
                interactableObjects.Add(gameObjects[i]);
            }
        }

        foreach(GameObject interactableObject in interactableObjects)
        {
            interactableObject.AddComponent<OnHoverGlow_Dan>();
        }
    }
}
