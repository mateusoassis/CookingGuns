using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpacebarFadeout : MonoBehaviour
{
    public Button[] disableButtons;    

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
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
