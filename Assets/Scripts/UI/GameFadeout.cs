using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFadeout : MonoBehaviour
{
    public _PlayerManager playerManager;
    public float fadeoutDuration;

    void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<_PlayerManager>();
        playerManager.isFading = true;
    }

    public void CoroutineToDisable()
    {
        StartCoroutine(playerManager.WaitFadeout(fadeoutDuration));    
    }
    public void DisableThisObject()
    {
        this.gameObject.SetActive(false);
    }

    public void CoroutineToEnable()
    {
        
    }
}
