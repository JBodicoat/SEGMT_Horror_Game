//Created by Dan 10/03/2020
// Jack 23/03/2020 Intergrated script with Interaction_Jack

using UnityEngine;

public class OnHoverGlow_Dan : MonoBehaviour
{
    public Shader glowShader;
    private Shader previousShader;

    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    /// <summary>
    /// Sets the objects shader to the glow shader.
    /// </summary>
    public void Glow()
    {
        previousShader = objectRenderer.material.shader;
        objectRenderer.material.shader = glowShader;
    }

    /// <summary>
    /// Sets the objects shader back to it's original.
    /// </summary>
    public void StopGlow()
    {
        objectRenderer.material.shader = previousShader;
    }
}
