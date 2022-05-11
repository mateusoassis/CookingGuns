using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPartKillTowerTarget : MonoBehaviour
{
    public ThirdPartKillTower thirdPartKillTower;
    public WindowContainer windowContainer;
    public int index;

    public int type;

     void Start()
    {
        thirdPartKillTower = GameObject.Find("3rd_KillTower").GetComponent<ThirdPartKillTower>();
        windowContainer = GameObject.Find("TutorialWindowContainer").GetComponent<WindowContainer>();
    }

    void OnDestroy()
    {
        if(type == 0)
        {
            thirdPartKillTower.tower = true;
            windowContainer.NextDialogue();
            windowContainer.NextDialogue();
            windowContainer.currentDialogueIndex = index;
            windowContainer.interact = true;
            windowContainer.openDialogue[index] = true;
        }
    }
}
