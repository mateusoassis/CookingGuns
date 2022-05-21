using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCollider : MonoBehaviour
{
    [Header("Index da conversa")]
    public int index;   
    /*
    0 - andar
    1 - mirar_atirar
    2 - trocar_armas
    3 - rolar
    4 - comer
    5 - interagir_pet
    */

    /*
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("encostou");
            if(other.gameObject.TryGetComponent(out _PlayerManager playerManager))
            {
                playerManager.tutorialWindowContainer.NextDialogue();
                playerManager.tutorialWindowContainer.NextDialogue();
                playerManager.tutorialWindowContainer.currentDialogueIndex = index;
                for(int i = 0; i < playerManager.tutorialWindowContainer.closeDialogue.Length; i++)
                {
                    if(i < index)
                    {
                        playerManager.tutorialWindowContainer.dialogueObjects[i].SetActive(false);
                    }
                }
                playerManager.tutorialWindowContainer.interact = true;
                playerManager.tutorialWindowContainer.openDialogue[index] = true;
            }
        }
    }
    */
}
