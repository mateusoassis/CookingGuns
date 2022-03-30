using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetBillboard : MonoBehaviour
{
    public Transform cam;
    public GameObject activateOnEnemiesKilled;
    public bool lockOnPlayer;
    public bool isItLocked;
    public EnemySpawner enemySpawner;

    void Start() 
    {
        lockOnPlayer = false;
        isItLocked = false;
        cam = GameObject.Find("Main Camera").GetComponent<Transform>(); 
    }  

    void LateUpdate()
    {
        if(!isItLocked)
        {
            if(lockOnPlayer)
            {
                isItLocked = true;
            }
        }
        transform.LookAt(transform.position + cam.forward);
    }

    public void DeactivateOnEnemiesKilled()
    {
        activateOnEnemiesKilled.SetActive(false);
    }
    public void ActivateOnEnemiesKilled()
    {
        activateOnEnemiesKilled.SetActive(true);
    }
}
