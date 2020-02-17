// Jack : 16/02/2020 Created Script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

/// <summary>
/// Handles effects resulting from the Midnight Man being near the player.
/// </summary>
/// All effects are only triggered when the Midnight Man is in the same room.
/// Creates a vignette with opacity based on distance to the Midnight Man.
/// Activates a whispering sound effect when the Midnight Man is within range.
public class ProximityAlerts_Jack : MonoBehaviour
{
    // Vignette
    public PostProcessVolume m_Volume;
    public MMTargeting_Morgan mmTargetingScript;
    private Vignette proximityVignette;

    private const float minVignetteDistanceSquared = 400f;

    private FloatParameter opacity = new FloatParameter();
    private float targetOpacity;

    private bool fadingIn = false;
    private bool fadedIn = false;

    private bool fadingOut = false;
    private bool fadedOut = false;

    private const float fadeInIncrement = 0.025f;
    private const float fadeOutIncrement = 0.0125f; 

    private readonly WaitForSeconds fadeWait = new WaitForSeconds(0.05f);

    // Whispers
    public AudioSource whispersAudioSource;
    private const float minWhispersDistanceSquared = 200f;

    // Breath
    public ParticleSystem breathParticleSystem;
    private const float minBreathDistanceSquared = 300f;
    private bool breathActive = false;
    private const float timeBetweenBreaths = 4f;
    private float breathTimer = 0f;

    // Start is called before the first frame update
    private void Start()
    {
        proximityVignette = FindObjectOfType<Vignette>();
        proximityVignette.opacity.Override(1);

        m_Volume = PostProcessManager.instance.QuickVolume(10, 100f, proximityVignette);

        opacity.value = 0;
        proximityVignette.opacity = opacity;
    }

    // Update is called once per frame
    void Update()
    {
        // Vignette
        if (!fadedOut 
            && !fadingOut
            && mmTargetingScript.distanceToPlayerSquared > minVignetteDistanceSquared 
            && proximityVignette.opacity > 0)
        {
            StartCoroutine(FadeOut());
        }
        else if(fadedIn)
        {
            opacity.value = 1 - (mmTargetingScript.distanceToPlayerSquared / minVignetteDistanceSquared);
            proximityVignette.opacity = opacity;
        }
        else if(fadingIn)
        {
            targetOpacity = 1 - (mmTargetingScript.distanceToPlayerSquared / minVignetteDistanceSquared);
        }
        else if (!fadingIn && mmTargetingScript.isWithPlayer && mmTargetingScript.distanceToPlayerSquared <= minVignetteDistanceSquared)
        {
            targetOpacity = 1 - (mmTargetingScript.distanceToPlayerSquared / minVignetteDistanceSquared);
            StartCoroutine(FadeIn());
        }

        // Whispers
        if(!whispersAudioSource.isPlaying && mmTargetingScript.isWithPlayer && mmTargetingScript.distanceToPlayerSquared <= minWhispersDistanceSquared)
        {
            whispersAudioSource.Play();
        }
        else if(whispersAudioSource.isPlaying 
                && (!mmTargetingScript.isWithPlayer || mmTargetingScript.distanceToPlayerSquared > minWhispersDistanceSquared))
        {
            whispersAudioSource.Stop();
        }

        // Breath
        if(!breathActive && mmTargetingScript.isWithPlayer && mmTargetingScript.distanceToPlayerSquared <= minBreathDistanceSquared)
        {
            breathActive = true;
            breathTimer = 0f;
            breathParticleSystem.Play(false);
        }

        if(breathActive)
        {
            if(!mmTargetingScript.isWithPlayer || mmTargetingScript.distanceToPlayerSquared > minBreathDistanceSquared)
            {
                breathActive = false;
            }
            else
            {
                // Continue breaths
                breathTimer += Time.deltaTime;
                if(breathTimer >= timeBetweenBreaths)
                {
                    breathTimer = 0f;
                    breathParticleSystem.Play(false);
                }
            }
        }
    }

    /// <summary>
    /// Fades in the vignette.
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeIn()
    {
        fadingOut = false;
        fadedOut = false;

        fadingIn = true;

        opacity.value = proximityVignette.opacity;
        do
        {
            if((opacity.value += fadeInIncrement) > 1)
            {
                opacity.value = 1;
            }
            proximityVignette.opacity = opacity;

            yield return fadeWait;

            if(Mathf.Abs(opacity - targetOpacity) <= 0.05f)
            {
                fadedIn = true;
            }
        } while (!fadedIn);

        fadingIn = false;
    }

    /// <summary>
    /// Fades out the vignette.
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeOut()
    {
        fadingIn = false;
        fadedIn = false;

        fadingOut = true;

        opacity.value = proximityVignette.opacity;
        do
        {
            if(!fadingOut)
            {
                break;
            }

            if((opacity.value -= fadeOutIncrement) < 0)
            {
                opacity.value = 0;
            }
            proximityVignette.opacity = opacity;

            yield return fadeWait;

            opacity = proximityVignette.opacity;
        } while (proximityVignette.opacity.value > 0);

        if (fadingOut)
        {
            fadedIn = false;
            fadedOut = true;
        }

        fadingOut = false;
    }
}
