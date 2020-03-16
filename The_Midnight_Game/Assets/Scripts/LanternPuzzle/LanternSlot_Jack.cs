// Jack 16/03/2020 Created script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the activation of the lantern upon collision with the slot.
/// </summary>
public class LanternSlot_Jack : MonoBehaviour
{
    private const string moveableLanternTag = "MoveableLantern";

    private bool triggered = false;

    public GameObject lantern;
    private Rigidbody lanternRigidbody;
    private ParticleSystem lanternParticleSystem;

    private const RigidbodyConstraints lanternConstraints = RigidbodyConstraints.FreezeAll;

    private const float minDistance = 0.001f;
    private const float lanternMoveSpeed = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        lanternRigidbody = lantern.GetComponent<Rigidbody>();
        lanternParticleSystem = lantern.GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        if(triggered)
        {
            Vector3 direction = new Vector3(transform.position.x - lantern.transform.position.x, 0, transform.position.z - lantern.transform.position.z);

            float sqrDistance = direction.x * direction.x + direction.z * direction.z;
            if (sqrDistance <= minDistance)
            {
                // Set the lantern alight
                lanternRigidbody.constraints = lanternConstraints;
                lanternParticleSystem.Play();
                Destroy(this);
            }
            else
            {
                direction.Normalize();
                lanternRigidbody.velocity = direction * lanternMoveSpeed;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!triggered && other.CompareTag(moveableLanternTag))
        {
            triggered = true;
        }
    }
}
