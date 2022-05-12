using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateMap : MonoBehaviour
{

    public EnemySpawner enemySpawner;
    public bool test;

    public GameManager gameManager;
    public _PlayerStats playerStats;
    public GameObject mainUI;

    public bool healEveryRoom;
        

    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerStats = GetComponent<_PlayerStats>();
        mainUI = GameObject.Find("MainUI");
    }

    void RoomSelector()
    {
        
        int roomNumber = Random.Range(0,6);
        
        if(!playerStats.playerManager.testingCredits)
        {
            /*
            if(enemySpawner.playerInfo.playerCurrentRoom == 1) // cena 1
            {
                SceneManager.LoadScene("_2_RoomScene");
                if(!healEveryRoom)
                {
                    enemySpawner.playerInfo.healthFromLastRoom = playerStats.playerCurrentHealth;
                }
                else
                {
                    enemySpawner.playerInfo.healthFromLastRoom = playerStats.playerMaxHealth;
                }
            }
            else if(enemySpawner.playerInfo.playerCurrentRoom == 2) // cena 2
            {
                SceneManager.LoadScene("_3_RoomScene");
                if(!healEveryRoom)
                {
                    enemySpawner.playerInfo.healthFromLastRoom = playerStats.playerCurrentHealth;
                }
                else
                {
                    enemySpawner.playerInfo.healthFromLastRoom = playerStats.playerMaxHealth;
                }
            }
            else if(enemySpawner.playerInfo.playerCurrentRoom == 3) // cena 3
            {
                SceneManager.LoadScene("_4_RoomScene");
                if(!healEveryRoom)
                {
                    enemySpawner.playerInfo.healthFromLastRoom = playerStats.playerCurrentHealth;
                }
                else
                {
                    enemySpawner.playerInfo.healthFromLastRoom = playerStats.playerMaxHealth;
                }
            }
            else if(enemySpawner.playerInfo.playerCurrentRoom == 4) // cena 4
            {
                SceneManager.LoadScene("_5_RoomScene");
                if(!healEveryRoom)
                {
                    enemySpawner.playerInfo.healthFromLastRoom = playerStats.playerCurrentHealth;
                }
                else
                {
                    enemySpawner.playerInfo.healthFromLastRoom = playerStats.playerMaxHealth;
                }
            }
            else if(enemySpawner.playerInfo.playerCurrentRoom == 5) // cena 5
            {
                gameManager.PauseGame();
                mainUI.GetComponent<ThankYouHolder>().thankYouWindow.SetActive(true);
                gameManager.playerManager.endGame = true;
                enemySpawner.playerInfo.playerCurrentRoom = 0;
                enemySpawner.playerInfo.healthFromLastRoom = 0;
            }
            */
        }
        else
        {
            gameManager.CreditsScene();
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
                Debug.Log("porta");
                RoomSelector();
            }
            else if(test)
            {
                TestRoomSelector();
            }
        }

        if(other.gameObject.tag == "T_Door")
        {
            GetComponent<_PlayerManager>().playerInfo.isOnTutorial = false;
            SceneManager.LoadScene("Room_1_Bugada", LoadSceneMode.Single);
        }
    }
}
