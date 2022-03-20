using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerManager : MonoBehaviour
{
    private _PlayerMovement playerMovement;
    private _AnimationHandler animationHandler;
    private _PlayerWeaponHandler playerWeaponHandler;
    private PetHandler petHandler;

    [Header("Player Flags")]
    public bool isShooting;
    public bool isRolling;
    // public bool isOnCombat; à implementar no futuro, para travar a interação com a airfryer pra somente quando terminar a batalha (?)

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<_PlayerMovement>();
        animationHandler = GetComponent<_AnimationHandler>();   
        playerWeaponHandler = GetComponent<_PlayerWeaponHandler>();
        petHandler = GetComponent<PetHandler>();

        playerWeaponHandler.ActivatePistol();
    }

    // Update is called once per frame
    void Update()
    {
        playerWeaponHandler.WeaponBehaviour();

        if(Input.GetKeyDown(KeyCode.Space) && !isRolling)
        {
            if(playerMovement.rollCount < playerMovement.maxRoll)
            {
                isRolling = true;
                playerMovement.rollTimer = playerMovement.rollDuration;
                playerMovement.rollCount++;
            }
        }
        playerMovement.RollCountTimer();
        playerMovement.PlayerAim();
    }

    void LateUpdate()
    {
        petHandler.HandlePet();
    }

    void FixedUpdate()
    {
        playerMovement.HandleMovement();   
        playerMovement.Move();
    }
}