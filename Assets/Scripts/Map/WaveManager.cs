using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //[SerializeField] public int numberOfEnemies;

    //[SerializeField] private GameObject[] enemyList;

    //[SerializeField] private Transform[] enemyPositions;

    //[SerializeField] private GameObject[] enemies;

    private WavesSpawn wavesSpawnScript;


    private void Awake()
    {
        wavesSpawnScript = GameObject.Find("WaveSpawner").GetComponent<WavesSpawn>();
    }
    private void Start()
    {
        //SpawnEnemies();
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    private void Update()
    {
        //numberOfEnemies = enemies.Length;
        if (transform.childCount <= 0)
        {
            wavesSpawnScript.StartCoroutine("ControlSpawn");
        }
    }
    /*private void SpawnEnemies()
    {
        for (int i = 0; i < enemyList.Length; i++)
        {
            Instantiate(enemyList[i], enemyPositions[i].position, Quaternion.identity);
            enemyList[i].transform.parent = gameObject.transform;
        }
    }*/
}
