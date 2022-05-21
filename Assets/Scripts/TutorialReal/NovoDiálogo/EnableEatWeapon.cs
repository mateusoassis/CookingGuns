using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEatWeapon : MonoBehaviour
{
    public TutorialBrain tutorialBrain;
    public string targetTag;

    void Awake()
    {
        tutorialBrain = GameObject.Find("TutorialStuff").GetComponent<TutorialBrain>();
    }


    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == targetTag)
        {
            tutorialBrain.playerCanEatWeapon = true;
        }
    }
}
