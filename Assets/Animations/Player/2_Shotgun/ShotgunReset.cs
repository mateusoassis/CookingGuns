using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunReset : MonoBehaviour
{
    public _PlayerShooting shotgunScript;

    public void ResetThisShotgun()
    {
        shotgunScript.playerManager.animationHandler.anim[shotgunScript.playerManager.animationHandler.weapon].ResetTrigger("Shoot");
    }
}
