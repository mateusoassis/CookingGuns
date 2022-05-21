using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallDialogueOnKill : MonoBehaviour
{
    [Header("Arrastar objeto com diálogo")]
    public DialogueBox dialogueBoxScript;

    void OnDestroy()
    {
        dialogueBoxScript.StartDialogue();
    }
}
