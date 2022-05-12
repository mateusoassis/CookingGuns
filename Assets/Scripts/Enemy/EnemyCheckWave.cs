using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheckWave : MonoBehaviour
{
    [SerializeField] private bool belongsToWave;

    private WaveManager waveManagerScript;

    private void OnDestroy()
    {
        CheckIfBelongsToWave();
    }

    private void CheckIfBelongsToWave() 
    {
        if (belongsToWave) 
        {
            Debug.Log("");
            //waveManagerScript.numberOfEnemies = 0;
        }
        else 
        {
            Debug.Log("Não faz parte de Wave");
        }
    }
}
