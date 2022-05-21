using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [Header("Preencher")] // referenciar o diálogo responsável pelo booleano que deixará a porta abrir
    [SerializeField] private DialogueBox dialogueBoxScript;
    [SerializeField] private string targetTag;
    //[SerializeField] string doorNameInHierarchy;
    [SerializeField] private Transform targetDoor;
    public float speedToOpen;
    [SerializeField] private float yOffset;

    [Header("Ignora")]
    private Vector3 targetPos;
    private bool open;
    public bool blockedOpen;

    void Awake()
    {
        //targetDoor = GameObject.Find(doorNameInHierarchy).GetComponent<Transform>();
        //dialogueBoxScript = GetComponent<DialogueBox>();
    }

    void Start()
    {
        targetPos = targetDoor.position + new Vector3(0f, -yOffset, 0f);
    }

    void Update()
    {
        if(dialogueBoxScript.ended && open && !blockedOpen)
        {
            targetDoor.position = Vector3.MoveTowards(targetDoor.position, targetPos, speedToOpen * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == targetTag)
        {
            open = true;
        }
    }
}
