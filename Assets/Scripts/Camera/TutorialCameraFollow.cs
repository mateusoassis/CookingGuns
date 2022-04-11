using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 fixedDistanceFromPlayer;

    void Start()
    {
        player = GameObject.Find("TutorialPlayer").GetComponent<Transform>();
        fixedDistanceFromPlayer = transform.position - player.position;
    }

    void Update()
    {
        transform.position = player.position + fixedDistanceFromPlayer;
    }
}
