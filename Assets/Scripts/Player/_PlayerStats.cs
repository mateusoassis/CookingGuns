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
    public int playerHealthFromPreviousRoom;
    public int playerMaxHealth;

    private void Awake()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    void Start()
    {
        if(playerHealthFromPreviousRoom == 0)
        {
            playerCurrentHealth = playerMaxHealth;
        }
        else
        {
            playerCurrentHealth = playerHealthFromPreviousRoom;
        }
    }

    public void TestHeal()
    {
        playerCurrentHealth = playerMaxHealth;
        OnPlayerDamaged?.Invoke();
    }

    public void TakeHPDamage(int damage)
    {
        playerCurrentHealth -= damage;
        OnPlayerDamaged?.Invoke();
    }
}
