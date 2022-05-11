using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Vector3 targetPos;
    public float yOffset;
    public float speedToOpen;
    public int targetIndex;
    public BoxCollider targetBoxColliderToDisable;

    public WindowContainer tutorialWindowContainer;

    public bool open;

    void Awake()
    {
        tutorialWindowContainer = GameObject.Find("TutorialWindowContainer").GetComponent<WindowContainer>();
    }

    void Start()
    {
        targetPos = targetBoxColliderToDisable.gameObject.transform.position + new Vector3(0f, -yOffset, 0f);
    }

    void Update()
    {
        if(open && tutorialWindowContainer.endedDialogue[targetIndex])
        {
            targetBoxColliderToDisable.enabled = false;
            targetBoxColliderToDisable.gameObject.transform.position = Vector3.MoveTowards(targetBoxColliderToDisable.gameObject.transform.position, targetPos, speedToOpen * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            open = true;
            tutorialWindowContainer.NextDialogue();
            tutorialWindowContainer.NextDialogue();
        }
    }
}
