using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_PlayerManager : MonoBehaviour
{
    public TutorialPlayerMovement tutorialPlayerMovement;
    public _AnimationHandler animationHandler;
    public _PlayerShooting playerShootingPistol;
    public _PlayerShooting playerShootingShotgun;
    public _PlayerShooting playerShootingMachineGun;
    public T_WeaponHandler tutorialPlayerWeaponHandler;
    public TutorialManager tutorialManager;
    //public PetHandler petHandler;
    public PlayerInfo playerInfo;

    
      

    [Header("Player Flags")]
    public bool isShooting;
    public bool isRolling;
    
    // public bool isOnCombat; à implementar no futuro, para travar a interação com a airfryer pra somente quando terminar a batalha (?)

    void Awake()
    {
        tutorialPlayerWeaponHandler = GetComponent<T_WeaponHandler>();
        playerShootingPistol = GameObject.Find("Pistol").GetComponent<_PlayerShooting>();
        playerShootingShotgun = GameObject.Find("Shotgun").GetComponent<_PlayerShooting>();
        playerShootingMachineGun = GameObject.Find("MachineGun").GetComponent<_PlayerShooting>();
        tutorialPlayerMovement = GetComponent<TutorialPlayerMovement>();
        animationHandler = GetComponent<_AnimationHandler>(); 
        //tutorialPlayerWeaponHandler.ActivatePistol_();
        
    }
    void Start()
    {
          
        
        //petHandler = GetComponent<PetHandler>();

        //playerWeaponHandler.ActivatePistol();
        tutorialPlayerWeaponHandler.ActivatePistol_();
        tutorialPlayerWeaponHandler.WeaponManager(tutorialPlayerWeaponHandler.weaponEquipped);
    }

    void Update()
    {
        // roll
        if(Input.GetKeyDown(KeyCode.Space) && !isRolling)
        {
            if(tutorialPlayerMovement.rollCount < tutorialPlayerMovement.maxRoll)
            {
                isRolling = true;
                tutorialPlayerMovement.rollTimer = tutorialPlayerMovement.rollDuration;
                tutorialPlayerMovement.rollCount++;
                playerInfo.totalTimesRolled++;
            }
        }

        // pause
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(tutorialManager.confirmationWindowOpen)
            {
                tutorialManager.CloseAllConfirmationWindows();
            }
            else if(!tutorialManager.confirmationWindowOpen)
            {
                if(!tutorialManager.pausedGame)
                {
                    tutorialManager.PauseGame();
                }
                else if(tutorialManager.pausedGame)
                {
                    tutorialManager.ResumeGame();
                }
            }
            
        }

        // normal behaviour quando O JOGO NÃO ESTÁ PAUSADO
        if(!tutorialManager.pausedGame)
        {
            //tutorialPlayerWeaponHandler.WeaponBehaviour();
            if(tutorialPlayerWeaponHandler.weaponEquipped == 0)
            {
                playerShootingPistol.MyInput();
            }
            else if(tutorialPlayerWeaponHandler.weaponEquipped == 1)
            {
                playerShootingShotgun.MyInput();
            }
            else if(tutorialPlayerWeaponHandler.weaponEquipped == 2)
            {
                playerShootingMachineGun.MyInput();
            }
            
            tutorialPlayerMovement.RollCountTimer();
            tutorialPlayerMovement.PlayerAim();
        }

        tutorialPlayerWeaponHandler.SwitchGuns();
    }

    void LateUpdate()
    {
        //petHandler.HandlePet();
    }

    void FixedUpdate()
    {
        tutorialPlayerMovement.HandleMovement();   
        tutorialPlayerMovement.Move();
    }
}
