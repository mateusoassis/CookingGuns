using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    public GameObject enemy4Prefab;

    [HideInInspector]
    public int enemiesKilled;
    private int enemiesMax;
    public bool roomCleared;

    [SerializeField] private int enemyNumberRandomizer;

    private int i;

    public PetBillboard petBillboard;
    public PetHandler petHandler;

    void Start()
    {
        enemyNumberRandomizer = Random.Range(3,9);
        enemiesKilled = 0;
        roomCleared = false;
        SpawnEnemies();
        petBillboard = GameObject.Find("PetCanvas").GetComponent<PetBillboard>();
        petHandler = GameObject.Find("Player").GetComponent<PetHandler>();
    }

    void Update()
    {
        if (enemiesKilled >= enemyNumberRandomizer && !roomCleared)
        {
            roomCleared = true;
            petBillboard.lockOnPlayer = true;
            if(petBillboard.lockOnPlayer)
            {
                petHandler.MoveTowardsPlayer();
                petBillboard.lockOnPlayer = false;
            }
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
        int enemyIndex = Random.Range(0,4);

        if(enemyIndex == 0)
        {
            Instantiate(enemy1Prefab, new Vector3(Random.Range(-13,13), 1.5f, Random.Range(-13,13)), Quaternion.identity);
            enemiesMax++;
        }
        else if(enemyIndex == 1)
        {
            Instantiate(enemy2Prefab, new Vector3(Random.Range(-13,13), 1.3f, Random.Range(-13,13)), Quaternion.identity);
            enemiesMax++;
        }
        else if(enemyIndex == 2)
        {
            Instantiate(enemy3Prefab, new Vector3(Random.Range(-13,13), 1.3f, Random.Range(-13,13)), Quaternion.identity);
            enemiesMax++;
        }
        else if(enemyIndex == 3)
        {
            Instantiate(enemy4Prefab, new Vector3(Random.Range(-13,13), 2.6f, Random.Range(-13,13)), Quaternion.identity);
            enemiesMax++;
        }
    }
}
