using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public int enemyMaxHealth;
    public int enemyHealth;
    [SerializeField] private PlayerController playerController;
    public GameObject dropPrefab;
    public float dropChance;
    public PlayerInfo playerInfo;

    void Start() {
        enemyHealth = enemyMaxHealth;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }
    
    public void TakeDamage(int damageTaken)
    {
        enemyHealth -= damageTaken;
        if(enemyHealth <= 0)
        {            
            Destroy(this.gameObject);
            if(GetComponent<MinusOnDestroy>() == null)
            {
                Debug.Log(name);
                enemySpawner.enemiesKilled++;
            }
            playerInfo.totalEnemiesKilled++;

            float realDropChance = 100 - dropChance;
            int u = Random.Range(0, 101);
            if(u >= realDropChance)
            {
                Instantiate(dropPrefab, transform.position, Quaternion.identity);
            }
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
