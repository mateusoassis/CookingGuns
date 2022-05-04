using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWeapon : MonoBehaviour
{
    private _WeaponHandler weaponHandler;

    public GameObject[] fracturedWeapon;

    void Awake()
    {
        weaponHandler = GetComponentInParent<_WeaponHandler>();
    }

    public void BreakTheWeapon()
    {
        Instantiate(fracturedWeapon[weaponHandler.weaponTypeEquipped], transform.position, transform.rotation);
    }
}
