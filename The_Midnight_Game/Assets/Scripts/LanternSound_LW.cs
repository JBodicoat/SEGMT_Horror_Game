// Louie 07/04/2020 - Created script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternSound_LW : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private float SpeedMinimum;
    private AudioSource stoneSound;
    public AudioClip fireLitSound;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        SpeedMinimum = 0.01f;
        stoneSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.x > SpeedMinimum || rb.velocity.y > SpeedMinimum || rb.velocity.x < -SpeedMinimum || rb.velocity.y < -SpeedMinimum)
        {
            if (!stoneSound.isPlaying)
            {
                stoneSound.Play();
                print("Play");
            }
        }
        else
        {
            if (stoneSound.isPlaying)
            {
                stoneSound.Pause();
                print("Pause");
            }
        }
    }
    /// <summary>
    /// Plays the fire lit sound when the puzzle is complete and the fire is lit.
    /// </summary>
    public void PlayFireLitSound()
    {
        stoneSound.clip = fireLitSound;
    }
}
