using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _PlayerStats : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;

    public YouLose youLoseScript;

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
        playerTakeDamage = GameObject.Find("PlayerTakeDamage").GetComponent<Animator>();
        youLoseScript = GameObject.Find("MainCanvas").GetComponent<YouLose>();
    }

    public void TestHeal()
    {
        
    }

    public void TakeHPDamage(int damage)
    {
        playerCurrentHealth -= damage;
        heartScript.hpLost += damage;
        heartScript.UpdateAllHearts();

        if(playerCurrentHealth <= 0)
        {
            playerManager.gameManager.PauseGame();
            youLoseScript.PlayerLost();
            playerManager.playerInfo.healthFromLastRoom = 0;
            playerManager.playerInfo.playerCurrentRoom = 0;
        }
        
        playerTakeDamage.SetTrigger("Pressed");
    }
}
