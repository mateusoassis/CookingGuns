using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FullScreen : MonoBehaviour
{
    public ArrowOptions fullScreenController;
    public bool fullScreen;
    public PlayerInfo playerInfo;
    public TextMeshProUGUI fullscreenText;

    void Awake()
    {
        UpdateText();
    }

    void Start()
    {
        fullScreen = true;
        Screen.fullScreen = fullScreen;
    }

    public void UpdateText()
    {
        fullScreenController.index = playerInfo.fullscreenIndex;
        fullscreenText.text = fullScreenController.optionsArray[fullScreenController.index];
    }

    public void ChangeFullscreen()
    {
        if(fullScreenController.index == 0)
        {
            playerInfo.fullscreenIndex = 0;
            fullScreen = true;
            Screen.fullScreen = fullScreen;
        }
        else
        {
            playerInfo.fullscreenIndex = 1;
            fullScreen = false;
            Screen.fullScreen = fullScreen;
        }
    }
}
