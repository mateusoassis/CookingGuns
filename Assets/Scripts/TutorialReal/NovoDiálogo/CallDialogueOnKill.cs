using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallDialogueOnKill : MonoBehaviour
{
    public DialogueBox dialogueBoxScript;
    public GameManager gameManager;
    public bool startedDialogue;

    void Awake()
    {
        dialogueBoxScript = GetComponent<DialogueBox>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
