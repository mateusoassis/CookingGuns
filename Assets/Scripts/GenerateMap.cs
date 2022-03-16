using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateMap : MonoBehaviour
{

    public EnemySpawner enemySpawner;

    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    void RoomSelector()
    {
        int roomNumber = Random.Range(0,5);

        if(roomNumber == 1)
        {
            SceneManager.LoadScene("2_RoomScene");
        }
        else if(roomNumber == 2)
        {
            SceneManager.LoadScene("3_RoomScene");
        }
        else if(roomNumber == 3)
        {
            SceneManager.LoadScene("4_RoomScene");
        }
        else if(roomNumber == 4)
        {
            SceneManager.LoadScene("5_RoomScene");
        }
        else if(roomNumber == 0)
        {
            SceneManager.LoadScene("1_RoomScene");
        }

    }    

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Door" && enemySpawner.roomCleared == true)
        {
            RoomSelector();
        }
    }
}
