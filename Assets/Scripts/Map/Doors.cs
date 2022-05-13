using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Transform upPosition;
    public Transform downPosition;
    public GameManager gameManagerScript;
    public float speed;

    void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(gameManagerScript.roomCleared)
        {
            transform.position = Vector3.MoveTowards(transform.position, upPosition.position, speed * Time.deltaTime);
        }
    }
}
