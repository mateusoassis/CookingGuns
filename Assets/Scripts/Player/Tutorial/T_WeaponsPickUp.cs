using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_WeaponsPickUp : MonoBehaviour
{
    public Vector3 rotateVector;
    public float rotationMultiplier;

    public GameObject leftMouseClickButton;
    
    void Update()
    {
        transform.Rotate(rotateVector * Time.deltaTime * rotationMultiplier);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<T_WeaponHandler>().UnlockPistol();
            leftMouseClickButton.SetActive(true);
            other.gameObject.GetComponent<T_PlayerManager>().tutorialPlayerWeaponHandler.ActivatePistol_();
            other.gameObject.GetComponent<T_PlayerManager>().tutorialPlayerWeaponHandler.WeaponManager(other.gameObject.GetComponent<T_PlayerManager>().tutorialPlayerWeaponHandler.weaponEquipped);
            Destroy(this.gameObject);
        }
    }
}
