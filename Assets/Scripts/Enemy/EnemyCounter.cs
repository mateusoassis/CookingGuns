using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private GameObject[] numberOfEnemies;

    [SerializeField] private GameObject waveManager;

    [SerializeField] private GameManager gameManagerScript;


    void Start()
    {
        numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        waveManager = GameObject.Find("WaveManager");
    }

    void Update()
    {
        if (transform.childCount == 0 && waveManager == null)
        {
            gameManagerScript.roomCleared = true;
            Debug.Log("Finalizou");
        }
    }
}
