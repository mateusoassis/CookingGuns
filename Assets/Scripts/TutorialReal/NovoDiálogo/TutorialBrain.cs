using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBrain : MonoBehaviour
{
    public bool playerCanEatWeapon;
    public bool playerEatWeapon;
    public bool playerCraftedWeapon;
    public bool lastDialogue;

    public DialogueBox dialogueAfterCraftedWeapon;

    public void SuccessfullyCraftedWeapon()
    {
        playerCraftedWeapon = true;
    }
}
