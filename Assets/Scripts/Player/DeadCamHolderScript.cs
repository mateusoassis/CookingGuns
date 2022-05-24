using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DeadCamHolderScript : MonoBehaviour
{
    private Transform player;
    public CinemachineVirtualCamera deadVCAM;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }   
    void Start()
    {
        transform.position = player.position;
        deadVCAM.LookAt = player;
        deadVCAM.Follow = player;
    }
}
