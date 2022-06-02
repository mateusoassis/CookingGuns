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
    public int slotEquipped;
    private int numberOfFalseIndexes;
    private int firstFalseIndex;
    private int secondFalseIndex;

    [Header("Objetos na cena")]
    public GameObject realGameObjects;
    public ParticleSystem healingParticle;

    [Header("Arrays")]
    public Sprite[] realWeaponIconsPool;
    public Image[] realWeaponIcons;
    public GameObject[] weaponObjects;
    public bool[] freeSlotArray;
    public int[] weaponTypeOnSlot;
    public _PlayerShooting[] playerShooting;

    [Header("Referências à scripts")]
    public _PlayerManager playerManager;
    public BreakWeapon breakWeaponScript;
    [SerializeField] private IngredientUpdater[] ingredientUpdater;

    [Header("Gato Comemorando")]
    public GameObject catEndRoom;

    void Awake()
    {
        realGameObjects = GameObject.Find("RealWeaponIcons");
        ingredientUpdater = new IngredientUpdater[4];
        ingredientUpdater[0] = GameObject.Find("PistolBackImage").GetComponent<IngredientUpdater>();
        ingredientUpdater[1] = GameObject.Find("ShotgunBackImage").GetComponent<IngredientUpdater>();
        ingredientUpdater[2] = GameObject.Find("MachineGunBackImage").GetComponent<IngredientUpdater>();
        ingredientUpdater[3] = GameObject.Find("GrenadeLauncherBackImage").GetComponent<IngredientUpdater>();

        breakWeaponScript = GameObject.Find("WeaponCellsHolder").GetComponent<BreakWeapon>();

        for(int i = 0; i < realWeaponIcons.Length; i++)
        {
            realWeaponIcons[i] = GameObject.Find("Weapon" + (i+1) + "_Slot").GetComponent<Image>();
        }
            
        UpdateWeaponHandler();
        UpdateWeaponHandler();
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
        if(playerManager.gameManager.roomCleared && !playerManager.calledEndRoomAnimation && !playerManager.isEndRoomAnimation)
        {
            StartCoroutine(FinishRoomAnimation());
        }
    }

    public IEnumerator FinishRoomAnimation()
    {
        playerShooting[weaponTypeEquipped].ReloadInterrupted();
        DeactivateAll();
        playerManager.calledEndRoomAnimation = true;
        playerManager.isEndRoomAnimation = true;
        catEndRoom.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        playerManager.isEndRoomAnimation = false;
        catEndRoom.SetActive(false);
        WeaponManager(weaponTypeEquipped);
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
        playerManager.playerInfo.lastWeaponTypeEquipped = n;
        playerManager.playerInfo.lastSlotEquipped = slotEquipped;
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

    public void DeactivateAll()
    {
        for(int i = 0; i < weaponObjects.Length; i++)
        {
            weaponObjects[i].SetActive(false);
        }
    }

    public void NextWeapon()
    {   
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
            UpdateWeaponSlotSprites();
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

    public void SwitchGuns()
    {
        if(Input.GetKeyDown(KeyCode.Q) && !playerManager.isShooting && !playerManager.gameManager.pausedGame)
        {
            PreviousWeapon();
        }
        else if(Input.GetKeyDown(KeyCode.E) && !playerManager.isShooting && !playerManager.gameManager.pausedGame)
        {
            NextWeapon();
        }
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
        Debug.Log("você tem " + amountUnlocked + " armas");
        if(amountUnlocked < 3)
        {
            bool canCraft = false;
        
            for(int j = 0; j < ingredientUpdater[0].typeOfIngredients.Length; j++)
            {
                canCraft = playerManager.playerInfo.ingredientes[ingredientUpdater[0].typeOfIngredients[j]] 
                            >= ingredientUpdater[0].amountOfIngredients[j];
            }

            Debug.Log(canCraft);

            if(canCraft)
            {
                int slotFree = FindFirstTrueIndex(freeSlotArray);
                if(slotFree >= 0)
                {
                    freeSlotArray[slotFree] = false;
                    playerManager.playerInfo.freeSlotArraySaved[slotFree] = false;
                    weaponTypeOnSlot[slotFree] = 0;
                    playerManager.playerInfo.weaponTypeOnSlotSaved[slotFree] = 0;
                    realWeaponIcons[slotFree].sprite = realWeaponIconsPool[0];
                }
                UpdateWeaponSlotSprites();
                playerManager.playerInfo.totalWeaponsCrafted++;

                UpdateAllIngredientAmount();
            }
            else
            {
                // algo acontece quando não pode craftar
                Debug.Log("falta material");
            }
            
            if(playerManager.tutorial)
            {
                playerManager.tutorialBrain.playerCraftedWeapon = true;
            }
        }
        else
        {
            // cheio de armas
            Debug.Log("inventário cheio de armas");
        }
        
    }
    public void DisablePistol()
    {
        freeSlotArray[slotEquipped] = true;
        playerManager.playerInfo.freeSlotArraySaved[slotEquipped] = true;
        weaponTypeOnSlot[slotEquipped] = 4;
        playerManager.playerInfo.weaponTypeOnSlotSaved[slotEquipped] = 4;
        playerManager.playerShootingPistol.bulletsLeft = playerManager.playerShootingPistol.magazineSize;
        UpdateAmountUnlocked();
        UpdateWeaponSlotSprites();
    }

    public void UnlockShotgun()
    {
        Debug.Log("você tem " + amountUnlocked + " armas");
        if(amountUnlocked < 3)
        {
            bool canCraft = false;
        
            for(int j = 0; j < ingredientUpdater[1].typeOfIngredients.Length; j++)
            {
                canCraft = playerManager.playerInfo.ingredientes[ingredientUpdater[1].typeOfIngredients[j]] 
                            >= ingredientUpdater[1].amountOfIngredients[j];
            }

            Debug.Log(canCraft);

            if(canCraft)
            {
                int slotFree = FindFirstTrueIndex(freeSlotArray);
                if(slotFree >= 0)
                {
                    freeSlotArray[slotFree] = false;
                    playerManager.playerInfo.freeSlotArraySaved[slotFree] = false;
                    weaponTypeOnSlot[slotFree] = 1;
                    playerManager.playerInfo.weaponTypeOnSlotSaved[slotFree] = 1;
                    realWeaponIcons[slotFree].sprite = realWeaponIconsPool[1];
                }
                UpdateWeaponSlotSprites();
                playerManager.playerInfo.totalWeaponsCrafted++;
                
                UpdateAllIngredientAmount();
            }
            else
            {
                // algo acontece quando não pode craftar
                Debug.Log("falta material");
            }
            
            if(playerManager.tutorial)
            {
                playerManager.tutorialBrain.playerCraftedWeapon = true;
            }
        }
        else
        {
            // cheio de armas
            Debug.Log("inventário cheio de armas");
        }
    }
    public void DisableShotgun()
    {
        
        freeSlotArray[slotEquipped] = true;
        playerManager.playerInfo.freeSlotArraySaved[slotEquipped] = true;           
        weaponTypeOnSlot[slotEquipped] = 4;
        playerManager.playerInfo.weaponTypeOnSlotSaved[slotEquipped] = 4;
        playerManager.playerShootingShotgun.bulletsLeft = playerManager.playerShootingShotgun.magazineSize;
        UpdateAmountUnlocked();
        UpdateWeaponSlotSprites();
    }

    public void UnlockMachineGun()
    {
        Debug.Log("você tem " + amountUnlocked + " armas");
        if(amountUnlocked < 3)
        {
            bool canCraft = false;
        
            for(int j = 0; j < ingredientUpdater[2].typeOfIngredients.Length; j++)
            {
                canCraft = playerManager.playerInfo.ingredientes[ingredientUpdater[2].typeOfIngredients[j]] 
                            >= ingredientUpdater[2].amountOfIngredients[j];
            }

            Debug.Log(canCraft);

            if(canCraft)
            {
        
                int slotFree = FindFirstTrueIndex(freeSlotArray);
                if(slotFree >= 0)
                {
                    freeSlotArray[slotFree] = false;
                    playerManager.playerInfo.freeSlotArraySaved[slotFree] = false;
                    weaponTypeOnSlot[slotFree] = 2;
                    playerManager.playerInfo.weaponTypeOnSlotSaved[slotFree] = 2;
                    realWeaponIcons[slotFree].sprite = realWeaponIconsPool[2];
                }
                UpdateWeaponSlotSprites();
                playerManager.playerInfo.totalWeaponsCrafted++;

                UpdateAllIngredientAmount();
            }
            else
            {
                // algo acontece quando não pode craftar
                Debug.Log("falta material");
            }
            
            if(playerManager.tutorial)
            {
                playerManager.tutorialBrain.playerCraftedWeapon = true;
            }
        }
        else
        {
            // cheio de armas
            Debug.Log("inventário cheio de armas");
        }
    }
    
    public void DisableMachineGun()
    {
        freeSlotArray[slotEquipped] = true;
        playerManager.playerInfo.freeSlotArraySaved[slotEquipped] = true;
        weaponTypeOnSlot[slotEquipped] = 4;
        playerManager.playerInfo.weaponTypeOnSlotSaved[slotEquipped] = 4;
        playerManager.playerShootingMachineGun.bulletsLeft = playerManager.playerShootingMachineGun.magazineSize;
        UpdateAmountUnlocked();
        UpdateWeaponSlotSprites();       
    } 

    public void UnlockGrenadeLauncher()
    {
        Debug.Log("você tem " + amountUnlocked + " armas");
        if(amountUnlocked < 3)
        {
            bool canCraft = false;
        
            for(int j = 0; j < ingredientUpdater[3].typeOfIngredients.Length; j++)
            {
                canCraft = playerManager.playerInfo.ingredientes[ingredientUpdater[3].typeOfIngredients[j]] 
                            >= ingredientUpdater[3].amountOfIngredients[j];
            }

            Debug.Log(canCraft);

            if(canCraft)
            {
                int slotFree = FindFirstTrueIndex(freeSlotArray);
                if(slotFree >= 0)
                {
                    freeSlotArray[slotFree] = false;
                    playerManager.playerInfo.freeSlotArraySaved[slotFree] = false;
                    weaponTypeOnSlot[slotFree] = 3;
                    playerManager.playerInfo.weaponTypeOnSlotSaved[slotFree] = 3;
                    realWeaponIcons[slotFree].sprite = realWeaponIconsPool[3];
                }
                UpdateWeaponSlotSprites();
                playerManager.playerInfo.totalWeaponsCrafted++;

                UpdateAllIngredientAmount(); 
            }
            else
            {
                // algo acontece quando não pode craftar
                Debug.Log("falta material");
            }
            
            if(playerManager.tutorial)
            {
                playerManager.tutorialBrain.playerCraftedWeapon = true;
            }
        }
        else
        {
            // cheio de armas
            Debug.Log("inventário cheio de armas");
        }
    }

    public void DisableGrenadeLauncher()
    {
        freeSlotArray[slotEquipped] = true;
        playerManager.playerInfo.freeSlotArraySaved[slotEquipped] = true;
        weaponTypeOnSlot[slotEquipped] = 4;
        playerManager.playerInfo.weaponTypeOnSlotSaved[slotEquipped] = 4;
        playerManager.playerShootingGranadeLauncher.bulletsLeft = playerManager.playerShootingGranadeLauncher.magazineSize;
        UpdateAmountUnlocked();
        UpdateWeaponSlotSprites();
    }

    private void UpdateAllIngredientAmount()
    {
        
        for(int j = 0; j < ingredientUpdater[3].typeOfIngredients.Length; j++)
        {
            playerManager.playerInfo.ingredientes[ingredientUpdater[3].typeOfIngredients[j]] -= ingredientUpdater[3].amountOfIngredients[j];
            
        }
        for(int i = 0; i < ingredientUpdater.Length; i++)
        {
            ingredientUpdater[i].UpdateIngredientAmount();
        }
        UpdateAmountUnlocked();
    }

    public void HealFromEatingWeapon()
    {
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
            // joga a função de quebrar referente a metralhadora aqui
        }
        else if(weaponTypeEquipped == 3)
        {
            DisableGrenadeLauncher();
            // joga a função de quebrar referente ao grenade launcher aqui
        }
        breakWeaponScript.BreakTheWeapon();
        SwitchToNextAvailableWeapon();
        UpdateWeaponSlotSprites();
    
        playerManager.playerStats.heartScript.FullHeal();
        healingParticle.Play();
        playerManager.playerInfo.totalWeaponsEaten++;

        if(playerManager.tutorial)
        {
            playerManager.tutorialBrain.playerEatWeapon = true;
        }
    }

    public void UpdateAmountUnlocked()
    {
        amountUnlocked = CountBool(freeSlotArray, false);
    }

    public void PlayerIsDead()
    {
        for(int i = 0; i < weaponObjects.Length; i++)
        {
            {
                weaponObjects[i].SetActive(false);
            }
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
