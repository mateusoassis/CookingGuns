using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_WeaponsPickUp : MonoBehaviour
{
    public Vector3 rotateVector;
    public float rotationMultiplier;

    public GameObject leftMouseClickButton;
    public TutorialFadeOut tutorialFadeout;
    
    void Start()
    {
        tutorialFadeout = GameObject.Find("FadeInFadeOut").GetComponent<TutorialFadeOut>();
    }

    void Update()
    {
        if(tutorialFadeout.sceneIndex == 1)
        {
            transform.Rotate(rotateVector * Time.deltaTime * rotationMultiplier);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(tutorialFadeout.sceneIndex == 1)
            {
                leftMouseClickButton.SetActive(true);
            }            
            other.gameObject.GetComponent<T_WeaponHandler>().UnlockPistol();
            other.gameObject.GetComponent<T_PlayerManager>().tutorialPlayerWeaponHandler.ActivatePistol_();
            other.gameObject.GetComponent<T_PlayerManager>().tutorialPlayerWeaponHandler.WeaponManager(other.gameObject.GetComponent<T_PlayerManager>().tutorialPlayerWeaponHandler.weaponEquipped);
            Destroy(this.gameObject);
        }
    }
}
