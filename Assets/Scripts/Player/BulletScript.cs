using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damageDone;
    void Start() 
    {
        Destroy(this.gameObject, 4f);
    }

    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("inimigo de nome " + other.name);
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "EnemyShield")
        {
            Debug.Log("inimigo de nome " + other.name);
            Destroy(this.gameObject);
        }

    }
}
