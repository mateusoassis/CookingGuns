using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WindowContainer : MonoBehaviour
{
    [Header("Referências")]
    public _PlayerManager playerManager;
    public ThirdPartKillTower thirdPartKillTower;


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
    public bool[] openDialogue;
    public bool[] closeDialogue;
    public bool[] endedDialogue;
    
    public bool isWindowUp;
    public bool waitingInput;
    public bool vanishingLastOne;
    public bool interact;

    void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<_PlayerManager>();
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            //thirdPartKillTower = GameObject.Find("3rd_KillTower").GetComponent<ThirdPartKillTower>();
        }
    }

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex != 3)
        {
            gameObject.SetActive(false);
        }
        Debug.Log("começa janela");
    }

    void Update()
    {
        if(interact)
        {
            HandleDialogues(currentDialogueIndex);
        }
        

        if(Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("apertei F");
            if(waitingInput)
            {
                NextDialogue();
            }
        }
    }


    public void NextDialogue()
    {
        if(waitingInput && interact)
        {
            Debug.Log("passa pro próximo");
            waitingInput = false;
        }
    }

    public void HandleDialogues(int i)
    {
        if(!playerManager.isFading)
        {
            if(!closeDialogue[i])
            {
                if(openDialogue[i] && !waitingInput)
                {
                    dialogueObjects[i].SetActive(true);
                    canvasAlpha += Time.deltaTime;
                    dialogueContainerCanvasGroup.alpha = canvasAlpha;
                    if(canvasAlpha >= 1)
                    {
                        waitingInput = true;
                        closeDialogue[i] = true;
                    }
                }
            }

           if(closeDialogue[i] && !waitingInput)
            {
                canvasAlpha -= Time.deltaTime;
                dialogueContainerCanvasGroup.alpha = canvasAlpha;
                if(Input.GetKeyDown(KeyCode.F))
                {
                    canvasAlpha = 0;
                    dialogueContainerCanvasGroup.alpha = canvasAlpha;
                }

                if(canvasAlpha <= 0)
                {
                    dialogueObjects[i].SetActive(false);
                    lastDialogueIndex = i;
                    endedDialogue[i] = true;
                    interact = false;
                }
            }
        }
    }
}
