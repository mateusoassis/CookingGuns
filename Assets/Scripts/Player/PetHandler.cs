using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PetHandler : MonoBehaviour
{
    private GameObject mainUI;
    [Header("Pet Settings")]
    private Transform pet;
    public Transform petModel;
    public GameObject pressEKey;
    //public NavMeshAgent petNavMeshAgent;
    
    private PetBillboard petBillboard;
    public bool playerOnArea;

    [Header("Crafting")]
    public bool craftingWindowOpen;
    public GameObject craftingWindowObject; // tem que arrastar pra o inspector
    private Inventory inventorytxt;
    private _PlayerManager playerManager;

    [Header("Controle de movimentação")]
    [SerializeField] private float sinRadius;
    private bool move;
    private bool arrived;
    [SerializeField] private float petSpeed;
    //public Vector3 moveTowards;
    [SerializeField] private Transform[] targetTransforms;
    [SerializeField] private int index;
    [SerializeField] private float moveToNextDelay;
    private float moveToNextDelayTimer;
    private bool stop;

    [Header("Troca de câmera e LookAt nos botões")]
    private CinemachineSwitchBlend cinemachineSwitchBlend;
    [SerializeField] private GameObject buttonsCanvasObject;
    private PetLookAt petLookAt;
    
    void Awake()
    {
        petModel = GameObject.Find("PetAirFryer").GetComponent<Transform>();
        pet = GameObject.Find("Pet").GetComponent<Transform>();
        pressEKey = GameObject.Find("ApertaE");
        cinemachineSwitchBlend = GameObject.Find("CinemachineCamSetup").GetComponent<CinemachineSwitchBlend>();
        buttonsCanvasObject = GameObject.Find("ButtonsCanvas");
        petLookAt = GameObject.Find("ButtonsCanvas").GetComponent<PetLookAt>();
        
    }

    void Start()
    {
        pressEKey.SetActive(false);
        mainUI = GameObject.Find("MainUI");
        playerManager = GameObject.Find("Player").GetComponent<_PlayerManager>();
        inventorytxt = GameObject.Find("Player").GetComponent<Inventory>();
        pet.transform.parent = null;
        petBillboard = GameObject.Find("PetCanvas").GetComponent<PetBillboard>();
        playerOnArea = false;    
        pet.transform.LookAt(targetTransforms[index].position, pet.transform.up);
    }

    public void HandlePet()
    {
        if(!playerManager.gameManager.pausedGame && !playerManager.isFading)
        {
            // sobe e desce senoidal
            if(!craftingWindowOpen)
            {
                Vector3 sinMovement = new Vector3(0f, Mathf.Sin(Time.time * 3f) * sinRadius, 0f);
                petModel.transform.position += sinMovement;

                if(stop)
                {
                    pet.transform.LookAt(new Vector3(transform.position.x, pet.transform.position.y, transform.position.z));
                }
            }
            else
            {
                petBillboard.DeactivateOnEnemiesKilled();
                if(!petLookAt.lookAtButton)
                {
                    petModel.LookAt(petLookAt.lookAtPosition);
                    petLookAt.lookAtButton = true;
                }
            }

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
        //craftingWindowObject.SetActive(true);
        //inventorytxt.UpdateItem();
        cinemachineSwitchBlend.SwitchPriority();
        pressEKey.SetActive(false);
        buttonsCanvasObject.SetActive(true);
    }
    public void CloseCraftingWindow()
    {
        craftingWindowOpen = false;
        //craftingWindowObject.SetActive(false);
        cinemachineSwitchBlend.SwitchPriority();
        pressEKey.SetActive(true);
        buttonsCanvasObject.SetActive(false);
        petModel.transform.forward = pet.transform.forward;
    }
}
