using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _PlayerStats : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;

    public YouLoseHolder youLoseHolder;


    [Header("Player Stats")]
    public int playerCurrentHealth;
    public int playerMaxHealth;

    private Animator playerTakeDamage;

    [Header("Container de Corações de Vida")]
    public HeartContainerManager heartScript;

    [Header("Referência de _PlayerManager")]
    public _PlayerManager playerManager;
    private SimpleFlash simpleFlashEffect;
    private CameraShake cameraShakeEffect;

    private void Awake()
    {
        playerManager = GetComponent<_PlayerManager>();
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

        youLoseHolder = GameObject.Find("MainCanvas").GetComponent<YouLoseHolder>();
        
        //youLoseHolder.youLoseObject.SetActive(false);
        playerTakeDamage = GameObject.Find("PlayerTakeDamage").GetComponent<Animator>();
        //youLoseScript = GameObject.Find("MainCanvas").GetComponent<YouLose>();
        StartHPDamage();
    }

    public void TakeHPDamage(int damage)
    {
        if(!playerManager.isFading && !playerManager.isRolling)
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
                FindObjectOfType<SoundManager>().PlayOneShot("Mr.MeowAttacked");
            }
            

            /*
            if(futureHP > 0)
            {
                FindObjectOfType<SoundManager>().PlayOneShot("Mr.MeowAttacked");
            }
            */

            if(futureHP <= 0)
            {
                Debug.Log("perdeu");
                FindObjectOfType<SoundManager>().PlayOneShot("Mr.MeowDeath");
                playerManager.gameManager.PauseAndLose();
                //playerManager.gameManager.PauseGame();
                //youLoseHolder.PlayerLost();
                playerManager.playerInfo.totalPlayedTime += (int)playerManager.gameManager.elapsedTime;
                //youLoseHolder.gameObject.GetComponentInChildren<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
                //youLoseScript.PlayerLost();
                playerManager.playerInfo.healthFromLastRoom = 0;
                playerManager.playerInfo.playerCurrentRoom = 0;
            }
            playerTakeDamage.SetTrigger("Pressed");
        }  
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
