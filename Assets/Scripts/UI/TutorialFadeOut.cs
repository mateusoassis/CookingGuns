using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFadeOut : MonoBehaviour
{
    public T_PlayerManager tutorialPlayerManager;


    void Start()
    {
        tutorialPlayerManager = GameObject.Find("TutorialPlayer").GetComponent<T_PlayerManager>();
        tutorialPlayerManager.isFading = true;
    }

    public void DisableObject()
    {
        tutorialPlayerManager.isFading = false;
        this.gameObject.SetActive(false);
    }
}
