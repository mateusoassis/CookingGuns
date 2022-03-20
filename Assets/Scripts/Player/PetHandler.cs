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

    void Start()
    {
        pet = GameObject.Find("Pet").GetComponent<Transform>();
        pet.transform.parent = null;
        petChild = pet.transform.GetChild(0);
        petNavMeshAgent = GameObject.Find("Pet").GetComponent<NavMeshAgent>();
    }

    public void HandlePet()
    {
        Vector3 moveTowards = new Vector3(transform.position.x, pet.transform.position.y, transform.position.z);
        petNavMeshAgent.SetDestination(moveTowards);

        Vector3 sinMovement = new Vector3(0f, Mathf.Sin(Time.time * 3f) * sinRadius, 0f);
        pet.transform.position += sinMovement;
    }
}
