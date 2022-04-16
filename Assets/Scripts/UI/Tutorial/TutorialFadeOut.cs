using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialFadeOut : MonoBehaviour
{
    public T_PlayerManager tutorialPlayerManager;

    public BoxCollider startCheckpointCollider;
    public int sceneIndex;

    void Awake()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Start()
    {
        if(sceneIndex == 1)
        {
            startCheckpointCollider = GameObject.FindGameObjectWithTag("T_Checkpoint_1").GetComponent<BoxCollider>();
        }
        tutorialPlayerManager = GameObject.Find("TutorialPlayer").GetComponent<T_PlayerManager>();
        tutorialPlayerManager.isFading = true;
    }

    public void CoroutineToDisable()
    {
        if(sceneIndex == 1)
        {
            StartCoroutine(tutorialPlayerManager.WaitFadeout()); 
        }
        else if(sceneIndex == 2)
        {
            StartCoroutine(tutorialPlayerManager.WaitFadeoutTutorial2());
        }
    }
    public void DisableThisObject()
    {
        this.gameObject.SetActive(false);
    }
}
