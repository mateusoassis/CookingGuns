using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Transform upPosition;
    public Transform downPosition;
    public EnemySpawner enemySpawner;
    public float speed;

    void Start()
    {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    void Update()
    {
        if(enemySpawner.roomCleared)
        {
            transform.position = Vector3.MoveTowards(transform.position, upPosition.position, speed * Time.deltaTime);
        }
    }
}
