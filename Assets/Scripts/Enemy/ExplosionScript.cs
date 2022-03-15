using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public EnemyBehaviour enemyBehaviour;

    void Start() 
    {
        enemyBehaviour = GetComponentInParent<EnemyBehaviour>();
    }

    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("dale");
            enemyBehaviour.explosionCollision = true;
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("ih saiu");
            enemyBehaviour.explosionCollision = false;
        }
    }
}
