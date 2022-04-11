using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Objects : MonoBehaviour
{
    public TutorialManager tutorialManager;

    void Start()
    {
        tutorialManager = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
    }
}
