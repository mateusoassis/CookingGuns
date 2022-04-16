using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shieldy : MonoBehaviour
{
    public ShieldGirl parent;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBullet")
        {
            parent.ShieldyWasHit();
            Destroy(other.gameObject);
        }
    }
}
