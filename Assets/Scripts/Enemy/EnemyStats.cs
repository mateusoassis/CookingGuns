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
    public GameObject powerUpPrefab;

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
            enemySpawner.enemiesKilled++;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBullet")
        {
            if((other.gameObject.TryGetComponent(out BulletScript bulletScript)))
            {
                Debug.Log("dano");
                TakeDamage(bulletScript.damageDone);
                
            }
            /*Debug.Log("tag");
            if(playerController.isAttacking || playerController.weaponActive == 3)
            {
                Debug.Log("weapon active");
                if((other.gameObject.TryGetComponent(out CollisionDetection otherCollision)))
                {
                    Debug.Log("dano");
                    TakeDamage(otherCollision.damageDone);
                }
            //}*/
            
        }
    }
}
