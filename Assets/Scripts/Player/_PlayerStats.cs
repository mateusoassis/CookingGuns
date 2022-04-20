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
        //playerCurrentHealth = playerMaxHealth;
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
        /*
        heartScript.FullHeal(playerCurrentHealth);
        playerCurrentHealth = playerMaxHealth;
        //OnPlayerDamaged?.Invoke();
        heartScript.FullHeal();
        */
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
        /*
        int loops = 0;
        while (loops < damage)
        {
            heartScript.UpdateHPonUI(playerCurrentHealth-loops);
            playerCurrentHealth--;
            loops++;
            return;
        }
        */

        /*
        if(heartScript.hpLost == 0)
        {
            return;
        }
        else
        {
            while(heartScript.hpLost - heartScript.heartController != 0)
            {
                heartScript.UpdateHPonUI(playerMaxHealth - heartScript.heartController);
                heartScript.heartController--;
                
            }
        }
        */
        //heartScript.UpdateHPonUI(playerCurrentHealth);
        //playerCurrentHealth -= damage;
        //OnPlayerDamaged?.Invoke();
        //heartScript.UpdateHPonUI();
        playerTakeDamage.SetTrigger("Pressed");
    }
}
