using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnRangeToAttack : MonoBehaviour
{
    
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GetComponentInParent<Shieldoca>().playerOnRange = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GetComponentInParent<Shieldoca>().playerOnRange = false;
        }
    }
}
