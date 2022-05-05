using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAuxiliary : MonoBehaviour
{
    private _WeaponHandler playerWeaponHandler;
    private float newAnimationSpeed;
    private float currentRollDuration;
    private float currentAnimationSpeed;
    private float baseRollDuration = 0.2f;
    private float baseAnimationSpeed = 2.5f;
    private Animator anim;
    private _PlayerMovement playerMovement;

    void Awake()
    {
        playerWeaponHandler = GetComponentInParent<_WeaponHandler>();
        anim = GetComponent<Animator>();
        if(playerMovement == null)
        {
            playerMovement = GetComponentInParent<_PlayerMovement>();
        }
        currentAnimationSpeed = anim.GetFloat("rollSpeedMultiplier");
        currentRollDuration = playerMovement.rollDuration;
    }

    void Start()
    {
        
        gameObject.SetActive(false);
    }

    void Update()
    {
        if(currentRollDuration != baseRollDuration || currentAnimationSpeed != baseAnimationSpeed)
        {
            newAnimationSpeed = baseAnimationSpeed / (currentRollDuration/baseRollDuration);
            anim.SetFloat("rollSpeedMultiplier", newAnimationSpeed);
        }
    }

    public void DeactivateThisObject()
    {
        gameObject.SetActive(false);
    }
    public void OnDisable()
    {
        playerWeaponHandler.WeaponManager(playerWeaponHandler.weaponTypeEquipped);
    }
}
