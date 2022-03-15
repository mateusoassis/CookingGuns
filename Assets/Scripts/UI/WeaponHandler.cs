using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponHandler : MonoBehaviour
{
    public GameObject ironBar;
    public Image ironBarImage;
    public GameObject axe;
    public Image axeImage;
    public GameObject pistol;
    public Image pistolImage;

    public bool axeUnlocked;
    public bool pistolUnlocked;
    public int powerUpCount;

    void Start()
    {
        axeImage.color = Color.red;
        pistolImage.color = Color.red;
        axeUnlocked = false;
        pistolUnlocked = false;
    }

    void Update()
    {
        if(axeUnlocked)
        {
            axeImage.color = Color.white;
        }
        if(pistolUnlocked)
        {
            pistolImage.color = Color.white;
        }
    }
}
