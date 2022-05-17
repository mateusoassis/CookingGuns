using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damageDone;
    public float timeToDestroy;
    void Start() 
    {
        Destroy(gameObject, timeToDestroy);
    }

    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Enemy"|| other.gameObject.tag == "Wall"|| other.gameObject.tag == "Barrel"|| other.gameObject.tag == "EnemyShield")
        {
            Destroy(this.gameObject);
        }
    }

    //void OnDestroy()
}
