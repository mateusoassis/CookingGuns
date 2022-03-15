using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform playerTransform;
    private Transform targetTransform;
    public int setBehaviour;
    public bool isPlayerOnRange;
    public Rigidbody enemyBulletPrefab;

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    // 1 = shooting + follow
    // 2 = shooting + retreat
    // 3 = follow + explode

    void Update()
    {
        
    }
}
