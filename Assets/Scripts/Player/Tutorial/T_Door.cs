using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class T_Door : MonoBehaviour
{
    public Transform doorTarget;
    public Transform door;
    public bool enemyKilled;
    public bool lootDrops;
    public bool doorInPlace;
    public float doorTime;
    public float currentLerpTime;
    public float perc;

    public EnemySpawner enemySpawner;

    void Awake()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    public void Start()
    {
        doorInPlace = false;
        enemyKilled = false;
    }

    public void Update()
    {
        if(doorInPlace)
        {
            return;
        }
        else if(!doorInPlace && enemySpawner.roomCleared)
        {
            currentLerpTime += Time.deltaTime;
            perc = currentLerpTime / doorTime;

            door.position = Vector3.Lerp(door.position, doorTarget.position, perc);
        }  
        if(door.position == doorTarget.position)
        {
            doorInPlace = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("_1_RoomScene", LoadSceneMode.Single);
        }
    }
}
