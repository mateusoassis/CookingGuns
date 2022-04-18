using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_WeaponsPickUp : MonoBehaviour
{
    public GameObject leftMouseClickButton;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            leftMouseClickButton.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
