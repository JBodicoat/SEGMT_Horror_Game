// Jack
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public enum Orientation
{
    right,
    down,
    left,
    up,
    sizeOf
}

/// Holds tablet objects in place.
/// 
/// Used for the first puzzle. Holds tablets in place when they collide and
/// allows the player to rotate the tablets. When all the tablets are in the
/// correct orientation the door opens.
public class TabletSlot_Jack : MonoBehaviour
{
    public Door_Jack doorScript;

    private const string tabletTag = "Tablet";
    private bool holdingTablet = false;
    private GameObject heldTablet = null;
    private FirstPersonController_Jack playerScript;
    private LayerMask interactableLayer;
    private const RigidbodyConstraints tabletConstraints = RigidbodyConstraints.FreezeAll;
    private Orientation tabletOrientation = Orientation.right;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<FirstPersonController_Jack>();
        interactableLayer = LayerMask.GetMask("Interactable Objects");
    }

    /// Returns holdingTablet.
    public bool IsHoldingTablet()
    {
        return holdingTablet;
    }

    /// Rotates the held tablet 90 degrees clockwise.
    public void RotateTablet()
    {
        if(heldTablet)
        {
            if(++tabletOrientation == Orientation.sizeOf)
            {
                tabletOrientation = 0;
            }

            switch(tabletOrientation)
            {
                case Orientation.right:
                    heldTablet.transform.rotation = Quaternion.Euler(0, 0, 0);
                    break;

                case Orientation.down:
                    heldTablet.transform.rotation = Quaternion.Euler(0, 90, 0);
                    break;

                case Orientation.left:
                    heldTablet.transform.rotation = Quaternion.Euler(0, 180, 0);
                    break;

                case Orientation.up:
                    heldTablet.transform.rotation = Quaternion.Euler(0, 270, 0);
                    break;
            }

            doorScript.CheckSlots();
        }
    }

    /// Returns the orientation of the held tablet.
    public Orientation GetOrientation()
    {
        return tabletOrientation;
    }

    /// Holds the tablet into place on collision.
    private void OnTriggerEnter(Collider other)
    {
        if(!holdingTablet && other.CompareTag(tabletTag))
        {
            holdingTablet = true;
            heldTablet = other.gameObject;

            if(playerScript.GetHeldObject() == heldTablet)
            {
                playerScript.DropObject();
            }

            heldTablet.layer = interactableLayer;
            heldTablet.GetComponent<Rigidbody>().constraints = tabletConstraints;

            // Fix tablet in place
            heldTablet.transform.position = transform.position + Vector3.up * 0.6f;
            heldTablet.transform.rotation = Quaternion.Euler(0, 0, 0);

            doorScript.CheckSlots();
        }
    }
}
