using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternSound_LW : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private float SpeedMinimum;
    public AudioSource stoneSound;
    public AudioClip fireSound;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        SpeedMinimum = 0.01f;
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

    public void PlayFireLitSound()
    {
        stoneSound.clip = fireSound;
    }
}
