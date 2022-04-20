using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public _PlayerManager playerManager;
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    public GameObject enemy4Prefab;
    public GameObject enemy5Prefab;

    public Transform[] roomPositions;

    //[HideInInspector]
    public int enemiesKilled;
    private int enemiesMax;
    public bool roomCleared;

    public int currentRoom;
    // 0 = tutorial
    // 1 = sala 4
    // 2 = sala 5
    // 3 = sala 3
    // 4 = sala 2
    // 5 = sala 1

    public PlayerInfo playerInfo;

    [SerializeField] private int totalEnemies;

    private int i;

    public PetBillboard petBillboard;
    public PetHandler petHandler;

    public GameObject thankYouForPlaying;

    void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<_PlayerManager>();
    }

    void Start()
    {
        //totalEnemies = Random.Range(3,9);
        enemiesKilled = 0;
        roomCleared = false;
        if(!playerManager.testing)
        {
            //SpawnEnemies();
        }
        petBillboard = GameObject.Find("PetCanvas").GetComponent<PetBillboard>();
        petHandler = GameObject.Find("Player").GetComponent<PetHandler>();
    }

    void Update()
    {
        if(!playerManager.testing)
        {
            if (enemiesKilled >= totalEnemies && !roomCleared)
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
        else if(playerManager.testing && !roomCleared)
        {
            
        }
    }

    void SpawnEnemies()
    {

        for(i = 0; i < totalEnemies; i++)
        {
            if(enemiesMax < totalEnemies){
                EnemySelector();
            }
        }
    }

    void EnemySelector()
    {
        int enemyIndex = Random.Range(0,5);

        if(enemyIndex == 0)
        {
            Instantiate(enemy1Prefab, new Vector3(Random.Range(-13,13), 1.3f, Random.Range(-13,13)), Quaternion.identity);
            enemiesMax++;
        }
        else if(enemyIndex == 1)
        {
            Instantiate(enemy2Prefab, new Vector3(Random.Range(-13,13), 1.3f, Random.Range(-13,13)), Quaternion.identity);
            enemiesMax++;
        }
        else if(enemyIndex == 2)
        {
            Instantiate(enemy3Prefab, new Vector3(Random.Range(-13,13), 0f, Random.Range(-13,13)), Quaternion.identity);
            enemiesMax++;
        }
        else if(enemyIndex == 3)
        {
            Instantiate(enemy4Prefab, new Vector3(Random.Range(-13,13), 0f, Random.Range(-13,13)), Quaternion.identity);
            enemiesMax++;
        }
        else if(enemyIndex == 4)
        {
            Instantiate(enemy5Prefab, new Vector3(Random.Range(-13,13), 0f, Random.Range(-13,13)), Quaternion.identity);
            enemiesMax++;
        }
    }
}
