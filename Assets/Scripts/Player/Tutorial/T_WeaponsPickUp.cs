using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_WeaponsPickUp : MonoBehaviour
{
    public Vector3 rotateVector;
    public float rotationMultiplier;

    void Update()
    {
        transform.Rotate(rotateVector * Time.deltaTime * rotationMultiplier);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<T_WeaponHandler>().UnlockPistol();
            Destroy(this.gameObject);
        }
    }
}
