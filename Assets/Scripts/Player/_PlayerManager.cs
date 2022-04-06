using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerManager : MonoBehaviour
{
    public _PlayerMovement playerMovement;
    public _AnimationHandler animationHandler;
    public _PlayerShooting playerShootingPistol;
    public _PlayerShooting playerShootingShotgun;
    public _PlayerShooting playerShootingMachineGun;
    public _WeaponHandler playerWeaponHandler;
    public GameManager gameManager;
    public PetHandler petHandler;
    public PlayerInfo playerInfo;

    
      

    [Header("Player Flags")]
    public bool isShooting;
    public bool isRolling;
    
    // public bool isOnCombat; à implementar no futuro, para travar a interação com a airfryer pra somente quando terminar a batalha (?)

    void Start()
    {
        playerShootingPistol = GameObject.Find("Pistol").GetComponent<_PlayerShooting>();
        playerShootingShotgun = GameObject.Find("Shotgun").GetComponent<_PlayerShooting>();
        playerShootingMachineGun = GameObject.Find("MachineGun").GetComponent<_PlayerShooting>();
        playerMovement = GetComponent<_PlayerMovement>();
        animationHandler = GetComponent<_AnimationHandler>();   
        playerWeaponHandler = GetComponent<_WeaponHandler>();
        petHandler = GetComponent<PetHandler>();

        //playerWeaponHandler.ActivatePistol();
        playerWeaponHandler.ActivatePistol_();
        playerWeaponHandler.WeaponManager(playerWeaponHandler.weaponEquipped);
    }

    void Update()
    {
        // roll
        if(Input.GetKeyDown(KeyCode.Space) && !isRolling)
        {
            if(playerMovement.rollCount < playerMovement.maxRoll)
            {
                isRolling = true;
                playerMovement.rollTimer = playerMovement.rollDuration;
                playerMovement.rollCount++;
                playerInfo.totalTimesRolled++;
            }
        }

        // pause
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameManager.confirmationWindowOpen)
            {
                gameManager.CloseAllConfirmationWindows();
            }
            else if(!gameManager.confirmationWindowOpen)
            {
                if(!gameManager.pausedGame)
                {
                    gameManager.PauseGame();
                }
                else if(gameManager.pausedGame)
                {
                    gameManager.ResumeGame();
                }
            }
            
        }

        // normal behaviour quando O JOGO NÃO ESTÁ PAUSADO
        if(!gameManager.pausedGame)
        {
            //playerWeaponHandler.WeaponBehaviour();
            if(playerWeaponHandler.weaponEquipped == 0)
            {
                playerShootingPistol.MyInput();
            }
            else if(playerWeaponHandler.weaponEquipped == 1)
            {
                playerShootingShotgun.MyInput();
            }
            else if(playerWeaponHandler.weaponEquipped == 2)
            {
                playerShootingMachineGun.MyInput();
            }
            
            playerMovement.RollCountTimer();
            playerMovement.PlayerAim();
        }

        playerWeaponHandler.SwitchGuns();
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
