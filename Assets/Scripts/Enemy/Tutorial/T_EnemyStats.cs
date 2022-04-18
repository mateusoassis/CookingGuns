using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T_EnemyStats : MonoBehaviour
{
    public T_Door doorScript;
    public Button leftMouseClickButton;
    

    void OnDestroy() 
    {
        doorScript.enemyKilled = true;     
        leftMouseClickButton.interactable = false;
    }
}

