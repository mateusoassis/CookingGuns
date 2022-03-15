using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillboardCanvas : MonoBehaviour
{
    public Transform cam;
    public Slider healthSlider;
    public EnemyStats enemyStats;

    void Start() 
    {
        SetMaxHealth();
    }

    void Update() 
    {
        healthSlider.value = enemyStats.enemyHealth;
    }

    public void SetMaxHealth()
    {
        healthSlider.maxValue = enemyStats.enemyMaxHealth;
        healthSlider.value = enemyStats.enemyHealth;
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
