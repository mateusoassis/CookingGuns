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

    [Header("Variáveis de armas")]
    public int amountUnlocked;
    public int weaponTypeEquipped;
    private int slotEquipped;
    private int numberOfFalseIndexes;
    private int firstFalseIndex;
    private int secondFalseIndex;

    [Header("Objetos na cena")]
    public GameObject realGameObjects;

    [Header("Arrays")]
    public Sprite[] realWeaponIconsPool;
    public Image[] realWeaponIcons;
    //public Transform[] weaponIcons;
    //private Image[] weaponImages;
    public GameObject[] weaponObjects;
    public bool[] freeSlotArray;
    private int[] weaponTypeOnSlot;
    public _PlayerShooting[] playerShooting;

    [Header("Referências à scripts")]
    public _PlayerManager playerManager;
    private BreakWeapon breakWeaponScript;

    void Awake()
    {
        playerManager = GetComponent<_PlayerManager>();
        //testingGameObjects = GameObject.Find("TestingWeaponIcons");
        realGameObjects = GameObject.Find("RealWeaponIcons");

        //weaponImages = new Image[4];

        breakWeaponScript = GameObject.Find("WeaponCellsHolder").GetComponent<BreakWeapon>();

        //weaponImages[0] = GameObject.Find("PistolIcon").GetComponent<Image>();
        //weaponImages[1] = GameObject.Find("ShotgunIcon").GetComponent<Image>();
        //weaponImages[2] = GameObject.Find("MachineGunIcon").GetComponent<Image>();
        //weaponImages[3] = GameObject.Find("GranadeLauncherIcon").GetComponent<Image>();
        /*
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
            */
            //testingGameObjects.SetActive(false);
            //unlockedWeapons = new bool[4];
            for(int i = 0; i < realWeaponIcons.Length; i++)
            {
                realWeaponIcons[i] = GameObject.Find("Weapon" + (i+1) + "_Slot").GetComponent<Image>();
            }
            
            UpdateWeaponHandler();
        /*
        }
        */
        
        UpdateWeaponHandler();
        //UpdateWeaponSlotSprites();
        
    }

    void Start()
    {   
        WeaponManager(weaponTypeEquipped);
        UpdateWeaponSlotSprites();
        NextWeapon();
        PreviousWeapon();
    }

    void Update()
    {
        //UpdateWeaponSlotSprites();
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
        /*
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
        {*/
            playerManager.playerInfo.lastWeaponTypeEquipped = n;
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
        //}
    }

    public void NextWeapon()
    {   
        Image[] tempButtonImage = new Image[3];
        if(CountBool(freeSlotArray, true) < 2)
        {
            if(playerShooting[weaponTypeEquipped].reloading)
            {
                playerShooting[weaponTypeEquipped].ReloadInterrupted();
            }
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
                    playerManager.playerInfo.lastSlotEquipped = slotEquipped;
                }
            }
            
            if(!freeSlotArray[newSlot])
            {
                slotEquipped = newSlot;
                playerManager.playerInfo.lastSlotEquipped = newSlot;
            }
            
            if(slotEquipped > 2)
            {
                slotEquipped = 0;
                playerManager.playerInfo.lastSlotEquipped = slotEquipped;
            }

            if(!freeSlotArray[slotEquipped])
            {
                weaponTypeEquipped = weaponTypeOnSlot[slotEquipped];
                WeaponManager(weaponTypeEquipped);
            }

            //weaponObjects[weaponTypeEquipped].GetComponent<_PlayerShooting>().ReloadInterrupted();

            
            UpdateWeaponSlotSprites();
        }
    }

    public void SwitchToNextAvailableWeapon()
    {
        
        int newSlot = FindFirstFalseIndex(freeSlotArray);
        slotEquipped = newSlot;

        weaponTypeEquipped = weaponTypeOnSlot[slotEquipped];
        WeaponManager(weaponTypeEquipped);
    }

    public void UpdateWeaponSlotSprites()
    {
        firstFalseIndex = FindFirstFalseIndex(freeSlotArray);
        secondFalseIndex = FindSecondFalseIndex(freeSlotArray);
        numberOfFalseIndexes = CountBool(freeSlotArray, false);

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
                        realWeaponIcons[2].sprite = realWeaponIconsPool[weaponTypeOnSlot[1]];
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
        
    }

    public void PreviousWeapon()
    {
        if(CountBool(freeSlotArray, true) < 2)
        {
            if(playerShooting[weaponTypeEquipped].reloading)
            {
                playerShooting[weaponTypeEquipped].ReloadInterrupted();
            }

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
                    playerManager.playerInfo.lastSlotEquipped = slotEquipped;
                }
            }
            
            if(!freeSlotArray[newSlot])
            {
                slotEquipped = newSlot;
                playerManager.playerInfo.lastSlotEquipped = newSlot;
            }
            
            if(slotEquipped < 0)
            {
                slotEquipped = 2;
                playerManager.playerInfo.lastSlotEquipped = slotEquipped;
            }

            if(!freeSlotArray[slotEquipped])
            {
                weaponTypeEquipped = weaponTypeOnSlot[slotEquipped];
                WeaponManager(weaponTypeEquipped);
            }

            //weaponObjects[weaponTypeEquipped].GetComponent<_PlayerShooting>().ReloadInterrupted();

            
            UpdateWeaponSlotSprites();
        }
    }

    public void SwitchGuns()
    {
        /*
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
            */
            if(Input.GetKeyDown(KeyCode.Q) && !playerManager.isShooting)
            {
                PreviousWeapon();
            }
            else if(Input.GetKeyDown(KeyCode.E) && !playerManager.isShooting)
            {
                NextWeapon();
            }
        //}
    }

    public void UpdateWeaponHandler()
    {
        weaponTypeEquipped = playerManager.playerInfo.lastWeaponTypeEquipped;
        slotEquipped = playerManager.playerInfo.lastSlotEquipped;
        freeSlotArray = playerManager.playerInfo.freeSlotArraySaved;
        weaponTypeOnSlot = playerManager.playerInfo.weaponTypeOnSlotSaved;
        UpdateWeaponSlotSprites();
    }

    public void ActivateLastWeaponEquipped()
    {
        weaponTypeEquipped = playerManager.playerInfo.lastWeaponTypeEquipped;
    }

    public void ActivatePistol_()
    {
        weaponTypeEquipped = 0;
        playerManager.playerInfo.lastWeaponTypeEquipped = 0;
    }
    public void ActivateShotgun_()
    {
        weaponTypeEquipped = 1;
        playerManager.playerInfo.lastWeaponTypeEquipped = 1;
    }
    public void ActivateMachineGun_()
    {
        weaponTypeEquipped = 2;  
        playerManager.playerInfo.lastWeaponTypeEquipped = 2;
    }

    public void ActivateGranadeLauncher_()
    {
        weaponTypeEquipped = 3; 
        playerManager.playerInfo.lastWeaponTypeEquipped = 3; 
    }

    public void UnlockPistol()
    {
        /*
        if(playerManager.testingWeapons)
        {
            unlockedWeapons[0] = true;
        }
        else
        {
            */
            int slotFree = FindFirstTrueIndex(freeSlotArray);
            if(slotFree >= 0)
            {
                freeSlotArray[slotFree] = false;
                playerManager.playerInfo.freeSlotArraySaved[slotFree] = false;
                UpdateAmountUnlocked();
                weaponTypeOnSlot[slotFree] = 0;
                playerManager.playerInfo.weaponTypeOnSlotSaved[slotFree] = 0;
                realWeaponIcons[slotFree].sprite = realWeaponIconsPool[0];
            }
            UpdateWeaponSlotSprites();
            playerManager.playerInfo.totalWeaponsCrafted++;
        //}
        
        if(playerManager.tutorial)
        {
            //playerManager.tutorialWindowContainer.thirdPartKillTower.craftedAnyWeapon = true;
            playerManager.tutorialBrain.playerCraftedWeapon = true;
        }
    }
    public void DisablePistol()
    {
        /*
        if(playerManager.testingWeapons)
        {
            unlockedWeapons[0] = false;
        }
        else
        {
            */
            freeSlotArray[slotEquipped] = true;
            playerManager.playerInfo.freeSlotArraySaved[slotEquipped] = true;
            UpdateAmountUnlocked();
            weaponTypeOnSlot[slotEquipped] = 4;
            playerManager.playerInfo.weaponTypeOnSlotSaved[slotEquipped] = 4;
            //realWeaponIcons[slotEquipped].sprite = realWeaponIconsPool[4];
            UpdateWeaponSlotSprites();
        //}
    }

    public void UnlockShotgun()
    {
        /*
        if(playerManager.testingWeapons)
        {
            unlockedWeapons[1] = true;
        }
        else
        {
            */
            int slotFree = FindFirstTrueIndex(freeSlotArray);
            if(slotFree >= 0)
            {
                freeSlotArray[slotFree] = false;
                playerManager.playerInfo.freeSlotArraySaved[slotFree] = false;
                UpdateAmountUnlocked();
                weaponTypeOnSlot[slotFree] = 1;
                playerManager.playerInfo.weaponTypeOnSlotSaved[slotFree] = 1;
                //realWeaponIcons[slotFree].sprite = realWeaponIconsPool[1];
            }
            UpdateWeaponSlotSprites();
            playerManager.playerInfo.totalWeaponsCrafted++;
        //}
        
        if(playerManager.tutorial)
        {
            //playerManager.tutorialWindowContainer.thirdPartKillTower.craftedAnyWeapon = true;
            playerManager.tutorialBrain.playerCraftedWeapon = true;
        }
    }
    public void DisableShotgun()
    {
        /*
        if(playerManager.testingWeapons)
        {
            unlockedWeapons[1] = false;
        }
        else
        {
            */
            freeSlotArray[slotEquipped] = true;
            playerManager.playerInfo.freeSlotArraySaved[slotEquipped] = true;
            UpdateAmountUnlocked();
            weaponTypeOnSlot[slotEquipped] = 4;
            playerManager.playerInfo.weaponTypeOnSlotSaved[slotEquipped] = 4;
            //realWeaponIcons[slotEquipped].sprite = realWeaponIconsPool[4];
            UpdateWeaponSlotSprites();
        //}
    }

    public void UnlockMachineGun()
    {
        /*
        if(playerManager.testingWeapons)
        {
            unlockedWeapons[2] = true;
        }
        else
        {
            */
            int slotFree = FindFirstTrueIndex(freeSlotArray);
            if(slotFree >= 0)
            {
                freeSlotArray[slotFree] = false;
                playerManager.playerInfo.freeSlotArraySaved[slotFree] = false;
                UpdateAmountUnlocked();
                weaponTypeOnSlot[slotFree] = 2;
                playerManager.playerInfo.weaponTypeOnSlotSaved[slotFree] = 2;
                //realWeaponIcons[slotFree].sprite = realWeaponIconsPool[2];  
            }
            UpdateWeaponSlotSprites();
            playerManager.playerInfo.totalWeaponsCrafted++;
        //}
        
        if(playerManager.tutorial)
        {
            //playerManager.tutorialWindowContainer.thirdPartKillTower.craftedAnyWeapon = true;
            playerManager.tutorialBrain.playerCraftedWeapon = true;
        }
    }
    public void DisableMachineGun()
    {
        /*
        if(playerManager.testingWeapons)
        {
            unlockedWeapons[2] = false;
        }
        else
        {
            */
            freeSlotArray[slotEquipped] = true;
            playerManager.playerInfo.freeSlotArraySaved[slotEquipped] = true;
            UpdateAmountUnlocked();
            weaponTypeOnSlot[slotEquipped] = 4;
            playerManager.playerInfo.weaponTypeOnSlotSaved[slotEquipped] = 4;
            //realWeaponIcons[slotEquipped].sprite = realWeaponIconsPool[4];
            UpdateWeaponSlotSprites();
        //}
        
    } 

    public void UnlockGrenadeLauncher()
    {
        /*
        if(playerManager.testingWeapons)
        {
            unlockedWeapons[3] = true;
        }
        else
        {
            */
            int slotFree = FindFirstTrueIndex(freeSlotArray);
            if(slotFree >= 0)
            {
                freeSlotArray[slotFree] = false;
                playerManager.playerInfo.freeSlotArraySaved[slotFree] = false;
                UpdateAmountUnlocked();
                weaponTypeOnSlot[slotFree] = 3;
                playerManager.playerInfo.weaponTypeOnSlotSaved[slotFree] = 3;
                //realWeaponIcons[slotFree].sprite = realWeaponIconsPool[3];
            }
            UpdateWeaponSlotSprites();
            playerManager.playerInfo.totalWeaponsCrafted++;
        //}
        
        if(playerManager.tutorial)
        {
            //playerManager.tutorialWindowContainer.thirdPartKillTower.craftedAnyWeapon = true;
            playerManager.tutorialBrain.playerCraftedWeapon = true;
        }
    }
    public void DisableGrenadeLauncher()
    {
        /*
        if(playerManager.testingWeapons)
        {
            unlockedWeapons[3] = false;
        }
        else
        {
            */
            freeSlotArray[slotEquipped] = true;
            playerManager.playerInfo.freeSlotArraySaved[slotEquipped] = true;
            UpdateAmountUnlocked();
            weaponTypeOnSlot[slotEquipped] = 4;
            playerManager.playerInfo.weaponTypeOnSlotSaved[slotEquipped] = 4;
            //realWeaponIcons[slotEquipped].sprite = realWeaponIconsPool[4];
            UpdateWeaponSlotSprites();
        //}
    }
    
    /*
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
    */

    public void HealFromEatingWeapon()
    {
        /*
        if(playerManager.testingWeapons)
        {
            EatWeapon(weaponTypeEquipped);
        
        }
        else
        {
            */
            if(weaponTypeEquipped == 0)
            {
                DisablePistol();
                // joga a função de quebrar referente a pistol
            }
            else if(weaponTypeEquipped == 1)
            {
                DisableShotgun();
                // joga a função de quebrar referente a shotgun aqui
            }
            else if(weaponTypeEquipped == 2)
            {
                DisableMachineGun();
                // joga a função de quebrar referente a metralhadoraaqui
            }
            else if(weaponTypeEquipped == 3)
            {
                DisableGrenadeLauncher();
                // joga a função de quebrar referente ao grenade launcher aqui
            }
            breakWeaponScript.BreakTheWeapon(); // depois, tem que criar uma função separada que leva em consideração o weaponTypeEquipped pra destruir a certa
            SwitchToNextAvailableWeapon();
            UpdateWeaponSlotSprites();
        //}
        playerManager.playerStats.heartScript.FullHeal();
        playerManager.playerInfo.totalWeaponsEaten++;

        if(playerManager.tutorial)
        {
            //playerManager.tutorialWindowContainer.thirdPartKillTower.craftedAnyWeapon = true;
            playerManager.tutorialBrain.playerEatWeapon = true;
        }
    }

    public void UpdateAmountUnlocked()
    {
        /*
        if(playerManager.testingWeapons)
        {
            amountUnlocked = CountBool(unlockedWeapons, true);
        }
        else
        {
            */
            amountUnlocked = CountBool(freeSlotArray, false);
        //}
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

    public static int FindAny(int[] array, int flag)
    {
        int n = -1;
        for(int i = 0; i < array.Length; i++)
        {
            if(array[i] == flag)
            {
                n = i;
                break;
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
                iterations++;
            }
            
            if(iterations == 2)
            {
                break;
            }
        }
        return index;
    }
}
