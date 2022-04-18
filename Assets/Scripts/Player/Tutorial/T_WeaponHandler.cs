using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class T_WeaponHandler : MonoBehaviour
{
    // 0 = pistola
    // 1 = shotgun
    // 2 = machinegun
    public int weaponEquipped;
    public T_PlayerManager tutorialPlayerManager;
    public Transform[] weaponIcons;
    public Image[] weaponImages;
    public bool[] unlockedWeapons;
    public GameObject[] weaponObjects;  

    void Awake()
    {
        tutorialPlayerManager = GetComponent<T_PlayerManager>();
        DisableAll();
        //weaponIcons = new Transform[3];
        //weaponImages = new Image[3];
        //unlockedWeapons = new bool[3];        
    }

    void Start()
    {   
        /*
        weaponIcons[0] = GameObject.Find("TutorialPistolIcon").GetComponent<Transform>();
        weaponIcons[1] = GameObject.Find("TutorialShotgunIcon").GetComponent<Transform>();
        weaponIcons[2] = GameObject.Find("TutorialMachineGunIcon").GetComponent<Transform>();
        
        weaponImages[0] = GameObject.Find("TutorialPistolIcon").GetComponent<Image>();
        weaponImages[1] = GameObject.Find("TutorialShotgunIcon").GetComponent<Image>();
        weaponImages[2] = GameObject.Find("TutorialMachineGunIcon").GetComponent<Image>();
        */


        /*
        axeImage.color = Color.red;
        pistolImage.color = Color.red;
        axeUnlocked = false;
        pistolUnlocked = false;
        */
        UnlockPistol();
        //DisableAll();
        
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
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(unlockedWeapons[0])
            {
                ActivatePistol_();
                WeaponManager(weaponEquipped);
            }  
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(unlockedWeapons[1])
            {
                ActivateShotgun_();
                WeaponManager(weaponEquipped);
            }     
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(unlockedWeapons[2])
            {
                ActivateMachineGun_();
                WeaponManager(weaponEquipped);
            }
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

    public void UnlockPistol()
    {
        unlockedWeapons[0] = true;
        UpdateGuns();
    }
    public void DisablePistol()
    {
        unlockedWeapons[0] = false;
        UpdateGuns();
    }

    public void UnlockShotgun()
    {
        unlockedWeapons[1] = true;
        UpdateGuns();
    }
    public void DisableShotgun()
    {
        unlockedWeapons[1] = false;
        UpdateGuns();
    }

    public void UnlockMachineGun()
    {
        unlockedWeapons[2] = true;
        UpdateGuns();
    }
    public void DisableMachineGun()
    {
        unlockedWeapons[2] = false;
        UpdateGuns();
    } 
    public void DisableAll()
    {
        for(int i = 0; i < weaponObjects.Length; i++)
        {
            weaponImages[i].color = Color.red;
            weaponObjects[i].SetActive(false);
        }
    }
    public void UpdateGuns()
    {
        for(int i = 0; i < weaponObjects.Length; i++)
        {
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
