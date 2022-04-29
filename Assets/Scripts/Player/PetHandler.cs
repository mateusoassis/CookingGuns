using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetHandler : MonoBehaviour
{
    public GameObject mainUI;
    public Transform pet;
    public GameObject pressEKey;
    //public NavMeshAgent petNavMeshAgent;
    public float sinRadius;
    private PetBillboard petBillboard;
    public bool playerOnArea;
    public bool craftingWindowOpen;
    public GameObject craftingWindowObject;
    public Inventory inventorytxt;
    public _PlayerManager playerManager;

    [Header("Controle de movimentação")]
    public bool move;
    public bool arrived;
    public float petSpeed;
    //public Vector3 moveTowards;
    public Transform[] targetTransforms;
    public int index;
    public float moveToNextDelay;
    public float moveToNextDelayTimer;
    public bool stop;
    
    void Awake()
    {
        pressEKey = GameObject.Find("ApertaE");
    }

    void Start()
    {
        pressEKey.SetActive(false);
        mainUI = GameObject.Find("MainUI");
        playerManager = GameObject.Find("Player").GetComponent<_PlayerManager>();
        inventorytxt = GameObject.Find("Player").GetComponent<Inventory>();
        pet = GameObject.Find("Pet").GetComponent<Transform>();
        pet.transform.parent = null;
        petBillboard = GameObject.Find("PetCanvas").GetComponent<PetBillboard>();
        playerOnArea = false;    
    }

    public void HandlePet()
    {
        if(!playerManager.gameManager.pausedGame && !playerManager.isFading)
        {
            // sobe e desce senoidal
            Vector3 sinMovement = new Vector3(0f, Mathf.Sin(Time.time * 3f) * sinRadius, 0f);
            pet.transform.position += sinMovement;

            if(pet.transform.position != targetTransforms[index].position && index < targetTransforms.Length && !arrived && !stop)
            {
                pet.transform.position = Vector3.MoveTowards(pet.transform.position, targetTransforms[index].position, petSpeed * Time.deltaTime);

                if(targetTransforms[index].position == pet.transform.position)
                {
                    if(index == (targetTransforms.Length - 1))
                    {
                        stop = true;
                    }
                    else
                    {
                        index++;
                        pet.transform.LookAt(targetTransforms[index].position);
                    }
                    moveToNextDelayTimer = moveToNextDelay;
                    arrived = true;
                }
            }

            // isso já vai servir pro futuro quando for implementar o pet só andar pra próxima parte quando chegar perto do pet, NÃO TIRA
            if(arrived)
            {
                moveToNextDelayTimer -= Time.deltaTime;
                if(moveToNextDelayTimer < 0)
                {
                    arrived = false;
                }
            }

            if(stop)
            {
                pet.transform.LookAt(new Vector3(transform.position.x, pet.transform.position.y, transform.position.z));
            }

            /*
            if(move && !arrived)
            {
                moveTowards = new Vector3(transform.position.x, pet.transform.position.y, transform.position.z);
                if(Vector3.Distance(pet.transform.position, transform.position) > 3f)
                {
                    pet.transform.position = Vector3.Lerp(pet.transform.position, transform.position, petSpeed * Time.deltaTime * Time.deltaTime);    
                }
            }
            */

            
        }
    }

    /*
    public void MoveTowardsPlayer()
    {
        
        if(playerManager.playerInfo.playerCurrentRoom >= 1 && !arrived)
        {
            //Vector3 moveTowards = new Vector3(transform.position.x, pet.transform.position.y, transform.position.z);
            //petNavMeshAgent.SetDestination(moveTowards);
            //if(Vector3.Distance(transform.position, moveTowards) > 3f)
            //{
            //     transform.position = Vector3.Lerp(transform.position, moveTowards.normalized, Time.deltaTime * Time.deltaTime * Vector3.Distance(transform.position, moveTowards));    
            //}
            //else
            //{
            //move = false;
            //}
            //transform.position = Vector3.Lerp(transform.position, moveTowards.normalized, Time.deltaTime * Time.deltaTime * Vector3.Distance(transform.position, moveTowards));
            //petBillboard.ActivateOnEnemiesKilled();
            move = true;
        }  
    }
    */

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
        inventorytxt.UpdateItem();
    }
    public void CloseCraftingWindow()
    {
        craftingWindowOpen = false;
        craftingWindowObject.SetActive(false);
    }
}
