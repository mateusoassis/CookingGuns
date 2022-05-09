using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public VermelhaBehaviour vermelhaBehaviour;
    public Transform playerTransform;

    void Start() 
    {
        vermelhaBehaviour = GetComponentInParent<VermelhaBehaviour>();
    }

    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            playerTransform = other.gameObject.transform;
            vermelhaBehaviour.explosionCollision = true;
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            vermelhaBehaviour.explosionCollision = false;
        }
    }

    /*
    public void OnDestroy()
    {
        if(vermelhaBehaviour.explosionCollision)
        {
            if(playerTransform != null)
            {
                playerTransform.GetComponent<_PlayerStats>().TakeHPDamage(vermelhaBehaviour.explosionDamage);
            }
        }
    }
    */
}
