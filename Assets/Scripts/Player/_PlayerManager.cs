using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _PlayerManager : MonoBehaviour
{
    public bool testing;
    public Rigidbody playerRigidbody;
    public _AnimationHandler animationHandler;
    public _PlayerStats playerStats;
    public _PlayerMovement playerMovement;
    public _PlayerShooting playerShootingPistol;
    public _PlayerShooting playerShootingShotgun;
    public _PlayerShooting playerShootingMachineGun;
    public _PlayerShooting playerShootingGranadeLauncher;
    public _WeaponHandler playerWeaponHandler;
    public GameManager gameManager;
    public PetHandler petHandler;
    public PlayerInfo playerInfo;
    public CapsuleCollider playerCapsuleCollider;

    public CraftingMainScript craftingHandlerInPlayer;
    public GameObject InventoryHandler;
    public Inventory inventory;
    public GameFadeout gameFadeOut;

    public int sceneIndex;

    
    [Header("Player Flags")]
    public bool isShooting;
    public bool isRolling;
    public bool isFading;
    public bool isWalking;

    void Awake()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerInfo.currentSceneIndex = sceneIndex;
    }
    
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animationHandler = GetComponent<_AnimationHandler>();
        //playerShootingPistol = GameObject.Find("Pistol").GetComponent<_PlayerShooting>();
        //playerShootingShotgun = GameObject.Find("Shotgun").GetComponent<_PlayerShooting>();
        //playerShootingMachineGun = GameObject.Find("MachineGun").GetComponent<_PlayerShooting>();
        //playerShootingGranadeLauncher = GameObject.Find("GranadeLauncher").GetComponent<_PlayerShooting>();
        playerMovement = GetComponent<_PlayerMovement>();  
        playerWeaponHandler = GetComponent<_WeaponHandler>();
        petHandler = GetComponent<PetHandler>();
        playerCapsuleCollider = GetComponent<CapsuleCollider>();
        gameFadeOut = GameObject.Find("FadeInFadeOut").GetComponent<GameFadeout>();
        playerStats = GetComponent<_PlayerStats>();
        craftingHandlerInPlayer = GameObject.Find("CraftingManager").GetComponent<CraftingMainScript>();
        inventory = GetComponent<Inventory>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(!isFading)
        {
            // roll
            if(Input.GetKeyDown(KeyCode.Space) && !isRolling && !petHandler.craftingWindowOpen)
            {
                if(playerMovement.rollCount < playerMovement.maxRoll)
                {
                    isRolling = true;
                    gameObject.layer = 12;
                    playerRigidbody.useGravity = false;
                    if(sceneIndex != 1 && sceneIndex != 2)
                    {
                        //playerCapsuleCollider.enabled = false;
                    }
                    playerMovement.rollTimer = playerMovement.rollDuration;
                    playerMovement.rollCount++;
                    playerInfo.totalTimesRolled++;
                    animationHandler.anim[animationHandler.weapon].SetBool("Rolling", true);
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

            /*
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(petHandler.playerOnArea)
                {
                    petHandler.OpenCraftingWindow();
                    craftingHandlerInPlayer.ShowCraftOptions();
                }
            }
            */

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
                }else if(playerWeaponHandler.weaponEquipped == 3)
                {
                    playerShootingGranadeLauncher.MyInput();
                }

                if(Input.GetKeyDown(KeyCode.Mouse1))
                {
                    playerWeaponHandler.HealFromEatingWeapon();
                }
                
                playerMovement.RollCountTimer();
                //playerMovement.PlayerAim();
            }

            if(!petHandler.craftingWindowOpen)
            {
                playerWeaponHandler.SwitchGuns();
            }
        }
    }

    void LateUpdate()
    {
        if(!isFading)
        {
            petHandler.HandlePet();
        }
        
    }

    void FixedUpdate()
    {
        if(!petHandler.craftingWindowOpen && !isFading)
        {
            playerMovement.HandleMovement();   
            playerMovement.Move();
        }
    }
    public IEnumerator WaitFadeout()
    {
        yield return new WaitForSeconds(1f);
        isFading = false;
    }
}
