using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetHandler : MonoBehaviour
{
    public Transform pet;
    public Transform petChild;
    public NavMeshAgent petNavMeshAgent;
    public float sinRadius;
    private PetBillboard petBillboard;

    void Start()
    {
        pet = GameObject.Find("Pet").GetComponent<Transform>();
        pet.transform.parent = null;
        petChild = pet.transform.GetChild(0);
        petNavMeshAgent = GameObject.Find("Pet").GetComponent<NavMeshAgent>();
        petBillboard = GameObject.Find("PetCanvas").GetComponent<PetBillboard>();        
    }

    public void HandlePet()
    {
        if(!GetComponent<_PlayerManager>().gameManager.pausedGame) // referenciando o gamemanager que está no PLAYERMANAGER
        {
            Vector3 sinMovement = new Vector3(0f, Mathf.Sin(Time.time * 3f) * sinRadius, 0f);
            pet.transform.position += sinMovement;
        }
    }

    public void MoveTowardsPlayer()
    {
        Vector3 moveTowards = new Vector3(transform.position.x, pet.transform.position.y, transform.position.z);
        petNavMeshAgent.SetDestination(moveTowards);
        petBillboard.ActivateOnEnemiesKilled();
    }
}
