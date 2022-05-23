using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class PetHandler : MonoBehaviour
{
    private GameObject mainUI;
    [Header("Pet Settings")]
    private Transform pet;
    public Transform petModel;
    public GameObject pressFKey;
    //public NavMeshAgent petNavMeshAgent;
    
    public PetBillboard petBillboard;
    public bool playerOnArea;

    [Header("Crafting")]
    public bool craftingWindowOpen;
    public GameObject craftingWindowObject; // tem que arrastar pra o inspector
    private Inventory inventorytxt;
    public _PlayerManager playerManager;

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
    private Quaternion targetRotation;
    [SerializeField] private float lookAtSpeed;
    private float switchTimer;
    [SerializeField] private Animator canvasGroupAnimator;

    void Awake()
    {
        petModel = GameObject.Find("PetAirFryer").GetComponent<Transform>();
        pet = GameObject.Find("Pet").GetComponent<Transform>();
        pressFKey = GameObject.Find("F_Container");
        cinemachineSwitchBlend = GameObject.Find("CinemachineCamSetup").GetComponent<CinemachineSwitchBlend>();
        buttonsCanvasObject = GameObject.Find("ButtonsCanvas");
        petLookAt = GameObject.Find("ButtonsCanvas").GetComponent<PetLookAt>();
        
    }

    void Start()
    {
        pressFKey.SetActive(false);
        mainUI = GameObject.Find("MainUI");
        inventorytxt = GameObject.Find("Player").GetComponent<Inventory>();
        pet.transform.parent = null;
        petBillboard = GameObject.Find("PetCanvas").GetComponent<PetBillboard>();
        playerOnArea = false;    
        pet.transform.LookAt(targetTransforms[index].position, pet.transform.up);
        canvasGroupAnimator = buttonsCanvasObject.GetComponent<Animator>();
    }

    public void HandlePet()
    {
        if(!playerManager.gameManager.pausedGame && !playerManager.isFading)
        {
            // sobe e desce senoidal
            if(!craftingWindowOpen)
            {
                /*
                Vector3 sinMovement = new Vector3(0f, Mathf.Sin(Time.time * 3f) * sinRadius, 0f);
                petModel.transform.position += sinMovement;
                */

                if(stop)
                {
                    //Vector3 lookAtVector = Vector3.Lerp(new Vector3(transform.position.x, pet.transform.position))
                    pet.transform.LookAt(new Vector3(transform.position.x, pet.transform.position.y, transform.position.z));
                }
            }
            else
            {
                petBillboard.DeactivateOnEnemiesKilled();
                if(!petLookAt.lookAtButton)
                {
                    targetRotation = Quaternion.LookRotation(petLookAt.lookAtPosition - petModel.transform.position);
                    petModel.transform.rotation = Quaternion.Lerp(petModel.transform.rotation, targetRotation, lookAtSpeed * Time.deltaTime);
                    /*
                    petModel.LookAt(petLookAt.lookAtPosition);
                    petLookAt.lookAtButton = true;
                    */
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
        //craftingWindowObject.SetActive(true);
        //inventorytxt.UpdateItem();
        craftingWindowOpen = true;
        cinemachineSwitchBlend.SwitchPriority();
        //pet.transform.LookAt(new Vector3(transform.position.x, pet.transform.position.y, transform.position.z));
        targetRotation = Quaternion.LookRotation(petLookAt.lookAtPosition - transform.position);
        petLookAt.lookAtPosition = petLookAt.playerPos.position;
        pressFKey.SetActive(false);
        buttonsCanvasObject.SetActive(true);
        //StartCoroutine(DisableCanvasGroup(cinemachineSwitchBlend.mainToPetDuration));
    }
    public void CloseCraftingWindow()
    {
        craftingWindowOpen = false;
        //craftingWindowObject.SetActive(false);
        cinemachineSwitchBlend.SwitchPriority();
        pressFKey.SetActive(true);
        //buttonsCanvasObject.SetActive(false);
        //StartCoroutine(EnableCanvasGroup(cinemachineSwitchBlend.petToMainDuration));
        canvasGroupAnimator.SetTrigger("Disable");
        petModel.transform.forward = pet.transform.forward;
    }

    /*
    public IEnumerator EnableCanvasGroup(float duration)
    {
        buttonsCanvasObject.SetActive(true);
        switchTimer = 0f;
        petLookAt.canvasGroup.alpha = 0f;
        while(petLookAt.canvasGroup.alpha < 1)
        {
            switchTimer += Time.deltaTime / duration;
            petLookAt.canvasGroup.alpha = Mathf.MoveTowards(petLookAt.canvasGroup.alpha, 1f, switchTimer);
            break;
        }
        yield return new WaitForSeconds(0.1f);
        //buttonsCanvasObject.SetActive(true);
    }

    public IEnumerator DisableCanvasGroup(float duration)
    {
        switchTimer = 0f;
        petLookAt.canvasGroup.alpha = 1f;
        while(petLookAt.canvasGroup.alpha > 0)
        {
            switchTimer += Time.deltaTime / duration;
            petLookAt.canvasGroup.alpha = Mathf.MoveTowards(petLookAt.canvasGroup.alpha, 0f, switchTimer);
            //petLookAt.canvasGroup.alpha -= Time.deltaTime;
            break;
        }
        yield return new WaitForSeconds(0.1f);
        buttonsCanvasObject.SetActive(false);
    }
    */
}
