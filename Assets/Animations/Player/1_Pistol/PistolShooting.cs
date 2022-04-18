using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShooting : MonoBehaviour
{
    public _PlayerShooting pistolScript;

    void Start()
    {
        this.gameObject.SetActive(true);
    }

    public void DoShoot()
    {
        pistolScript.Shoot();
        pistolScript.playerManager.isShooting = true;
    }
}
