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
    public CapsuleCollider playerCapsuleCollider;

    public CraftingMainScript craftingHandlerInPlayer;
    public GameObject InventoryHandler;
    public Inventory inventory;
    
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
        playerCapsuleCollider = GetComponent<CapsuleCollider>();

        craftingHandlerInPlayer = GameObject.Find("CraftingManager").GetComponent<CraftingMainScript>();
        inventory = GetComponent<Inventory>();

        playerWeaponHandler.ActivatePistol_();
        playerWeaponHandler.WeaponManager(playerWeaponHandler.weaponEquipped);
    }

    void Update()
    {
        // roll
        if(Input.GetKeyDown(KeyCode.Space) && !isRolling && !petHandler.craftingWindowOpen)
        {
            if(playerMovement.rollCount < playerMovement.maxRoll)
            {
                isRolling = true;
                playerCapsuleCollider.enabled = false;
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
                    if(petHandler.craftingWindowOpen)
                    {
                        petHandler.CloseCraftingWindow();
                    }
                    else if(!petHandler.craftingWindowOpen)
                    {
                        gameManager.PauseGame();
                    }
                }
                else if(gameManager.pausedGame)
                {
                    gameManager.ResumeGame();
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(petHandler.playerOnArea)
            {
                petHandler.OpenCraftingWindow();
                craftingHandlerInPlayer.ShowCraftOptions();
            }
        }

        // normal behaviour quando O JOGO NÃO ESTÁ PAUSADO
        if(!gameManager.pausedGame && !petHandler.craftingWindowOpen)
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

        if(!petHandler.craftingWindowOpen)
        {
            playerWeaponHandler.SwitchGuns();
        }
    }

    void LateUpdate()
    {
        petHandler.HandlePet();
    }

    void FixedUpdate()
    {
        if(!petHandler.craftingWindowOpen)
        {
            playerMovement.HandleMovement();   
            playerMovement.Move();
        }
    }
}
