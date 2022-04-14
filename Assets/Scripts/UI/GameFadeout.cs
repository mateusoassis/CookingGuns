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
        Debug.Log("corotina começa");
        StartCoroutine(playerManager.WaitFadeout());
             
    }
    public void DisableThisObject()
    {
        Debug.Log("disable");
        this.gameObject.SetActive(false);
    }
}
