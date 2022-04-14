using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCheckpoints : MonoBehaviour
{
    public bool isPlayerInside;
    public GameObject enableButtons;
    public bool playerLeft;
    public Button[] disableButtons;

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
                enableButtons.SetActive(true);
            }
        }
        
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerInside = false;
            playerLeft = true;
            for(int i = 0; i < disableButtons.Length; i++)
            {
                if(disableButtons[i] != null)
                {
                    disableButtons[i].interactable = false;
                }
            }
        }
    }
}
