using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateMap : MonoBehaviour
{

    public EnemySpawner enemySpawner;
    public bool test;

    public GameManager gameManager;

    

    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void RoomSelector()
    {
        int roomNumber = Random.Range(0,6);

        if(enemySpawner.playerInfo.playerCurrentRoom == 1) // cena 4
        {
            SceneManager.LoadScene("5_RoomScene");
            enemySpawner.playerInfo.playerCurrentRoom = 2;
        }
        else if(enemySpawner.playerInfo.playerCurrentRoom == 2) // cena 5
        {
            SceneManager.LoadScene("3_RoomScene");
            enemySpawner.playerInfo.playerCurrentRoom = 3;
        }
        else if(enemySpawner.playerInfo.playerCurrentRoom == 3) // cena 3
        {
            SceneManager.LoadScene("2_RoomScene");
            enemySpawner.playerInfo.playerCurrentRoom = 4;
        }
        else if(enemySpawner.playerInfo.playerCurrentRoom == 4) // cena 2
        {
            SceneManager.LoadScene("1_RoomScene");
            enemySpawner.playerInfo.playerCurrentRoom = 5;
        }
        else if(enemySpawner.playerInfo.playerCurrentRoom == 5) // cena 1
        {
            gameManager.PauseGame();
            enemySpawner.thankYouForPlaying.SetActive(true);
        }

    }    

    public void TestRoomSelector()
    {
        SceneManager.LoadScene("1_RoomScene");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Door" && enemySpawner.roomCleared == true)
        {
            if(!test)
            {
                RoomSelector();
            }
            else if(test)
            {
                TestRoomSelector();
            }
        }
    }
}
