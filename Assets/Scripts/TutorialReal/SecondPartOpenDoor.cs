using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPartOpenDoor : MonoBehaviour
{
    public bool pudim;
    public bool shieldoca;

    public Transform door;
    public Vector3 targetPos;
    public float yOffset;
    public float speedToOpen;
    public bool open;
    public bool skipDialogueTwice;

    public WindowContainer windowContainer;

    void Awake()
    {
        windowContainer = GameObject.Find("TutorialWindowContainer").GetComponent<WindowContainer>();
    }
    
    void Start()
    {
        pudim = false;
        shieldoca = false;
        targetPos = door.position + new Vector3(0f, -yOffset, 0f);
    }

    void Update()
    {
        if(pudim && shieldoca)
        {
            open = true;
        }

        if(open)
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
            } 
            
        }
    }
}
