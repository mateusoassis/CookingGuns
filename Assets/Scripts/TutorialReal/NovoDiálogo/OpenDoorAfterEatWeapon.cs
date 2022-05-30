using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorAfterEatWeapon : MonoBehaviour
{
    public bool callNextPetPosition;
    [Header("Preencher")] // referenciar o diálogo responsável pelo booleano que deixará a porta abrir
    //[SerializeField] string doorNameInHierarchy;
    [SerializeField] private Transform targetDoor;
    [SerializeField] private string targetTag;
    public float speedToOpen;
    [SerializeField] private float yOffset;

    [Header("Ignora")]
    private Vector3 targetPos;
    [SerializeField] private bool isPlayerInside;
    private bool checkIfNextPetPosition;

    private TutorialBrain tutorialBrain;

    void Awake()
    {
        //targetDoor = GameObject.Find(doorNameInHierarchy).GetComponent<Transform>();
        //dialogueBoxScript = GetComponent<DialogueBox>();
        tutorialBrain = GameObject.Find("TutorialStuff").GetComponent<TutorialBrain>();
    }

    void Start()
    {
        targetPos = targetDoor.position + new Vector3(0f, -yOffset, 0f);
        GetComponent<BoxCollider>().enabled = true;
    }

    void Update()
    {
        if(isPlayerInside && tutorialBrain.playerEatWeapon)
        {
            targetDoor.position = Vector3.MoveTowards(targetDoor.position, targetPos, speedToOpen * Time.deltaTime);
            if(!checkIfNextPetPosition)
            {
                tutorialBrain.dialogueAfterCraftedWeapon.gameManager.playerManager.petHandler.NextPetPosition();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == targetTag)
        {
            isPlayerInside = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == targetTag)
        {
            isPlayerInside = false;
        }
    }
}
