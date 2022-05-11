using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPartTarget : MonoBehaviour
{
    public SecondPartOpenDoor secondPartOpenDoor;
    public WindowContainer windowContainer;

    public int type;
    // 0 = pudim
    // 1 = shieldoca

    void Start()
    {
        secondPartOpenDoor = GameObject.Find("2nd_PartColliders").GetComponent<SecondPartOpenDoor>();
        windowContainer = GameObject.Find("TutorialWindowContainer").GetComponent<WindowContainer>();
    }

    void OnDestroy()
    {
        if(type == 0)
        {
            secondPartOpenDoor.pudim = true;
            windowContainer.NextDialogue();
        }
        else if(type == 1)
        {
            secondPartOpenDoor.shieldoca = true;
            windowContainer.NextDialogue();
        }
    }
}
