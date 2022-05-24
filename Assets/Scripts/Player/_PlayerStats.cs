using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _PlayerStats : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;

    [Header("Player Stats")]
    public int playerCurrentHealth;
    public int playerMaxHealth;

    public float immuneDuration;
    private float immuneTimer;

    private Animator playerTakeDamage;

    [Header("Container de Corações de Vida")]
    public HeartContainerManager heartScript;

    [Header("Referência de _PlayerManager")]
    public _PlayerManager playerManager;

    [Header("Referência de Player Perdeu")]
    public YouLoseHolder youLoseHolder;
    public GameObject deadModel;

    [Header("VFX")]
    private SimpleFlash simpleFlashEffect;
    private CameraShake cameraShakeEffect;

    private void Awake()
    {
        heartScript = GameObject.Find("HeartContainer").GetComponent<HeartContainerManager>();
        simpleFlashEffect = GetComponentInChildren<SimpleFlash>();
        cameraShakeEffect = GameObject.Find("Shake").GetComponent<CameraShake>();
        if (playerManager.playerInfo.healthFromLastRoom > 0)
        {
            playerCurrentHealth = playerManager.playerInfo.healthFromLastRoom;
        }
        else
        {
            playerCurrentHealth = playerMaxHealth;
        }
    }

    void Start()
    {
        immuneTimer = immuneDuration;
        youLoseHolder = GameObject.Find("MainCanvas").GetComponent<YouLoseHolder>();
        
        //youLoseHolder.youLoseObject.SetActive(false);
        playerTakeDamage = GameObject.Find("PlayerTakeDamage").GetComponent<Animator>();
        //youLoseScript = GameObject.Find("MainCanvas").GetComponent<YouLose>();
        StartHPDamage();
    }

    void Update()
    {
        if(playerManager.isImmune)
        {
            immuneTimer -= Time.deltaTime;
            if(immuneTimer < 0)
            {
                playerManager.isImmune = false;
            }
        }
    }

    public void ImmuneNow()
    {
        immuneTimer = immuneDuration;
        playerManager.isImmune = true;
    }

    public void TakeHPDamage(int damage)
    {
        if(!playerManager.isFading && !playerManager.isRolling && !playerManager.isImmune)
        {
            int futureHP = playerCurrentHealth - damage;
            if(futureHP > 0)
            {
                Debug.Log("n perdeu");
                cameraShakeEffect.Shockwave();
                simpleFlashEffect.Flash();
                playerCurrentHealth = futureHP;
                heartScript.hpLost += damage;
                heartScript.UpdateAllHearts();
                ImmuneNow();
                if(!playerManager.isDead)
                {
                    FindObjectOfType<SoundManager>().PlayOneShot("Mr.MeowAttacked");
                }
                playerManager.playerInfo.healthFromLastRoom = playerCurrentHealth;
            }

            if(futureHP <= 0)
            {
                // switch pra câmera de dead
                if(!playerManager.isDead)
                {
                    FindObjectOfType<SoundManager>().PlayOneShot("Mr.MeowDeath");
                }
                playerManager.isDead = true;
                deadModel.SetActive(true);
                playerManager.petHandler.cinemachineSwitchBlend.DeadCamera();
                playerManager.playerWeaponHandler.PlayerIsDead();
                playerManager.playerWeaponHandler.breakWeaponScript.BreakTheWeapon();
                //DeadPlayer();
            }
            if(!playerManager.isDead)
            {
                playerTakeDamage.SetTrigger("Pressed");
            }
        }  
    }

    public void DeadPlayer()
    {
        Debug.Log("perdeu");
        //FindObjectOfType<SoundManager>().PlayOneShot("Mr.MeowDeath");
        playerManager.gameManager.PauseAndLose();
        //playerManager.gameManager.PauseGame();
        //youLoseHolder.PlayerLost();
        //playerManager.playerInfo.totalPlayedTime += (int)playerManager.gameManager.elapsedTime;
        //youLoseHolder.gameObject.GetComponentInChildren<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        //youLoseScript.PlayerLost();
        playerManager.playerInfo.healthFromLastRoom = 0;
        playerManager.playerInfo.playerCurrentRoom = 0;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EnemyGrenade")
        {
            if((other.gameObject.TryGetComponent(out PudimAreaDamage pudimAreaDamage)))
            {
                //flashEffect.FlashStart();
                simpleFlashEffect.Flash();
                TakeHPDamage(pudimAreaDamage.damageDone); 
            }
        }
        if (other.gameObject.tag == "BarrelExplosion")
        {
            if ((other.gameObject.TryGetComponent(out BarrelTrapExplosion barrelAreaDamage)))
            {
                //simpleFlashEffect.Flash();
                TakeHPDamage(barrelAreaDamage.damageDoneInPlayer);
            }
        }
    }

    public void StartHPDamage()
    {
        heartScript.UpdateAllHearts();
    }
}
