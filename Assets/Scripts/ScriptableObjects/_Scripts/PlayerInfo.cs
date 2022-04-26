using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInfo", menuName = "Cooking Guns/PlayerInfo", order = 5)]
public class PlayerInfo : ScriptableObject
{
    [Header("Usable Variables")]
    public bool hasPlayedTutorial;
    public int playerCurrentRoom;
    public int healthFromLastRoom;
    public int lastWeaponEquipped;

    public int currentSceneIndex;

    [Header("Estatisticas")]
    // tutorial
    
    public int timeSpentOnTutorial;

    // armas
    public int totalWeaponsCrafted;
    public int totalWeaponsEaten;

    // inimigos
    public int totalEnemiesKilled;
    public int mostEnemiesKilledOnSameRun;

    // player
    public int totalTimesRolled;
    
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

        lastWeaponEquipped = -1;
    }

    public void NewGameReset()
    {
        playerCurrentRoom = -1;
        healthFromLastRoom = -1;
        lastWeaponEquipped = -1;
    }
}