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
        weaponIcons = new Transform[4];
        weaponImages = new Image[4];
        unlockedWeapons = new bool[4];        
    }

    void Start()
    {   
        weaponIcons[0] = GameObject.Find("PistolIcon").GetComponent<Transform>();
        weaponIcons[1] = GameObject.Find("ShotgunIcon").GetComponent<Transform>();
        weaponIcons[2] = GameObject.Find("MachineGunIcon").GetComponent<Transform>();
        weaponIcons[3] = GameObject.Find("GranadeLauncherIcon").GetComponent<Transform>();
        
        weaponImages[0] = GameObject.Find("PistolIcon").GetComponent<Image>();
        weaponImages[1] = GameObject.Find("ShotgunIcon").GetComponent<Image>();
        weaponImages[2] = GameObject.Find("MachineGunIcon").GetComponent<Image>();
        weaponImages[3] = GameObject.Find("GranadeLauncherIcon").GetComponent<Image>();

        UnlockPistol();
    }

    void Update()
    {
        
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
        if(Input.GetKeyDown(KeyCode.Alpha1) && !playerManager.isShooting)
        {
            ActivatePistol_();
            WeaponManager(weaponEquipped);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && !playerManager.isShooting)
        {
            ActivateShotgun_();
            WeaponManager(weaponEquipped);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3) && !playerManager.isShooting)
        {
            ActivateMachineGun_();
            WeaponManager(weaponEquipped);
        }else if(Input.GetKeyDown(KeyCode.Alpha4) && !playerManager.isShooting)
        {
            ActivateGranadeLauncher_();
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
    }
    public void ActivateMachineGun_()
    {
        weaponEquipped = 2;  
    }

    public void ActivateGranadeLauncher_()
    {
        weaponEquipped = 3;  
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
