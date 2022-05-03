using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LMBFadeout : MonoBehaviour
{
    public Button lmbButton;

    void OnDestroy()
    {
        lmbButton.interactable = false;
        GetComponent<EnemyStats>().enemySpawner.roomCleared = true;
    }
    
}
