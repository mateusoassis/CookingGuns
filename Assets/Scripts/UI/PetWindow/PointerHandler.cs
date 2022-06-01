using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PetWindowBrain petWindowBrain;
    [SerializeField] private int weaponType;    
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(weaponType == 0)
        {
            petWindowBrain.OpenPistol();
            Debug.Log("abre pistola");
        }
        else if(weaponType == 1)
        {
            petWindowBrain.OpenShotgun();
            Debug.Log("abre shotgun");
        }
        else if(weaponType == 2)
        {
            petWindowBrain.OpenMachineGun();
            Debug.Log("abre metralhadora");
        }
        else if(weaponType == 3)
        {
            petWindowBrain.OpenGrenadeLauncher();
            Debug.Log("abre grenade launcher");
        }
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if(weaponType == 0)
        {
            petWindowBrain.ClosePistol();
            Debug.Log("fecha pistola");
        }
        else if(weaponType == 1)
        {
            petWindowBrain.CloseShotgun();
            Debug.Log("fecha shotgun");
        }
        else if(weaponType == 2)
        {
            petWindowBrain.CloseMachineGun();
            Debug.Log("fecha metralhadora");
        }
        else if(weaponType == 3)
        {
            petWindowBrain.CloseGrenadeLauncher();
            Debug.Log("fecha grenade launcher");
        }
    }
}
