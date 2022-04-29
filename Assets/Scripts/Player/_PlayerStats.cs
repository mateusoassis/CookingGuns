using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _PlayerStats : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;

    public GameObject youLoseWindow;

    //public YouLose youLoseScript;

    [Header("Player Stats")]
    [Range(0,7)]
    public int playerCurrentHealth;
    public int playerHealthFromPreviousRoom;
    public int playerMaxHealth;

    public Animator playerTakeDamage;

    public HeartContainerManager heartScript;
    public _PlayerManager playerManager;

    private void Awake()
    {
        youLoseWindow = GameObject.Find("YouLose");
        playerManager = GetComponent<_PlayerManager>();
        heartScript = GameObject.Find("HeartContainer").GetComponent<HeartContainerManager>();
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
        youLoseWindow.GetComponentInChildren<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        youLoseWindow.SetActive(false);
        playerTakeDamage = GameObject.Find("PlayerTakeDamage").GetComponent<Animator>();
        //youLoseScript = GameObject.Find("MainCanvas").GetComponent<YouLose>();
        StartHPDamage();
    }

    public void TakeHPDamage(int damage)
    {
        if(!playerManager.isFading)
        {
            playerCurrentHealth -= damage;
            heartScript.hpLost += damage;
            heartScript.UpdateAllHearts();

            if(playerCurrentHealth <= 0)
            {
                playerManager.gameManager.PauseGame();
                youLoseWindow.SetActive(true);
                
                //youLoseScript.PlayerLost();
                playerManager.playerInfo.healthFromLastRoom = 0;
                playerManager.playerInfo.playerCurrentRoom = 0;
            }
            playerTakeDamage.SetTrigger("Pressed");
        }  
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EnemyGranade")
        {
            if((other.gameObject.TryGetComponent(out PudimAreaDamage pudimAreaDamage)))
            {
                //flashEffect.FlashStart();
                TakeHPDamage(pudimAreaDamage.damageDone);
            }
        }
    }

    public void StartHPDamage()
    {
        heartScript.UpdateAllHearts();
    }
}
