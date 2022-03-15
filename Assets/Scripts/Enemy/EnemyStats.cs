using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public int enemyMaxHealth;
    public int enemyHealth;
    [SerializeField] private PlayerController playerController;

    void Start() {
        enemyHealth = enemyMaxHealth;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    
    public void TakeDamage(int damageTaken)
    {
        enemyHealth -= damageTaken;
        if(enemyHealth <= 0)
        {
            Debug.Log("matou o inimigo " + name);
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            Debug.Log("tag");
            if(playerController.isAttacking || playerController.weaponActive == 3)
            {
                Debug.Log("weapon active");
                if((other.gameObject.TryGetComponent(out CollisionDetection otherCollision)))
                {
                    Debug.Log("dano");
                    TakeDamage(otherCollision.damageDone);
                }
            }
            
        }
    }
}
