using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollAuxiliary : MonoBehaviour
{
    public _WeaponHandler playerWeaponHandler; 

    void Awake()
    {
        playerWeaponHandler = GetComponentInParent<_WeaponHandler>();
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void DeactivateThisObject()
    {
        gameObject.SetActive(false);
    }
    public void OnDisable()
    {
        playerWeaponHandler.WeaponManager(playerWeaponHandler.weaponTypeEquipped);
    }
}
