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
    public TutorialFadeOut tutorialFadeOut;
    
      

    [Header("Player Flags")]
    public bool isShooting;
    public bool isRolling;
    public bool isGrounded;
    public bool isFading;
    
    // public bool isOnCombat; à implementar no futuro, para travar a interação com a airfryer pra somente quando terminar a batalha (?)

    void Awake()
    {
        tutorialPlayerWeaponHandler = GetComponent<T_WeaponHandler>();
        playerShootingPistol = GameObject.Find("Pistol").GetComponent<_PlayerShooting>();
        playerShootingShotgun = GameObject.Find("Shotgun").GetComponent<_PlayerShooting>();
        playerShootingMachineGun = GameObject.Find("MachineGun").GetComponent<_PlayerShooting>();
        tutorialPlayerMovement = GetComponent<TutorialPlayerMovement>();
        animationHandler = GetComponent<_AnimationHandler>();         
        tutorialFadeOut = GetComponent<TutorialFadeOut>();
    }
    void Start()
    {

    }

    void Update()
    {
        if(!isFading)
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
                    tutorialPlayerMovement.playerRigidbody.useGravity = false;
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
        
    }

    void LateUpdate()
    {
        
    }

    void FixedUpdate()
    {
        if(!isFading)
        {
            tutorialPlayerMovement.HandleMovement();   
            tutorialPlayerMovement.Move();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Door")
        {
            // botar pra ir pra parte 2
        }
    }
    public IEnumerator WaitFadeout()
    {
        yield return new WaitForSeconds(1f);
        isFading = false;
        tutorialFadeOut.startCheckpointCollider.enabled = true;
        Debug.Log("corotina termina");
    }
    /*
    public void RunFadeOut()
    {
        StartCoroutine(WaitFadeout());
    }
    */
}
