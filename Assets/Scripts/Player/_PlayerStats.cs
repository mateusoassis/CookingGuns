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
    [Range(0,7)]
    public int playerCurrentHealth;
    public int playerHealthFromPreviousRoom;
    public int playerMaxHealth;

    public Animator playerTakeDamage;

    public HeartContainerManager heartScript;

    private void Awake()
    {
        playerCurrentHealth = playerMaxHealth;
        heartScript = GameObject.Find("HeartContainer").GetComponent<HeartContainerManager>();
        if(playerHealthFromPreviousRoom == 0)
        {
            playerCurrentHealth = playerMaxHealth;
        }
        else
        {
            playerCurrentHealth = playerHealthFromPreviousRoom;
        }
    }

    void Start()
    {
        /*
        if(playerHealthFromPreviousRoom == 0)
        {
            playerCurrentHealth = playerMaxHealth;
        }
        else
        {
            playerCurrentHealth = playerHealthFromPreviousRoom;
        }*/

        playerTakeDamage = GameObject.Find("PlayerTakeDamage").GetComponent<Animator>();
    }

    public void TestHeal()
    {
        heartScript.FullHeal(playerCurrentHealth);
        playerCurrentHealth = playerMaxHealth;
        //OnPlayerDamaged?.Invoke();
        //heartScript.FullHeal();
    }

    public void TakeHPDamage(int damage)
    {
        playerCurrentHealth -= damage;
        heartScript.hpLost += damage;
        heartScript.UpdateAllHearts();
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
