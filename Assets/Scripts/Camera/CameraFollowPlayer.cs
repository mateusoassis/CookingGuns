using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 fixedDistanceFromPlayer;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        fixedDistanceFromPlayer = transform.position - player.position;
    }

    void Update()
    {
        transform.position = player.position + fixedDistanceFromPlayer;
    }
}
