using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;

    [HideInInspector]
    public int enemiesKilled;
    private int enemiesMax;
    public bool roomCleared;

    private int i;

    void Start()
    {
        enemiesKilled = 0;
        roomCleared = false;
        SpawnEnemies();
    }

    void Update()
    {
        if (enemiesKilled == 3)
        {
            roomCleared = true;
        }
    }

    void SpawnEnemies()
    {
        for(i = 0; i < 4; i++)
        {
            if(enemiesMax < 4){
                EnemySelector();
            }
        }
    }

    void EnemySelector()
    {
        int enemyIndex = Random.Range(0,2);

        if(enemyIndex == 0)
        {
            Instantiate(enemy1Prefab, new Vector3(Random.Range(-12,12),1.5f,Random.Range(-12,12)), Quaternion.identity);
            enemiesMax++;
        }
        else if(enemyIndex == 1)
        {
            Instantiate(enemy2Prefab, new Vector3(Random.Range(-12,12),1.5f,Random.Range(-12,12)), Quaternion.identity);
            enemiesMax++;
        }
        else if(enemyIndex == 2)
        {
            Instantiate(enemy3Prefab, new Vector3(Random.Range(-12,12),1.5f,Random.Range(-12,12)), Quaternion.identity);
            enemiesMax++;
        }
    }
}
