using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EatWeaponDialogue : MonoBehaviour
{
    [Header("Referências")]
    private CanvasGroup canvasGroup;
    private GameObject canvasGroupObject;
    private TextMeshProUGUI dialogueText;
    private GameManager gameManager;

    [Header("Texto")]
    public string[] dialogueString;
    public float typeInterval;
    public float skipDuration;
    private float skipTimer;

    [Header("Reminder to eat weapon")]
    public float reminderInterval;
    public float reminderTimer;
    public bool eatenWeapon;

    [Header("Ignore abaixo")]
    private int index;
    private bool started;
    public bool ended;
    private bool canSkip;

    void Awake()
    {
        canvasGroupObject = GameObject.Find("TutorialWindowContainer");
        canvasGroup = canvasGroupObject.GetComponent<CanvasGroup>();
        dialogueText = GameObject.Find("DialogoTexto").GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        skipTimer = skipDuration;
    }

    void Start()
    {
        canSkip = false;
        dialogueText.text = string.Empty;
    }

    void Update()
    {
        if((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F)) && canSkip)
        {
            if(dialogueText.text == dialogueString[index])
            {
                NextDialogue();
                //RefreshSkipTimer();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueString[index];
                RefreshSkipTimer();
            }
        }

        if(started)
        {
            if(skipTimer > 0 && !canSkip)
            {
                skipTimer -= Time.unscaledDeltaTime;
            }
            else
            {
                canSkip = true;
            }
        }
        
    }

    public void RefreshSkipTimer()
    {
        skipTimer = skipDuration;
        canSkip = false;
    }

    public void StartDialogue()
    {
        dialogueText.text = string.Empty;
        started = true;
        gameManager.PauseForDialogue();
        canvasGroupObject.SetActive(true);
        canvasGroup.alpha = 1;
        index = 0;
        StartCoroutine(TypeDialogue());
    }

    public IEnumerator TypeDialogue()
    {
        foreach(char c in dialogueString[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(typeInterval);
        }
    }

    public void NextDialogue()
    {
        if(index < dialogueString.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeDialogue());
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroupObject.SetActive(false);
            gameManager.ResumeForDialogue();
            ended = true;
            gameObject.SetActive(false);
        }
    }
}
