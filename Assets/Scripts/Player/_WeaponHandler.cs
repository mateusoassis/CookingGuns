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
    // 3 = lança granada
    // 4 = slot vazio, sem arma craftada
    public int weaponTypeEquipped;
    public _PlayerManager playerManager;
    public Transform[] weaponIcons;
    private Image[] weaponImages;
    public bool[] unlockedWeapons;
    public int amountUnlocked;
    public GameObject[] weaponObjects;  
    public BreakWeapon breakWeaponScript;

    public GameObject testingGameObjects;
    public GameObject realGameObjects;

    [Header("Arrays e Ints da nova HUD")]
    public int slotEquipped;
    public bool[] freeSlotArray;
    public int[] weaponTypeOnSlot;
    public Sprite[] realWeaponIconsPool;
    public Image[] realWeaponIcons;

    void Awake()
    {
        playerManager = GetComponent<_PlayerManager>();
        testingGameObjects = GameObject.Find("TestingWeaponIcons");
        realGameObjects = GameObject.Find("RealWeaponIcons");

        weaponImages = new Image[4];

        breakWeaponScript = GameObject.Find("ModeloArma").GetComponent<BreakWeapon>();

        weaponImages[0] = GameObject.Find("PistolIcon").GetComponent<Image>();
        weaponImages[1] = GameObject.Find("ShotgunIcon").GetComponent<Image>();
        weaponImages[2] = GameObject.Find("MachineGunIcon").GetComponent<Image>();
        weaponImages[3] = GameObject.Find("GranadeLauncherIcon").GetComponent<Image>();
        if(playerManager.testingWeapons)
        {
            realGameObjects.SetActive(false);
            for(int i = 0; i < unlockedWeapons.Length; i++)
            {
                unlockedWeapons[i] = true;
            }
        }
        else
        {
            testingGameObjects.SetActive(false);
            unlockedWeapons = new bool[4];
            for(int i = 0; i < realWeaponIcons.Length; i++)
            {
                realWeaponIcons[i] = GameObject.Find("Weapon" + (i+1) + "_Slot").GetComponent<Image>();
            }
        }
    }

    void Start()
    {   
        UnlockPistol();
        ActivatePistol_();
        WeaponManager(weaponTypeEquipped);
    }

    void Update()
    {
        // checa o conteúdo do TIPO DE ARMA NO SLOT pra trocar diretamente o sprite
        for(int i = 0; i < weaponTypeOnSlot.Length; i++)
        {
            if(weaponTypeOnSlot[i] == 0)
            {
                realWeaponIcons[i].sprite = realWeaponIconsPool[0]; // pistola
            }
            else if(weaponTypeOnSlot[i] == 1)
            {
                realWeaponIcons[i].sprite = realWeaponIconsPool[1]; // shotgun
            }
            else if(weaponTypeOnSlot[i] == 2)
            {
                realWeaponIcons[i].sprite = realWeaponIconsPool[2]; // machinegun
            }
            else if(weaponTypeOnSlot[i] == 3)
            {
                realWeaponIcons[i].sprite = realWeaponIconsPool[3]; // grenade launcher
            }
            else if(weaponTypeOnSlot[i] == 4)
            {
                realWeaponIcons[i].sprite = realWeaponIconsPool[4]; // vazio
            }
        }
    }

    public void Roll()
    {
        weaponObjects[weaponTypeEquipped].SetActive(false);
        playerManager.animationHandler.rollCat.SetActive(true);
    }
    public void EndRoll()
    {
        weaponObjects[weaponTypeEquipped].SetActive(true);
    }

    public void WeaponManager(int n)
    {
        if(playerManager.testingWeapons)
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
        else
        if(!playerManager.testingWeapons)
        {
            for(int i = 0; i < weaponObjects.Length; i++)
            {
                if(i == n)
                {
                    weaponObjects[i].SetActive(true);
                }
                else
                {
                    weaponObjects[i].SetActive(false);
                }
            }
        }
    }

    public void NextWeapon()
    {   
        Image[] tempButtonImage = new Image[3];
        if(CountBool(freeSlotArray, true) < 2)
        {
            Debug.Log("troca de arma");
            int newSlot = slotEquipped + 1;
            if(newSlot > 2)
            {
                newSlot = 0;
                if(freeSlotArray[newSlot])
                {
                    newSlot++;
                }
            }

            if(freeSlotArray[newSlot])
            {
                slotEquipped = newSlot+1;
                if(slotEquipped > 2)
                {
                    slotEquipped = 0;
                }
            }
            
            if(!freeSlotArray[newSlot])
            {
                slotEquipped = newSlot;
            }
            
            if(slotEquipped > 2)
            {
                slotEquipped = 0;
            }

            if(!freeSlotArray[slotEquipped])
            {
                weaponTypeEquipped = weaponTypeOnSlot[slotEquipped];
                WeaponManager(weaponTypeEquipped);
            }
            UpdateWeaponSlotSprites();
        }
    }

    public void SwitchToNextAvailableWeapon()
    {
        
        int newSlot = FindFirstFalseIndex(freeSlotArray);
        slotEquipped = newSlot;

        weaponTypeEquipped = weaponTypeOnSlot[slotEquipped];
        WeaponManager(weaponTypeEquipped);

        /*
        UpdateWeaponSlotSprites();
        */
    }

    public void UpdateWeaponSlotSprites()
    {
        /*
        int firstFalseIndex = FindFirstFalseIndex(freeSlotArray);
        int secondFalseIndex = FindSecondFalseIndex(freeSlotArray);
        int numberOfFalseIndexes = CountBool(freeSlotArray, false);

        if(numberOfFalseIndexes == 3)
        {
            if(slotEquipped == 0)
            {
                realWeaponIcons[0].sprite = realWeaponIconsPool[weaponTypeOnSlot[0]];
                realWeaponIcons[1].sprite = realWeaponIconsPool[weaponTypeOnSlot[1]];
                realWeaponIcons[2].sprite = realWeaponIconsPool[weaponTypeOnSlot[2]];
            }
            else if(slotEquipped == 1)
            {
                realWeaponIcons[0].sprite = realWeaponIconsPool[weaponTypeOnSlot[1]];
                realWeaponIcons[1].sprite = realWeaponIconsPool[weaponTypeOnSlot[2]];
                realWeaponIcons[2].sprite = realWeaponIconsPool[weaponTypeOnSlot[0]];
            }
            else if(slotEquipped == 2)
            {
                realWeaponIcons[0].sprite = realWeaponIconsPool[weaponTypeOnSlot[2]];
                realWeaponIcons[1].sprite = realWeaponIconsPool[weaponTypeOnSlot[0]];
                realWeaponIcons[2].sprite = realWeaponIconsPool[weaponTypeOnSlot[1]];
            }
        }
        else if(numberOfFalseIndexes == 2)
        {
            if(firstFalseIndex == 0)
            {
                if(secondFalseIndex == 1)
                {
                    if(slotEquipped == 0)
                    {
                        realWeaponIcons[0].sprite = realWeaponIconsPool[weaponTypeOnSlot[0]];
                        realWeaponIcons[1].sprite = realWeaponIconsPool[weaponTypeOnSlot[1]];
                        realWeaponIcons[2].sprite = realWeaponIconsPool[weaponTypeOnSlot[2]];
                    }
                    else if(slotEquipped == 1)
                    {
                        realWeaponIcons[0].sprite = realWeaponIconsPool[weaponTypeOnSlot[1]];
                        realWeaponIcons[1].sprite = realWeaponIconsPool[weaponTypeOnSlot[0]];
                        realWeaponIcons[2].sprite = realWeaponIconsPool[weaponTypeOnSlot[2]];
                    }
                    
                }
                else if(secondFalseIndex == 2)
                {
                    if(slotEquipped == 0)
                    {
                        realWeaponIcons[0].sprite = realWeaponIconsPool[weaponTypeOnSlot[0]];
                        realWeaponIcons[1].sprite = realWeaponIconsPool[weaponTypeOnSlot[2]];
                        realWeaponIcons[2].sprite = realWeaponIconsPool[weaponTypeOnSlot[2]];
                    }
                    else if(slotEquipped == 2)
                    {
                        realWeaponIcons[0].sprite = realWeaponIconsPool[weaponTypeOnSlot[2]];
                        realWeaponIcons[1].sprite = realWeaponIconsPool[weaponTypeOnSlot[0]];
                        realWeaponIcons[2].sprite = realWeaponIconsPool[weaponTypeOnSlot[1]];
                    }
                }
            }
            else if(firstFalseIndex == 1)
            {
                if(slotEquipped == 1)
                {
                    realWeaponIcons[0].sprite = realWeaponIconsPool[weaponTypeOnSlot[1]];
                    realWeaponIcons[1].sprite = realWeaponIconsPool[weaponTypeOnSlot[2]];
                    realWeaponIcons[2].sprite = realWeaponIconsPool[weaponTypeOnSlot[0]];
                }
                else if(slotEquipped == 2)
                {
                    realWeaponIcons[0].sprite = realWeaponIconsPool[weaponTypeOnSlot[2]];
                    realWeaponIcons[1].sprite = realWeaponIconsPool[weaponTypeOnSlot[1]];
                    realWeaponIcons[2].sprite = realWeaponIconsPool[weaponTypeOnSlot[0]];
                }
            }
        }
        else if(numberOfFalseIndexes == 1)
        {
            if(firstFalseIndex == 0)
            {
                realWeaponIcons[0].sprite = realWeaponIconsPool[weaponTypeOnSlot[0]];
                realWeaponIcons[1].sprite = realWeaponIconsPool[weaponTypeOnSlot[1]];
                realWeaponIcons[2].sprite = realWeaponIconsPool[weaponTypeOnSlot[2]];
            }
            else if(firstFalseIndex == 1)
            {
                realWeaponIcons[0].sprite = realWeaponIconsPool[weaponTypeOnSlot[1]];
                realWeaponIcons[1].sprite = realWeaponIconsPool[weaponTypeOnSlot[2]];
                realWeaponIcons[2].sprite = realWeaponIconsPool[weaponTypeOnSlot[0]];
            }
            else if(slotEquipped == 2)
            {
                realWeaponIcons[0].sprite = realWeaponIconsPool[weaponTypeOnSlot[2]];
                realWeaponIcons[1].sprite = realWeaponIconsPool[weaponTypeOnSlot[0]];
                realWeaponIcons[2].sprite = realWeaponIconsPool[weaponTypeOnSlot[1]];
            }
        }
        */
    }

    public void PreviousWeapon()
    {
        if(CountBool(freeSlotArray, true) < 2)
        {
            Debug.Log("troca de arma");
            int newSlot = slotEquipped - 1;
            if(newSlot < 0)
            {
                newSlot = 2;
                if(freeSlotArray[newSlot])
                {
                    newSlot--;
                }
            }

            if(freeSlotArray[newSlot])
            {
                slotEquipped = newSlot - 1;
                if(slotEquipped < 0)
                {
                    slotEquipped = 2;
                }
            }
            
            if(!freeSlotArray[newSlot])
            {
                slotEquipped = newSlot;
            }
            
            if(slotEquipped < 0)
            {
                slotEquipped = 2;
            }

            if(!freeSlotArray[slotEquipped])
            {
                weaponTypeEquipped = weaponTypeOnSlot[slotEquipped];
                WeaponManager(weaponTypeEquipped);
            }

            UpdateWeaponSlotSprites();
        }
    }

    public void SwitchGuns()
    {
        if(playerManager.testingWeapons)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1) && !playerManager.isShooting && unlockedWeapons[0])
            {
                ActivatePistol_();
                WeaponManager(weaponTypeEquipped);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2) && !playerManager.isShooting && unlockedWeapons[1])
            {
                ActivateShotgun_();
                WeaponManager(weaponTypeEquipped);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3) && !playerManager.isShooting && unlockedWeapons[2])
            {
                ActivateMachineGun_();
                WeaponManager(weaponTypeEquipped);
            }else if(Input.GetKeyDown(KeyCode.Alpha4) && !playerManager.isShooting && unlockedWeapons[3])
            {
                ActivateGranadeLauncher_();
                WeaponManager(weaponTypeEquipped);
            } 
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.X) && !playerManager.isShooting)
            {
                PreviousWeapon();
            }
            else if(Input.GetKeyDown(KeyCode.C) && !playerManager.isShooting)
            {
                NextWeapon();
            }
        }
        
    }
    public void ActivatePistol_()
    {
        weaponTypeEquipped = 0;
    }
    public void ActivateShotgun_()
    {
        weaponTypeEquipped = 1;
    }
    public void ActivateMachineGun_()
    {
        weaponTypeEquipped = 2;  
    }

    public void ActivateGranadeLauncher_()
    {
        weaponTypeEquipped = 3;  
    }

    public void UnlockPistol()
    {
        if(playerManager.testingWeapons)
        {
            unlockedWeapons[0] = true;
        }
        else
        {
            int slotFree = FindFirstTrueIndex(freeSlotArray);
            if(slotFree >= 0)
            {
                freeSlotArray[slotFree] = false;
                weaponTypeOnSlot[slotFree] = 0;
                realWeaponIcons[slotFree].sprite = realWeaponIconsPool[0];
            }
        }
        UpdateAmountUnlocked();
        UpdateWeaponSlotSprites();
    }
    public void DisablePistol()
    {
        if(playerManager.testingWeapons)
        {
            unlockedWeapons[0] = false;
        }
        else
        {
            freeSlotArray[slotEquipped] = true;
            weaponTypeOnSlot[slotEquipped] = 4;
            //realWeaponIcons[slotEquipped].sprite = realWeaponIconsPool[4];
            UpdateWeaponSlotSprites();
        }
    }

    public void UnlockShotgun()
    {
        if(playerManager.testingWeapons)
        {
            unlockedWeapons[1] = true;
        }
        else
        {
            int slotFree = FindFirstTrueIndex(freeSlotArray);
            if(slotFree >= 0)
            {
                freeSlotArray[slotFree] = false;
                weaponTypeOnSlot[slotFree] = 1;
                //realWeaponIcons[slotFree].sprite = realWeaponIconsPool[1];
                UpdateWeaponSlotSprites();
            }
        }
        UpdateAmountUnlocked();
        //UpdateWeaponSlotSprites();
    }
    public void DisableShotgun()
    {
        if(playerManager.testingWeapons)
        {
            unlockedWeapons[1] = false;
        }
        else
        {
            freeSlotArray[slotEquipped] = true;
            weaponTypeOnSlot[slotEquipped] = 4;
            //realWeaponIcons[slotEquipped].sprite = realWeaponIconsPool[4];
            UpdateWeaponSlotSprites();
        }
    }

    public void UnlockMachineGun()
    {
        if(playerManager.testingWeapons)
        {
            unlockedWeapons[2] = true;
        }
        else
        {
            int slotFree = FindFirstTrueIndex(freeSlotArray);
            if(slotFree >= 0)
            {
                freeSlotArray[slotFree] = false;
                weaponTypeOnSlot[slotFree] = 2;
                //realWeaponIcons[slotFree].sprite = realWeaponIconsPool[2];
                UpdateWeaponSlotSprites();
            }
        }
        UpdateAmountUnlocked();
        //UpdateWeaponSlotSprites();
    }
    public void DisableMachineGun()
    {
        if(playerManager.testingWeapons)
        {
            unlockedWeapons[2] = false;
        }
        else
        {
            freeSlotArray[slotEquipped] = true;
            weaponTypeOnSlot[slotEquipped] = 4;
            //realWeaponIcons[slotEquipped].sprite = realWeaponIconsPool[4];
            UpdateWeaponSlotSprites();
        }
    } 

    public void UnlockGrenadeLauncher()
    {
        if(playerManager.testingWeapons)
        {
            unlockedWeapons[3] = true;
        }
        else
        {
            int slotFree = FindFirstTrueIndex(freeSlotArray);
            if(slotFree >= 0)
            {
                freeSlotArray[slotFree] = false;
                weaponTypeOnSlot[slotFree] = 3;
                //realWeaponIcons[slotFree].sprite = realWeaponIconsPool[3];
                UpdateWeaponSlotSprites();
            }
        }
        UpdateAmountUnlocked();
        //UpdateWeaponSlotSprites();
    }
    public void DisableGrenadeLauncher()
    {
        if(playerManager.testingWeapons)
        {
            unlockedWeapons[3] = false;
        }
        else
        {
            freeSlotArray[slotEquipped] = true;
            weaponTypeOnSlot[slotEquipped] = 4;
            //realWeaponIcons[slotEquipped].sprite = realWeaponIconsPool[4];
            UpdateWeaponSlotSprites();
        }
    }
    
    public void EatWeapon(int n)
    {
        if(CountBool(unlockedWeapons, true) >= 2)
        {
            if(unlockedWeapons[n])
            {
                breakWeaponScript.BreakTheWeapon();
                unlockedWeapons[n] = false;
                weaponTypeEquipped = FindFirstTrueIndex(unlockedWeapons);
                WeaponManager(weaponTypeEquipped);
            }
        }
    }

    public void HealFromEatingWeapon()
    {
        if(playerManager.testingWeapons)
        {
            EatWeapon(weaponTypeEquipped);
        
        }
        else
        {
            if(weaponTypeEquipped == 0)
            {
                DisablePistol();
            }
            else if(weaponTypeEquipped == 1)
            {
                DisableShotgun();
            }
            else if(weaponTypeEquipped == 2)
            {
                DisableMachineGun();
            }
            else if(weaponTypeEquipped == 3)
            {
                DisableGrenadeLauncher();
            }
        }
        playerManager.playerStats.heartScript.FullHeal();
        
        SwitchToNextAvailableWeapon();
        UpdateWeaponSlotSprites();
        
        
    }

    public void UpdateAmountUnlocked()
    {
        if(playerManager.testingWeapons)
        {
            amountUnlocked = CountBool(unlockedWeapons, true);
        }
        else
        {
            amountUnlocked = CountBool(freeSlotArray, false);
        }
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
    public static int FindFirstFalseIndex(bool[] array)
    {
        int index = -1;
        for(int i = 0; i < array.Length; i++)
        {
            if(array[i] == false)
            {
                index = i;
                break;
            }
        }
        return index;
    }

    public static int FindSecondFalseIndex(bool[] array)
    {
        int index = -1;
        int iterations = 0;
        for(int i = 0; i < array.Length; i++)
        {
            if(array[i] == false)
            {
                index = i;
            }
            iterations++;
            if(iterations == 2)
            {
                break;
            }
        }
        return index;
    }
}
