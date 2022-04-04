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
    
    [SerializeField] private Slider playerHealthSlider;

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
        playerHealthSlider.minValue = 0;
        playerHealthSlider.maxValue = playerMaxHealth;
        playerHealthSlider.value = playerCurrentHealth;
    }

    public void TestHeal()
    {
        playerCurrentHealth += 10;
        UpdateHealthValues();
    }
    public void TestMaxHP()
    {
        playerMaxHealth += 10;
        UpdateHealthValues();
    }

    public void UpdateHealthValues()
    {
        playerHealthSlider.maxValue = playerMaxHealth;
        playerHealthSlider.value = playerCurrentHealth;
    }

    public void TakeHPDamage(int damage)
    {
        OnPlayerDamaged?.Invoke();
        playerCurrentHealth -= damage;
        UpdateHealthValues();
    }
}
