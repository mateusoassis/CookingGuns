using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _AnimationHandler : MonoBehaviour
{
    public _PlayerManager playerManager;
    public int weapon;
    public Animator[] anim;

    public GameObject rollCat;

    void Start()
    { 
        GetWeaponInt();
    }
    
    public void GetWeaponInt()
    {
        weapon = playerManager.playerWeaponHandler.weaponEquipped;
    }
}
