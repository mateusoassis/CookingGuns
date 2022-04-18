using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShotgunReset : MonoBehaviour
{
    public _PlayerShooting shotgunScript;
    public int sceneIndex;

    void Awake()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void ResetThisShotgun()
    {
        if(sceneIndex != 0)
        {
            shotgunScript.playerManager.animationHandler.anim[shotgunScript.playerManager.animationHandler.weapon].ResetTrigger("Shoot");
        }
        else
        {
            GetComponent<Animator>().ResetTrigger("Shoot");
        }
        
    }
}
