using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWeapon : MonoBehaviour
{
    public GameObject fracturedWeapon;

    public void BreakTheWeapon()
    {
        Instantiate(fracturedWeapon, transform.position, transform.rotation);
    }
}
