using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCollider : MonoBehaviour
{
    public Transform checkpoint;
    public int damage;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if((other.gameObject.TryGetComponent(out _PlayerStats playerStats)))
            {
                playerStats.TakeHPDamage(damage);
            }
            other.transform.position = checkpoint.position;
        }
    }
}
