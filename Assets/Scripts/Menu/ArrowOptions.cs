using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrowOptions : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public int index;
    public string[] optionsArray;
    
    void Start()
    {
        textComponent.text = optionsArray[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextOption()
    {
        if(index == optionsArray.Length - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        textComponent.text = optionsArray[index];
    }

    public void PreviousOption()
    {
        if(index == 0)
        {
            index = optionsArray.Length - 1;
        }
        else
        {
            index--;
        }
        textComponent.text = optionsArray[index];
    }
}
