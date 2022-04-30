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
        if(other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }

        if(other.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }

    //void OnDestroy()
}
