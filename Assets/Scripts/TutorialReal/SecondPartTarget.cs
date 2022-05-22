using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondPartTarget : MonoBehaviour
{
    /*
    public SecondPartOpenDoor secondPartOpenDoor;
    public WindowContainer windowContainer;
    public int scene;

    public int type;
    // 0 = pudim
    // 1 = shieldoca

    void Awake()
    {
        scene = SceneManager.GetActiveScene().buildIndex;
        if(scene == 3)
        {
            secondPartOpenDoor = GameObject.Find("2nd_PartColliders").GetComponent<SecondPartOpenDoor>();
            windowContainer = GameObject.Find("TutorialWindowContainer").GetComponent<WindowContainer>();
        }
    }

    void Start()
    {
        
        if(scene == 3)
        {
            secondPartOpenDoor = GameObject.Find("2nd_PartColliders").GetComponent<SecondPartOpenDoor>();
            windowContainer = GameObject.Find("TutorialWindowContainer").GetComponent<WindowContainer>();
        }
        
    }

    void OnDestroy()
    {
        if(type == 0 && scene == 3)
        {
            secondPartOpenDoor.pudim = true;
            windowContainer.NextDialogue();
        }
        else if(type == 1 && scene == 3)
        {
            secondPartOpenDoor.shieldoca = true;
            windowContainer.NextDialogue();
        }
    }
    */
}
