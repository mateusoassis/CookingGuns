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
    public int amountUnlocked;
    public GameObject[] weaponObjects;  

    void Awake()
    {
        playerManager = GetComponent<_PlayerManager>();
        weaponIcons = new Transform[4];
        weaponImages = new Image[4];
        //unlockedWeapons = new bool[4];

        weaponIcons[0] = GameObject.Find("PistolIcon").GetComponent<Transform>();
        weaponIcons[1] = GameObject.Find("ShotgunIcon").GetComponent<Transform>();
        weaponIcons[2] = GameObject.Find("MachineGunIcon").GetComponent<Transform>();
        weaponIcons[3] = GameObject.Find("GranadeLauncherIcon").GetComponent<Transform>();
        
        weaponImages[0] = GameObject.Find("PistolIcon").GetComponent<Image>();
        weaponImages[1] = GameObject.Find("ShotgunIcon").GetComponent<Image>();
        weaponImages[2] = GameObject.Find("MachineGunIcon").GetComponent<Image>();
        weaponImages[3] = GameObject.Find("GranadeLauncherIcon").GetComponent<Image>();
    }

    void Start()
    {   
        //if(playerManager.testing)
        //{
            //if(playerManager.sceneIndex > 0)
            //for(int i = 0; i < 4; i++){
            //    unlockedWeapons[i] = true;
            //}
            //else
            //{
            //    unlockedWeapons[0] = true;
            //    unlockedWeapons[1] = false;
            //    unlockedWeapons[2] = false;
            //    unlockedWeapons[3] = false;
            //}
        //}

        ActivatePistol_();
        WeaponManager(weaponEquipped);
        UpdateAmountUnlocked();
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
                if(unlockedWeapons[n])
                {
                    weaponObjects[i].SetActive(true);
                    weaponImages[i].color = Color.green;
                }
            }
            else if(i != n)
            {
                weaponObjects[i].SetActive(false);
                if(unlockedWeapons[i])
                {
                    weaponImages[i].color = Color.white;
                }
                else
                {
                    weaponImages[i].color = Color.red;
                }
            }
        }
    }

    public void SwitchGuns()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && !playerManager.isShooting && unlockedWeapons[0])
        {
            ActivatePistol_();
            WeaponManager(weaponEquipped);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && !playerManager.isShooting && unlockedWeapons[1])
        {
            ActivateShotgun_();
            WeaponManager(weaponEquipped);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3) && !playerManager.isShooting && unlockedWeapons[2])
        {
            ActivateMachineGun_();
            WeaponManager(weaponEquipped);
        }else if(Input.GetKeyDown(KeyCode.Alpha4) && !playerManager.isShooting && unlockedWeapons[3])
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
    
    public void EatWeapon(int n)
    {
        if(CountBool(unlockedWeapons, true) >= 2)
        {
            if(unlockedWeapons[n])
            {
                unlockedWeapons[n] = false;
                weaponEquipped = FindFirstTrueIndex(unlockedWeapons);
                WeaponManager(weaponEquipped);
            }
        }
    }

    public void HealFromEatingWeapon()
    {
        EatWeapon(weaponEquipped);
        playerManager.playerStats.heartScript.FullHeal();
    }

    public void UpdateAmountUnlocked()
    {
        amountUnlocked = CountBool(unlockedWeapons, true);
    }

    public static int CountBool(bool[] array, bool flag)
    {
        int n = 0;

        for(int i = 0; i < array.Length; i++)
        {
            if(array[i] == flag)
            {
                n++;
            }
        }
        return n;
    }

    public static int FindFirstTrueIndex(bool[] array)
    {
        int index = -1;
        for(int i = 0; i < array.Length; i++)
        {
            if(array[i] == true)
            {
                index = i;
                break;
            }
        }
        return index;
    }
}
