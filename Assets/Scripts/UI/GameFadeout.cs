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

    public void DisableObject()
    {
        playerManager.isFading = false;
        this.gameObject.SetActive(false);
    }
}
