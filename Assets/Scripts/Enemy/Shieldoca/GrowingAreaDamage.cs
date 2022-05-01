using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingAreaDamage : MonoBehaviour
{
    public Shieldoca shieldoca;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            shieldoca.isPlayerInsideArea = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            shieldoca.isPlayerInsideArea = false;
        }
    }
}
