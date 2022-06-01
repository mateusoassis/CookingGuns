using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PetHandler : MonoBehaviour
{
    private GameObject mainUI;
    [Header("Pet Settings")]
    private Transform pet;
    public Transform petModel;
    public GameObject pressFKey;
    private Animator petAnimator;
    private bool endAnimation;
    //public NavMeshAgent petNavMeshAgent;
    
    public PetBillboard petBillboard;
    public bool playerOnArea;

    [Header("Crafting")]
    public bool craftingWindowOpen;
    public GameObject craftingWindowObject; // tem que arrastar pra o inspector
    private Inventory inventorytxt;
    public _PlayerManager playerManager;
    [SerializeField] private PetWindowBrain petWindowBrain;

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
    public bool stop;

    [Header("Troca de câmera e LookAt nos botões")]
    public CinemachineSwitchBlend cinemachineSwitchBlend;
    [SerializeField] private GameObject buttonsCanvasObject;
    private PetLookAt petLookAt;
    private Quaternion targetRotation;
    [SerializeField] private float lookAtSpeed;
    private float switchTimer;
    [SerializeField] private Animator canvasGroupAnimator;
    [SerializeField] private CanvasGroup fadeoutCanvasGroupWhenPetWindowOpen;

    void Awake()
    {
        petModel = GameObject.Find("PetAirFryer").GetComponent<Transform>();
        pet = GameObject.Find("Pet").GetComponent<Transform>();
        pressFKey = GameObject.Find("F_Container");
        cinemachineSwitchBlend = GameObject.Find("CinemachineCamSetup").GetComponent<CinemachineSwitchBlend>();
        buttonsCanvasObject = GameObject.Find("ButtonsCanvas");
        petLookAt = GameObject.Find("ButtonsCanvas").GetComponent<PetLookAt>();
        fadeoutCanvasGroupWhenPetWindowOpen = GameObject.Find("MainUI").GetComponent<CanvasGroup>();
        petAnimator = pet.GetComponent<Animator>();
        petWindowBrain = pet.GetComponent<PetWindowBrain>();
        
    }

    public void PetIdle()
    {
        petAnimator.SetBool("Idle", true);
        petAnimator.SetBool("Walking", false);
        petAnimator.SetBool("LookAt", false);
    }
    public void PetWalking()
    {
        petAnimator.SetBool("Idle", false);
        petAnimator.SetBool("Walking", true);
        petAnimator.SetBool("LookAt", false);
    }
    public void PetEndRoom()
    {
        petAnimator.SetBool("Idle", false);
        petAnimator.SetBool("Walking", false);
        petAnimator.SetBool("LookAt", false);
        petAnimator.SetTrigger("EndRoom");
    }
    public void IdleAfterEndRoom()
    {
        petAnimator.SetBool("Idle", true);
    }
    public void PetLookAtAnimation()
    {
        petAnimator.SetBool("Idle", false);
        petAnimator.SetBool("Walking", false);
        petAnimator.SetBool("LookAt", true);
    }

    void Start()
    {
        PetIdle();
        pressFKey.SetActive(false);
        mainUI = GameObject.Find("MainUI");
        inventorytxt = GameObject.Find("Player").GetComponent<Inventory>();
        pet.transform.parent = null;
        petBillboard = GameObject.Find("PetCanvas").GetComponent<PetBillboard>();
        playerOnArea = false;    
        pet.transform.LookAt(targetTransforms[index].position, pet.transform.up);
        canvasGroupAnimator = buttonsCanvasObject.GetComponent<Animator>();
        arrived = true;
        stop = false;

        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            index = 0;
        }
    }

    public void HandlePet()
    {
        if(SceneManager.GetActiveScene().buildIndex != 3)
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
                        /*
                        if(playerManager.gameManager.roomCleared)
                        {
                            PetEndRoom();
                        }
                        else
                        {
                            PetIdle();
                        }
                        */
                        pet.transform.LookAt(new Vector3(transform.position.x, pet.transform.position.y, transform.position.z));
                        Debug.Log("143");
                    }
                }
                else
                {
                    petBillboard.DeactivateOnEnemiesKilled();
                    /*
                    if(petLookAt.lookAtButton)
                    {
                        */
                        targetRotation = Quaternion.LookRotation(petLookAt.lookAtPosition - petModel.transform.position);
                        petModel.transform.rotation = Quaternion.Lerp(petModel.transform.rotation, targetRotation, lookAtSpeed * Time.deltaTime);
                        Debug.Log("152");
                        //petLookAt.lookAtButton = true;
                        /*
                        petModel.LookAt(petLookAt.lookAtPosition);
                        petLookAt.lookAtButton = true;
                        */
                        /*
                    }
                    else
                    {*/
                        
                    //}
                }

                if(pet.transform.position != targetTransforms[index].position && index < targetTransforms.Length && !arrived && !stop && !craftingWindowOpen)
                {
                    pet.transform.position = Vector3.MoveTowards(pet.transform.position, targetTransforms[index].position, petSpeed * Time.deltaTime);
                    PetWalking();

                    if(targetTransforms[index].position == pet.transform.position)
                    {
                        if(index == (targetTransforms.Length - 1))
                        {
                            stop = true;
                            endAnimation = true;
                            PetIdle();
                        }
                        else
                        {
                            index++;
                            pet.transform.LookAt(targetTransforms[index].position);
                            Debug.Log("178");
                        }
                        moveToNextDelayTimer = moveToNextDelay;
                        arrived = true;
                        PetIdle();
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

                if(stop && playerManager.gameManager.roomCleared && !craftingWindowOpen)
                {
                    pressFKey.SetActive(true);
                    if(endAnimation)
                    {
                        PetEndRoom();
                        endAnimation = false;
                    }
                }
                else
                {
                    pressFKey.SetActive(false);
                }
                
            }
        }
        else
        {
            if(!playerManager.gameManager.pausedGame && !playerManager.isFading)
            {
                if(!craftingWindowOpen)
                {
                    if(stop || arrived)
                    {
                        pet.transform.LookAt(new Vector3(transform.position.x, pet.transform.position.y, transform.position.z));
                    }
                }
                else
                {
                    petBillboard.DeactivateOnEnemiesKilled();
                    /*
                    if(petLookAt.lookAtButton)
                    {
                        */
                        targetRotation = Quaternion.LookRotation(petLookAt.lookAtPosition - petModel.transform.position);
                        petModel.transform.rotation = Quaternion.Lerp(petModel.transform.rotation, targetRotation, lookAtSpeed * Time.deltaTime);
                        Debug.Log("152");
                        //petLookAt.lookAtButton = true;
                        /*
                        petModel.LookAt(petLookAt.lookAtPosition);
                        petLookAt.lookAtButton = true;
                        */
                        /*
                    }
                    else
                    {*/
        
                    //}
                }

                if(pet.transform.position != targetTransforms[index].position && index < targetTransforms.Length && !arrived && !stop && !craftingWindowOpen)
                {
                    pet.transform.position = Vector3.MoveTowards(pet.transform.position, targetTransforms[index].position, petSpeed * Time.deltaTime);
                    pet.transform.LookAt(targetTransforms[index].position);
                    PetWalking();

                    if(targetTransforms[index].position == pet.transform.position)
                    {
                        if(index == (targetTransforms.Length - 1))
                        {
                            stop = true;
                            endAnimation = true;
                            PetIdle();
                        }
                        else
                        {
                            index++;
                            //pet.transform.LookAt(targetTransforms[index].position);
                        }
                        //moveToNextDelayTimer = moveToNextDelay;
                        arrived = true;
                        pet.transform.LookAt(new Vector3(transform.position.x, pet.transform.position.y, transform.position.z));
                        // tem que chamar o NextPetPosition() dos diálogos do tutorial quando ele acaba
                        PetIdle();
                    }
                }
                
                /*
                if(arrived)
                {
                    moveToNextDelayTimer -= Time.deltaTime;
                    if(moveToNextDelayTimer < 0)
                    {
                        arrived = false;
                    }
                }
                */

                if(stop && playerManager.gameManager.roomCleared && !craftingWindowOpen)
                {
                    pressFKey.SetActive(true);
                    if(endAnimation)
                    {
                        PetEndRoom();
                        endAnimation = false;
                    }
                }
                else
                {
                    pressFKey.SetActive(false);
                }
            }
        }
    }

    public void NextPetPosition(int i)
    {
        index = i;
        arrived = false;
        //index = i;
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
        PetLookAtAnimation();
        playerManager.playerMovement.StopMovingForPetHandlerCraftingWindow();
        //craftingWindowObject.SetActive(true);
        //inventorytxt.UpdateItem();
        fadeoutCanvasGroupWhenPetWindowOpen.alpha = 0f;
        playerManager.gameManager.DisableCursors();
        craftingWindowOpen = true;
        cinemachineSwitchBlend.SwitchPriority();
        //pet.transform.LookAt(new Vector3(transform.position.x, pet.transform.position.y, transform.position.z));
        targetRotation = Quaternion.LookRotation(petLookAt.lookAtPosition - transform.position);
        petLookAt.lookAtPosition = petLookAt.playerPos.position;
        //pressFKey.SetActive(false);
        buttonsCanvasObject.SetActive(true);
        petWindowBrain.UpdateIngredientAmount();
        //StartCoroutine(DisableCanvasGroup(cinemachineSwitchBlend.mainToPetDuration));
    }
    public void CloseCraftingWindow()
    {
        if(!playerManager.gameManager.pausedGame)
        {
            petLookAt.lookAtButton = false;
            petLookAt.lookAtPosition = petLookAt.playerPos.position;
            PetIdle();
            fadeoutCanvasGroupWhenPetWindowOpen.alpha = 1f;
            playerManager.gameManager.EnableCursors();
            if(SceneManager.GetActiveScene().buildIndex == 3 && playerManager.tutorialBrain.playerCraftedWeapon && !playerManager.tutorialBrain.lastDialogue)
            {
                playerManager.tutorialBrain.dialogueAfterCraftedWeapon.StartDialogue();
                playerManager.tutorialBrain.lastDialogue = true;
            }
            craftingWindowOpen = false;
            //craftingWindowObject.SetActive(false);
            cinemachineSwitchBlend.SwitchPriority();
            //pressFKey.SetActive(true);
            //buttonsCanvasObject.SetActive(false);
            //StartCoroutine(EnableCanvasGroup(cinemachineSwitchBlend.petToMainDuration));
            canvasGroupAnimator.SetTrigger("Disable");
            petModel.transform.forward = pet.transform.forward;
            petWindowBrain.CloseAll();
        }  
    }
}
