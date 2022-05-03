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

    //public YouLose youLoseScript;

    [Header("Player Stats")]
    [Range(0,7)]
    public int playerCurrentHealth;
    public int playerHealthFromPreviousRoom;
    public int playerMaxHealth;

    public Animator playerTakeDamage;

    public HeartContainerManager heartScript;
    public _PlayerManager playerManager;
    public SimpleFlash simpleFlashEffect;

    private void Awake()
    {
        youLoseHolder = GameObject.Find("MainUI").GetComponent<YouLoseHolder>();
        playerManager = GetComponent<_PlayerManager>();
        heartScript = GameObject.Find("HeartContainer").GetComponent<HeartContainerManager>();
        simpleFlashEffect = GetComponentInChildren<SimpleFlash>();
        if(playerManager.playerInfo.healthFromLastRoom > 0)
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
        //youLoseHolder.youLoseObject.SetActive(false);
        playerTakeDamage = GameObject.Find("PlayerTakeDamage").GetComponent<Animator>();
        //youLoseScript = GameObject.Find("MainCanvas").GetComponent<YouLose>();
        StartHPDamage();
    }

    public void TakeHPDamage(int damage)
    {
        if(!playerManager.isFading)
        {
            simpleFlashEffect.Flash();
            playerCurrentHealth -= damage;
            heartScript.hpLost += damage;
            heartScript.UpdateAllHearts();

            if(playerCurrentHealth <= 0)
            {
                playerManager.gameManager.PauseGame();
                youLoseHolder.youLoseObject.SetActive(true);
                youLoseHolder.youLoseObject.GetComponentInChildren<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
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
    }

    public void StartHPDamage()
    {
        heartScript.UpdateAllHearts();
    }
}
