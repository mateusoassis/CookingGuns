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
    [SerializeField] private GameObject stalactictBurstParticle;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        playerStats = player.GetComponent<_PlayerStats>();
    }

    void Start()
    {
        Destroy(gameObject, 1.7f);
    }

    void Update()
    {
        if(player.GetComponent<_PlayerManager>().isEndRoomAnimation)
        {
            isPlayerInside = false;
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerInside = true;
            Debug.Log("player entrou na estalactite");
        }

        if(other.gameObject.tag == "Ground")
        {
            //Instantiate(stalactictBurstParticle, transform.position, Quaternion.identity);
            //Destroy(gameObject);
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
        Instantiate(stalactictBurstParticle, transform.position, Quaternion.identity);
        if(isPlayerInside)
        {
            Debug.Log("destruiu");
            playerStats.TakeHPDamage(damage);
        }
    }
}
