using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCurrentDialogue : MonoBehaviour
{
    public WindowContainer tutorialWindowContainer;
    public bool closed;
    
    void Awake()
    {
        tutorialWindowContainer = GameObject.Find("TutorialWindowContainer").GetComponent<WindowContainer>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !closed)
        {
            tutorialWindowContainer.NextDialogue();
            tutorialWindowContainer.NextDialogue();
            closed = true;
        }
    }
}
