using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCheckpoints : MonoBehaviour
{
    public bool isPlayerInside;
    public GameObject tutorialButtons;
    public bool playerLeft;
    public Button[] WASDButtons;

    // Start is called before the first frame update
    void Start()
    {
        playerLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerInside = true;
            if(!playerLeft && isPlayerInside)
            {
                tutorialButtons.SetActive(true);
            }
        }
        
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerInside = false;
            playerLeft = true;
            for(int i = 0; i < WASDButtons.Length; i++)
            {
                if(WASDButtons[i] != null)
                {
                    WASDButtons[i].interactable = false;
                }
            }
            //tutorialButtons.SetActive(false);
        }
    }
}
