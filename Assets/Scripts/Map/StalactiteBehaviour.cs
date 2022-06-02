using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteBehaviour : MonoBehaviour
{
    [SerializeField] private Transform player;
    private bool isPlayerInside;
    [SerializeField] _PlayerStats playerStats;
    [SerializeField] private int damage;
    [SerializeField] private bool doesDamageAfterEndRoom;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        playerStats = player.GetComponent<_PlayerStats>();
    }

    void Update()
    {
        if(player.GetComponent<_PlayerManager>().isEndRoomAnimation)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerInside = true;
        }

        if(other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerInside = false;
            Debug.Log("player saiu da estalactite");
        }
    }

    void OnDestroy()
    {
        if(isPlayerInside)
        {
            playerStats.TakeHPDamage(damage);
        }
    }
}
