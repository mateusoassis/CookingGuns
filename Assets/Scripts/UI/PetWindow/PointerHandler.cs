using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private PetWindowBrain petWindowBrain;
    [SerializeField] private int weaponType;  
    [SerializeField] private Sprite regularImage;
    [SerializeField] private Sprite mouseOverSprite;
    [SerializeField] private Sprite clickSprite;
    [SerializeField] private PetLookAt petLookAt;

    void Start()
    {
        regularImage = GetComponent<Image>().sprite;
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(weaponType == 0)
        {
            petWindowBrain.OpenPistol();
            petLookAt.LookAtPistol();
            Debug.Log("abre pistola");
        }
        else if(weaponType == 1)
        {
            petWindowBrain.OpenShotgun();
            petLookAt.LookAtShotgun();
            Debug.Log("abre shotgun");
        }
        else if(weaponType == 2)
        {
            petWindowBrain.OpenMachineGun();
            petLookAt.LookAtMachineGun();
            Debug.Log("abre metralhadora");
        }
        else if(weaponType == 3)
        {
            petWindowBrain.OpenGrenadeLauncher();
            petLookAt.LookAtGrenadeLauncher();
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
        petLookAt.lookAtPosition = petLookAt.playerPos.position;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        
    }
}
