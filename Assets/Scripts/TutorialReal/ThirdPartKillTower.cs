using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPartKillTower : MonoBehaviour
{
    public bool tower;
    public int index;

    public Transform door;
    public Vector3 targetPos;
    public float yOffset;
    public float speedToOpen;
    public bool open;
    public bool openDialogue;
    public bool skipDialogueTwice;
    public bool craftedAnyWeapon;

    public WindowContainer windowContainer;
    public PlayerInfo playerInfo;

    void Awake()
    {
        playerInfo.isOnTutorial = true;
        windowContainer = GameObject.Find("TutorialWindowContainer").GetComponent<WindowContainer>();
    }

    void Start()
    {
        tower = false;
        targetPos = door.position + new Vector3(0f, -yOffset, 0f);
    }

    void Update()
    {
        if(tower && !openDialogue)
        {
            openDialogue = true;
            open = true;

            windowContainer.NextDialogue();
            windowContainer.NextDialogue();
            windowContainer.currentDialogueIndex = index;
            for(int i = 0; i < windowContainer.closeDialogue.Length; i++)
            {
                if(i < index)
                {
                    windowContainer.dialogueObjects[i].SetActive(false);
                }
            }
            windowContainer.interact = true;
            windowContainer.openDialogue[index] = true;
        }

        if(open && craftedAnyWeapon)
        {
            if(!skipDialogueTwice)
            {
                windowContainer.NextDialogue();
                windowContainer.NextDialogue();
                skipDialogueTwice = true;
            }
            else
            {
                door.position = Vector3.MoveTowards(door.position, targetPos, speedToOpen * Time.deltaTime);
                playerInfo.endedTutorial = true;
                playerInfo.hasPlayedTutorial = true;
            }
        }
    }
}
