using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollReload : MonoBehaviour
{
    [SerializeField] private _PlayerShooting[] playerShooting;
    [SerializeField] private PlayerInfo playerInfo;

    // Update is called once per frame
    void Update()
    {
        if(playerShooting[playerInfo.lastWeaponTypeEquipped].reloading)
        {
            playerShooting[playerInfo.lastWeaponTypeEquipped].UpdateReloadBar();
        }
    }
}
