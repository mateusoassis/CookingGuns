using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    public int damageDone;
    [SerializeField] private GameObject hitParticle;
    private BoxCollider colliderForWeapon;
    
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        colliderForWeapon = GetComponent<BoxCollider>();
    }
}
