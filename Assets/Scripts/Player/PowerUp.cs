using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector;
    private WeaponHandler weaponHandler;
    [SerializeField] private float rotationMultiplier;
    
    void Start()
    {
        weaponHandler = GameObject.Find("WeaponIcons").GetComponent<WeaponHandler>();
    }

    void Update()
    {
        transform.Rotate(rotationVector * Time.deltaTime * rotationMultiplier);
    }

    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            //weaponHandler.powerUpCount++;
            if(weaponHandler.powerUpCount == 0)
            {
                weaponHandler.axeUnlocked = true;
            }
            else if(weaponHandler.powerUpCount == 1)
            {
                weaponHandler.pistolUnlocked = true;
            }
            weaponHandler.powerUpCount++;
            Destroy(this.gameObject);
        }
    }
}
