using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class _WeaponHandler : MonoBehaviour
{
    // 0 = pistola
    // 1 = shotgun
    // 2 = machinegun
    public int weaponEquipped;
    public _PlayerManager playerManager;
    public Transform[] weaponIcons;
    public Image[] weaponImages;
    public bool[] unlockedWeapons;
    public GameObject[] weaponObjects;  

    void Awake()
    {
        playerManager = GetComponent<_PlayerManager>();
        weaponIcons = new Transform[3];
        weaponImages = new Image[3];
        unlockedWeapons = new bool[3];        
    }

    void Start()
    {   
        weaponIcons[0] = GameObject.Find("PistolIcon").GetComponent<Transform>();
        weaponIcons[1] = GameObject.Find("ShotgunIcon").GetComponent<Transform>();
        weaponIcons[2] = GameObject.Find("MachineGunIcon").GetComponent<Transform>();
        
        weaponImages[0] = GameObject.Find("PistolIcon").GetComponent<Image>();
        weaponImages[1] = GameObject.Find("ShotgunIcon").GetComponent<Image>();
        weaponImages[2] = GameObject.Find("MachineGunIcon").GetComponent<Image>();


        /*
        axeImage.color = Color.red;
        pistolImage.color = Color.red;
        axeUnlocked = false;
        pistolUnlocked = false;
        */
        UnlockPistol();
    }

    void Update()
    {
        /*
        if(axeUnlocked)
        {
            axeImage.color = Color.white;
        }
        if(pistolUnlocked)
        {
            pistolImage.color = Color.white;
        }
        */
    }

    public void WeaponManager(int n)
    {
        for(int i = 0; i < weaponObjects.Length; i++)
        {
            if(i == n)
            {
                weaponObjects[i].SetActive(true);
                weaponImages[i].color = Color.green;
            }
            else
            {
                weaponObjects[i].SetActive(false);
                weaponImages[i].color = Color.white;
            }
        }
    }

    public void SwitchGuns()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivatePistol_();
            WeaponManager(weaponEquipped);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateShotgun_();
            WeaponManager(weaponEquipped);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateMachineGun_();
            WeaponManager(weaponEquipped);
        } 
    }
    public void ActivatePistol_()
    {
        weaponEquipped = 0;
    }
    public void ActivateShotgun_()
    {
        weaponEquipped = 1;
        // falta adicionar shotgun
    }
    public void ActivateMachineGun_()
    {
        weaponEquipped = 2;  
    }

    public void UnlockPistol()
    {
        unlockedWeapons[0] = true;
    }
    public void DisablePistol()
    {
        unlockedWeapons[0] = false;
    }

    public void UnlockShotgun()
    {
        unlockedWeapons[1] = true;
    }
    public void DisableShotgun()
    {
        unlockedWeapons[1] = false;
    }

    public void UnlockMachineGun()
    {
        unlockedWeapons[2] = true;
    }
    public void DisableMachineGun()
    {
        unlockedWeapons[2] = false;
    } 
}