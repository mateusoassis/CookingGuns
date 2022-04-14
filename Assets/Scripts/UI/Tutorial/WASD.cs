using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WASD : MonoBehaviour
{
    public Button[] childButtons;
    public Transform disableTransform;

    void Start()
    {
        
    }

    public void DisableButton()
    {
        disableTransform.gameObject.SetActive(false);
    }
}
