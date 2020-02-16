// Jack 16/02/2020 - Abstracted salt system from FirstPersonController

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltPouring_Jack : MonoBehaviour
{
    public GameObject saltCirclePrefab;
    public Animator animator;
    public ParticleSystem saltParticleSystem;
    public CharacterController characterController;

    private Inventory_Jack inventoryScript;
    private float halfPlayerHeight;
    private bool inSaltCircle = false;
    private bool pouringSalt = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if(!characterController)
        {
            characterController = GetComponent<CharacterController>();
        }

        halfPlayerHeight = characterController.height / 2f;

        if (!animator)
        {
            animator = GetComponent<Animator>();
        }

        inventoryScript = FindObjectOfType<Inventory_Jack>();
    }

    /// Attempts to pour a salt circle.
    /// 
    /// If the player has any salt it will be used to start pouring
    /// a salt circle.
    /// <returns>Returns false if the player has no salt.</returns>
    public bool PourSaltCirlce()
    {
        if (inventoryScript.GetNumOf(ItemType.salt) > 0 && characterController.isGrounded)
        {
            inventoryScript.RemoveItems(ItemType.salt, 1);
            pouringSalt = true;
            animator.enabled = true;
            saltParticleSystem.Play(false);
            return true;
        }

        return false;
    }

    /// Creates a salt circle at the players feet. 
    /// This is called at the end of the salt pouring animation.
    public void InstantiateSaltCircle()
    {
        Instantiate(saltCirclePrefab,
                    new Vector3(transform.position.x, transform.position.y - halfPlayerHeight, transform.position.z),
                    Quaternion.identity);
        inSaltCircle = true;
        pouringSalt = false;
        animator.enabled = false;
        saltParticleSystem.Stop(false);
    }

    /// Sets inSaltCircle to the passed value.
    /// <param name="isInSaltCircle"></param>
    public void SetInSaltCircle(bool isInSaltCircle)
    {
        inSaltCircle = isInSaltCircle;
    }

    /// Returns inSaltCircle.
    public bool IsInSaltCircle()
    {
        return inSaltCircle;
    }

    /// <summary>
    /// Returns pouringSalt.
    /// </summary>
    /// <returns></returns>
    public bool IsPouringSalt()
    {
        return pouringSalt;
    }
}
