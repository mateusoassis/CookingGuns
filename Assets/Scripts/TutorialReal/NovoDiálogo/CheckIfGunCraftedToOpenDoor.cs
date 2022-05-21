using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfGunCraftedToOpenDoor : MonoBehaviour
{
    private TutorialBrain tutorialBrain;

    [Header("Porta, velocidade e offset")]
    [SerializeField] private Transform targetDoor;
    public float speedToOpen;
    [SerializeField] private float yOffset;

    [Header("Ignora")]
    private Vector3 targetPos;

    void Awake()
    {
        tutorialBrain = GameObject.Find("TutorialStuff").GetComponent<TutorialBrain>();
    }

    void Start()
    {
        targetPos = targetDoor.position + new Vector3(0f, -yOffset, 0f);
    }

    void Update()
    {
        if(tutorialBrain.playerCraftedWeapon)
        {
            targetDoor.position = Vector3.MoveTowards(targetDoor.position, targetPos, speedToOpen * Time.deltaTime);
        }
    }
}
