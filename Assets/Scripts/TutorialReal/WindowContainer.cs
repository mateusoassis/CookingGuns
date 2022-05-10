using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WindowContainer : MonoBehaviour
{
    public float canvasAlpha;
    public CanvasGroup dialogueContainerCanvasGroup;
    public bool enabledAlpha;
    public bool switchState;

    [Header("Backgrounds")]
    public GameObject petName;
    public GameObject dialogueBackground;

    [Header("Diálogos")]
    public GameObject[] dialogueObjects;
    public int lastDialogueIndex;
    public int currentDialogueIndex;

    /*
    0 - andar
    1 - mirar_atirar
    2 - trocar_armas
    3 - rolar
    4 - comer
    5 - interagir_pet
    */

    public float transitionDurationTimer;
    public float transitionDuration;

    [Header("Booleanos de tutorial")]
    public bool[] dialogueBools;

    void Start()
    {
        Debug.Log("começa janela");
    }

    void Update()
    {
        if(switchState)
        {
            enabledAlpha = !enabledAlpha;
            switchState = false;
        }
        else
        {
            if(enabledAlpha && canvasAlpha < 1)
            {
                canvasAlpha += Time.deltaTime;
                dialogueContainerCanvasGroup.alpha = canvasAlpha;
            }
            else if(!enabledAlpha && canvasAlpha > 0)
            {
                canvasAlpha -= Time.deltaTime;
                dialogueContainerCanvasGroup.alpha = canvasAlpha;
            }
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            if(currentDialogueIndex < dialogueObjects.Length)
            {
                SwitchState();
            }
        }
    }

    public void SwitchState()
    {
        if(enabledAlpha)
        {
            lastDialogueIndex = currentDialogueIndex;
            switchState = true;
        }
        else
        {
            currentDialogueIndex++;
            if(dialogueObjects[currentDialogueIndex] != null)
            {
                if(lastDialogueIndex > -1)
                {
                    dialogueObjects[lastDialogueIndex].SetActive(false);
                }
                dialogueObjects[currentDialogueIndex].SetActive(true);
            }
            switchState = true;
        }
    }
}
