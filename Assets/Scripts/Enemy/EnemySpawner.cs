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

    [SerializeField] private int enemyNumberRandomizer;

    private int i;

    public PetBillboard petBillboard;

    void Start()
    {
        enemyNumberRandomizer = Random.Range(3,9);
        enemiesKilled = 0;
        roomCleared = false;
        SpawnEnemies();
        petBillboard = GameObject.Find("PetCanvas").GetComponent<PetBillboard>();
    }

    void Update()
    {
        if (enemiesKilled >= enemyNumberRandomizer && !roomCleared)
        {
            roomCleared = true;
            petBillboard.lockOnPlayer = true;
        }
    }

    void SpawnEnemies()
    {

        for(i = 0; i < enemyNumberRandomizer; i++)
        {
            if(enemiesMax < enemyNumberRandomizer){
                EnemySelector();
            }
        }
    }

    void EnemySelector()
    {
        int enemyIndex = Random.Range(0,3);

        if(enemyIndex == 0)
        {
            Instantiate(enemy1Prefab, new Vector3(Random.Range(-13,13),1.5f,Random.Range(-13,13)), Quaternion.identity);
            enemiesMax++;
        }
        else if(enemyIndex == 1)
        {
            Instantiate(enemy2Prefab, new Vector3(Random.Range(-13,13),1.5f,Random.Range(-13,13)), Quaternion.identity);
            enemiesMax++;
        }
        else if(enemyIndex == 2)
        {
            Instantiate(enemy3Prefab, new Vector3(Random.Range(-13,13),1.5f,Random.Range(-13,13)), Quaternion.identity);
            enemiesMax++;
        }
    }
}
