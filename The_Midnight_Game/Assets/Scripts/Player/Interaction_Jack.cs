﻿// Jack 16/02/2020 - Abstracted salt system from FirstPersonController

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Jack : MonoBehaviour
{
    public Camera playerCamera;
    public CharacterController characterController;

    // Picking up objects
    private readonly LayerMask moveableObjectsLayer = 1 << 8;
    private const ushort interactDistance = 10;
    private GameObject heldObject = null;
    private Collider heldObjectCollider = null;
    private Rigidbody heldObjectRigidbody = null;
    private const RigidbodyConstraints freezeRotationConstraints = RigidbodyConstraints.FreezeRotation;
    private RigidbodyConstraints heldObjectConstraints = RigidbodyConstraints.None;
    private const float dropObjectDist = 20f;
    private const float objectMoveDeadZone = 0.3f;
    private const float objectMoveSpeed = 8f;
    private readonly Vector3 zeroVector3 = new Vector3(0, 0, 0);

    // Throwing objects
    private const float throwForce = 15f;

    // Interactable objects
    private readonly LayerMask interactableObjectsLayer = 1 << 9;

    // Start is called before the first frame update
    void Start()
    {
        if(!playerCamera)
        {
            playerCamera = GetComponent<Camera>();
        }

        if(!characterController)
        {
            characterController = GetComponent<CharacterController>();
        }
    }

    void FixedUpdate()
    {
        // Held object
        if (heldObject)
        {
            Vector3 targetPosition = playerCamera.transform.position + playerCamera.transform.forward * 2f;
            Vector3 distanceToTarget = targetPosition - heldObject.transform.position;
            if (distanceToTarget.sqrMagnitude > dropObjectDist)
            {
                DropObject();
            }
            else if (distanceToTarget.sqrMagnitude > objectMoveDeadZone)
            {
                distanceToTarget.Normalize();
                heldObjectRigidbody.velocity = distanceToTarget * objectMoveSpeed;
            }
            else
            {
                heldObjectRigidbody.velocity = zeroVector3;
            }
        }
    }

    /// Interacts with anything in front of the player.
    /// 
    /// Fires a ray forward from the camera. If it hits an interactable object
    /// it will be activated.
    public void Interact()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactDistance, interactableObjectsLayer))
        {
            if (hit.transform.CompareTag("Tablet Slot"))
            {
                hit.transform.gameObject.GetComponent<TabletSlot_Jack>().RotateTablet();
            }
        }
    }

    /// Picks up an object in front of the player.
    /// 
    /// Fires a ray from the camera in front of the player. If the ray hits an
    /// object that can be held it will be picked up.
    public void PickupObject()
    {
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactDistance, Color.red, 2f);
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, interactDistance, moveableObjectsLayer))
        {
            heldObject = hit.transform.gameObject;

            heldObjectCollider = heldObject.GetComponent<Collider>();
            Physics.IgnoreCollision(heldObjectCollider, characterController);

            heldObjectRigidbody = heldObject.GetComponent<Rigidbody>();
            heldObjectRigidbody.useGravity = false;
            heldObjectConstraints = heldObjectRigidbody.constraints;
            heldObjectRigidbody.constraints = freezeRotationConstraints;
        }
    }

    /// Drops the held object if there is one.
    public void DropObject()
    {
        heldObjectRigidbody.constraints = heldObjectConstraints;
        heldObjectRigidbody.useGravity = true;

        heldObjectRigidbody = null;

        Physics.IgnoreCollision(heldObjectCollider, characterController, false);
        heldObjectCollider = null;

        heldObject = null;
    }

    public void ThrowObject()
    {
        heldObjectRigidbody.AddForce(playerCamera.transform.forward * throwForce, ForceMode.Impulse);
        DropObject();
    }

    /// Returns heldObject.
    public GameObject GetHeldObject()
    {
        return heldObject;
    }
}
