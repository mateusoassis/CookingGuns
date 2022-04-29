using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public GameObject playerReloadBar;
    public GameManager gameManager;
    public PetHandler petHandler;
    public PlayerInfo playerInfo;
    public CapsuleCollider playerCapsuleCollider;

    public CraftingMainScript craftingHandlerInPlayer;
    public Inventory inventory;
    public GameFadeout gameFadeOut;

    public int sceneIndex;

    [Header("Comer arma")]
    public float eatingWeaponTimer;
    public float eatingWeaponDuration;
    public bool rmbHeldDown;
    public bool rmbHasToPressAgain;
    public bool canceledEating;
    private GameObject playerEatingWeaponBar;
    private Slider playerEatingWeaponBarSlider;

    
    [Header("Player Flags")]
    public bool isShooting;
    public bool isRolling;
    public bool isFading;
    public bool isWalking;
    public bool isEatingWeapon;
    public bool isDead;
    public bool endGame;

    void Awake()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerInfo.currentSceneIndex = sceneIndex;
        playerReloadBar = GameObject.Find("ReloadBar");
        playerEatingWeaponBar = GameObject.Find("EatingWeaponBar");
        playerEatingWeaponBarSlider = playerEatingWeaponBar.GetComponent<Slider>();
    }
    
    void Start()
    {
        playerEatingWeaponBarSlider.maxValue = eatingWeaponDuration;
        playerReloadBar.SetActive(false);
        playerEatingWeaponBar.SetActive(false);
        playerRigidbody = GetComponent<Rigidbody>();
        animationHandler = GetComponent<_AnimationHandler>();
        playerMovement = GetComponent<_PlayerMovement>();  
        playerWeaponHandler = GetComponent<_WeaponHandler>();
        petHandler = GetComponent<PetHandler>();
        playerCapsuleCollider = GetComponent<CapsuleCollider>();
        gameFadeOut = GameObject.Find("StartFadeIn").GetComponent<GameFadeout>();
        playerStats = GetComponent<_PlayerStats>();
        craftingHandlerInPlayer = GameObject.Find("CraftingManager").GetComponent<CraftingMainScript>();
        inventory = GetComponent<Inventory>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        eatingWeaponTimer = 0f;
    }

    void Update()
    {
        if(!isFading)
        {
            // roll
            if(Input.GetKeyDown(KeyCode.Space) && !isRolling && !petHandler.craftingWindowOpen) //&& !isEatingWeapon)
            {
                Debug.Log("barra de espaço");
                if(playerMovement.rollCount < playerMovement.maxRoll)
                {
                    Debug.Log("roll man");
                    eatingWeaponTimer = 0f;
                    canceledEating = true;
                    playerEatingWeaponBar.SetActive(false);

                    isRolling = true;
                    playerWeaponHandler.Roll();
                    gameObject.layer = 12;
                    playerRigidbody.useGravity = false;

                    playerMovement.rollTimer = playerMovement.rollDuration;
                    playerMovement.rollCount++;

                    playerInfo.totalTimesRolled++;

                    animationHandler.anim[animationHandler.weapon].SetBool("Rolling", true);

                    // parte que reseta a barra de comer arma
                    if(rmbHeldDown)
                    {
                        rmbHasToPressAgain = true;
                    }
                    Debug.Log("fim da barra de espaço");
                }
            }

            // pause
            if(Input.GetKeyDown(KeyCode.Escape) && !isDead && !endGame)
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

            
            if(Input.GetKeyDown(KeyCode.E) && !isEatingWeapon)
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
                if(playerWeaponHandler.weaponEquipped == 0 && !isEatingWeapon)
                {
                    playerShootingPistol.MyInput();
                    //playerShootingPistol.reloadDisplay.gameObject.GetComponent<Slider>();
                    playerShootingPistol.AmmoDisplayUpdate();
                }
                else if(playerWeaponHandler.weaponEquipped == 1 && !isEatingWeapon)
                {
                    playerShootingShotgun.MyInput();
                    //playerShootingShotgun.reloadDisplay.gameObject.GetComponent<Slider>();
                    playerShootingShotgun.AmmoDisplayUpdate();
                }
                else if(playerWeaponHandler.weaponEquipped == 2 && !isEatingWeapon)
                {
                    playerShootingMachineGun.MyInput();
                    //playerShootingMachineGun.reloadDisplay.gameObject.GetComponent<Slider>();
                    playerShootingMachineGun.AmmoDisplayUpdate();
                }
                else if(playerWeaponHandler.weaponEquipped == 3 && !isEatingWeapon)
                {
                    playerShootingGranadeLauncher.MyInput();
                    //playerShootingGranadeLauncher.reloadDisplay.gameObject.GetComponent<Slider>();
                    playerShootingGranadeLauncher.AmmoDisplayUpdate();
                }

                if(Input.GetKey(KeyCode.Mouse1) && !isRolling && !isFading && !canceledEating) //&& !rmbHasToPressAgain)
                {
                    if(rmbHasToPressAgain)
                    {
                        isEatingWeapon = false;
                    }
                    else
                    {
                        rmbHeldDown = true;
                        playerWeaponHandler.UpdateAmountUnlocked();
                        if(playerWeaponHandler.amountUnlocked > 1)
                        {
                            if(rmbHeldDown && !rmbHasToPressAgain)
                            {
                                playerEatingWeaponBar.SetActive(true);
                                playerEatingWeaponBarSlider.value = eatingWeaponTimer/eatingWeaponDuration;
                                isEatingWeapon = true;
                                eatingWeaponTimer += Time.deltaTime;
                                //animationHandler.anim[playerWeaponHandler.weaponEquipped].SetBool("Walking", false);

                                if(eatingWeaponTimer >= eatingWeaponDuration)
                                {
                                    playerWeaponHandler.HealFromEatingWeapon();
                                    isEatingWeapon = false;
                                    
                                    playerEatingWeaponBar.SetActive(false);
                                    rmbHasToPressAgain = true;
                                }
                            }
                            else if(rmbHasToPressAgain)
                            {
                                isEatingWeapon = false;
                            }
                        }
                    }
                }

                if(Input.GetKeyUp(KeyCode.Mouse1))
                {
                    //canceledEating = false;
                    rmbHasToPressAgain = false;
                    isEatingWeapon = false;
                    rmbHeldDown = false;
                    eatingWeaponTimer = 0f;
                    playerEatingWeaponBar.SetActive(false);
                }
                
                //playerMovement.RollCountTimer();
                //playerMovement.PlayerAim();
            }

            if(!petHandler.craftingWindowOpen && !isEatingWeapon)
            {
                playerWeaponHandler.SwitchGuns();
            }
        }
        playerMovement.RollCountTimer();
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
        if(!petHandler.craftingWindowOpen && !isFading) //&& !isEatingWeapon)
        {
            playerMovement.HandleMovement();   
            playerMovement.Move();
        }
    }
    public IEnumerator WaitFadeout(float n)
    {
        yield return new WaitForSeconds(n);
        isFading = false;
    }

    public void ReloadDisplayUpdate()
    {
        playerReloadBar.SetActive(true);
    }

    public void ReloadEndDisplay()
    {
        playerReloadBar.SetActive(false);
    }
}
