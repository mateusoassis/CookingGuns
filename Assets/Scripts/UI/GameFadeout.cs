using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFadeout : MonoBehaviour
{
    public _PlayerManager playerManager;
    public float timeToReturnIsFading;

    void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<_PlayerManager>();
        playerManager.isFading = true;
    }

    void Start()
    {
        
    }

    public void CoroutineToDisable()
    {
        StartCoroutine(playerManager.WaitFadeout(timeToReturnIsFading));
    }
    public void DisableThisObject()
    {
        this.gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        playerManager.isFading = true;
    }
}
