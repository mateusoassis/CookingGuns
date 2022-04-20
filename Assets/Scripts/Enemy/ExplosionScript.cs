using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public EnemyBehaviour enemyBehaviour;
    public Transform playerTransform;

    void Start() 
    {
        enemyBehaviour = GetComponentInParent<EnemyBehaviour>();
    }

    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            playerTransform = other.gameObject.transform;
            enemyBehaviour.explosionCollision = true;
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enemyBehaviour.explosionCollision = false;
        }
    }

    public void OnDestroy()
    {
        if(enemyBehaviour.explosionCollision)
        {
            if(playerTransform != null)
            {
                playerTransform.GetComponent<_PlayerStats>().TakeHPDamage(enemyBehaviour.explosionDamage);
            }
        }
    }
}
