using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T_EnemyStats : MonoBehaviour
{
    public int enemyMaxHealth;
    public int enemyHealth;
    [SerializeField] private PlayerController playerController;
    public GameObject dropPrefab;
    public float dropChance;
    public T_Door doorScript;

    void Start() {
        enemyHealth = enemyMaxHealth;
        playerController = GameObject.Find("TutorialPlayer").GetComponent<PlayerController>();
        doorScript = GameObject.Find("DoorContainer").GetComponent<T_Door>();
    }
    
    public void TakeDamage(int damageTaken)
    {
        enemyHealth -= damageTaken;
        if(enemyHealth <= 0)
        {     
            doorScript.enemyKilled = true;       
            Destroy(this.gameObject);

            Instantiate(dropPrefab, transform.position, Quaternion.identity);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBullet")
        {
            if((other.gameObject.TryGetComponent(out BulletScript bulletScript)))
            {
                TakeDamage(bulletScript.damageDone);
            }
        }
    }
}

