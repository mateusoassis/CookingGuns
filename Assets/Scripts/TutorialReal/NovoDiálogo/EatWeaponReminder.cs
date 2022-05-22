using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatWeaponReminder : MonoBehaviour
{
    public DialogueBox dialogueBox;

    public float reminderInterval;
    [SerializeField] private float reminderTimer;    

    public DialogueBox eatDialogue;
    public bool canStart;

    private _PlayerManager playerManager;
    private TutorialBrain tutorialBrain;


    void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<_PlayerManager>();
        tutorialBrain = GameObject.Find("TutorialStuff").GetComponent<TutorialBrain>();
    }

    void Start()
    {
        reminderTimer = reminderInterval;
    }

    void Update()
    {
        if(eatDialogue.ended && !canStart)
        {
            canStart = true;
        }
        
        if(canStart && !tutorialBrain.playerEatWeapon)
        {
            if(reminderTimer > 0)
            {
                reminderTimer -= Time.deltaTime;
            }
            else
            {
                dialogueBox.gameObject.SetActive(true);
                if(!playerManager.rmbHeldDown)
                {
                    dialogueBox.StartDialogue();
                }
                reminderTimer = reminderInterval;
            }
        }

        if(playerManager.rmbHeldDown)
        {
            reminderTimer = reminderInterval;
        }
    }
}
