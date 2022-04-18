using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetHandler : MonoBehaviour
{
    public GameObject mainUI;
    public Transform pet;
    public Transform petChild;
    public NavMeshAgent petNavMeshAgent;
    public float sinRadius;
    private PetBillboard petBillboard;
    public bool playerOnArea;
    public bool craftingWindowOpen;
    public GameObject craftingWindowObject;
    public _PlayerManager playerManager;
    

    void Start()
    {
        mainUI = GameObject.Find("MainUI");
        playerManager = GameObject.Find("Player").GetComponent<_PlayerManager>();
        pet = GameObject.Find("Pet").GetComponent<Transform>();
        pet.transform.parent = null;
        petChild = pet.transform.GetChild(0);
        petNavMeshAgent = GameObject.Find("Pet").GetComponent<NavMeshAgent>();
        petBillboard = GameObject.Find("PetCanvas").GetComponent<PetBillboard>();
        playerOnArea = false;    
        craftingWindowObject = mainUI.transform.Find("CraftingWindow").gameObject;
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
        if(playerManager.sceneIndex != 1 && playerManager.sceneIndex != 2)
        {
            Vector3 moveTowards = new Vector3(transform.position.x, pet.transform.position.y, transform.position.z);
            petNavMeshAgent.SetDestination(moveTowards);
            petBillboard.ActivateOnEnemiesKilled();
        }  
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Pet")
        {
            playerOnArea = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Pet")
        {
            playerOnArea = false;
        }
    }

    public void OpenCraftingWindow()
    {
        craftingWindowOpen = true;
        craftingWindowObject.SetActive(true);
    }
    public void CloseCraftingWindow()
    {
        craftingWindowOpen = false;
        craftingWindowObject.SetActive(false);
    }
}
