using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFadeout : MonoBehaviour
{
    public _PlayerManager playerManager;

    void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<_PlayerManager>();
        playerManager.isFading = true;
    }

    public void CoroutineToDisable()
    {
        StartCoroutine(playerManager.WaitFadeout());
             
    }
    public void DisableThisObject()
    {
        this.gameObject.SetActive(false);
    }
}
