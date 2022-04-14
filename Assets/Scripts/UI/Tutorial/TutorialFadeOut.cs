using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFadeOut : MonoBehaviour
{
    public T_PlayerManager tutorialPlayerManager;

    public BoxCollider startCheckpointCollider;


    void Start()
    {
        startCheckpointCollider = GameObject.FindGameObjectWithTag("T_Checkpoint_1").GetComponent<BoxCollider>();
        tutorialPlayerManager = GameObject.Find("TutorialPlayer").GetComponent<T_PlayerManager>();
        tutorialPlayerManager.isFading = true;
    }

    public void CoroutineToDisable()
    {
        StartCoroutine(tutorialPlayerManager.WaitFadeout());  
    }
    public void DisableThisObject()
    {
        this.gameObject.SetActive(false);
    }
}
