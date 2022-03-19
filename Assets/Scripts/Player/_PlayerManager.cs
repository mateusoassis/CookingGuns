using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerManager : MonoBehaviour
{
    private _PlayerMovement playerMovement;
    private _AnimationHandler animationHandler;
    private _PlayerWeaponHandler playerWeaponHandler;

    [Header("Player Flags")]
    public bool isShooting;
    public bool isRolling;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<_PlayerMovement>();
        animationHandler = GetComponent<_AnimationHandler>();   
        playerWeaponHandler = GetComponent<_PlayerWeaponHandler>();

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

    void FixedUpdate()
    {
        playerMovement.HandleMovement();   
        playerMovement.Move();
    }
}
