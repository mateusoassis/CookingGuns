using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "Cooking Guns/PlayerInfo", order = 5)]
public class PlayerInfo : ScriptableObject
{
    [Header("Usable Variables")]
    public bool hasPlayedTutorial;
    public bool isOnTutorial;
    public bool endedTutorial;
    public int playerCurrentRoom;
    public int healthFromLastRoom;
    // public int currentSceneIndex;

    [Header("Armas")]
    public int lastWeaponTypeEquipped;
    public bool[] unlockedGuns = new bool[4];
    public int lastSlotEquipped;
    public bool[] freeSlotArraySaved = new bool[3];
    public int[] weaponTypeOnSlotSaved = new int[3];
    

    [Header("Estatisticas")]
    // tutorial
    
    public int timeSpentOnTutorial;

    // armas
    public int totalWeaponsCrafted; //
    public int totalWeaponsEaten; //

    // inimigos
    public int totalEnemiesKilled; //
    public int mostEnemiesKilledOnSameRun;

    // player
    public int totalTimesRolled; //
    
    // tempo
    public int totalPlayedTime;
    public int fastestRunSoFar;



    public void ResetThisObject()
    {
        hasPlayedTutorial = false;
        timeSpentOnTutorial = 0;

        totalWeaponsCrafted = 0;
        totalWeaponsEaten = 0;

        totalEnemiesKilled = 0;
        mostEnemiesKilledOnSameRun = 0;

        totalTimesRolled = 0;

        totalPlayedTime = 0;
        fastestRunSoFar = 0;

        playerCurrentRoom = -1;
        healthFromLastRoom = -1;

        lastWeaponTypeEquipped = -1;
    }

    public void NewGameReset()
    {
        playerCurrentRoom = 0;
        healthFromLastRoom = -1;

        lastWeaponTypeEquipped = 0;
        unlockedGuns = new bool[4];

        lastSlotEquipped = 0;

        freeSlotArraySaved = new bool[3];
        freeSlotArraySaved[0] = false;
        freeSlotArraySaved[1] = true;
        freeSlotArraySaved[2] = true;

        weaponTypeOnSlotSaved = new int[3];
        weaponTypeOnSlotSaved[0] = 0;
        weaponTypeOnSlotSaved[1] = 4;
        weaponTypeOnSlotSaved[2] = 4;
    }

    public void TutorialReset()
    {
        playerCurrentRoom = 0;
        healthFromLastRoom = 0;

        lastWeaponTypeEquipped = 0;
        unlockedGuns = new bool[4];

        lastSlotEquipped = 0;

        freeSlotArraySaved = new bool[3];
        freeSlotArraySaved[0] = false;
        freeSlotArraySaved[1] = false;
        freeSlotArraySaved[2] = false;

        weaponTypeOnSlotSaved = new int[3];
        weaponTypeOnSlotSaved[0] = 2;
        weaponTypeOnSlotSaved[1] = 3;
        weaponTypeOnSlotSaved[2] = 1;
    }
}