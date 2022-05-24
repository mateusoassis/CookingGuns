using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSManager : MonoBehaviour
{
    public ArrowOptions fpsController;
    public int currentFPStarget;
    public int currentRefreshRate;
    public PlayerInfo playerInfo;
    public TextMeshProUGUI fpsText;

    void Awake()
    {
        UpdateText();
    }

    void Start()
    {
        ChangeTargetFPS();
        currentRefreshRate = Screen.currentResolution.refreshRate;
    }

    public void UpdateText()
    {
        fpsController.index = playerInfo.fpsIndex;
        fpsText.text = fpsController.optionsArray[fpsController.index];
    }

    public void ChangeTargetFPS()
    {
        if(fpsController.index == 0)
        {
            playerInfo.fpsIndex = 0;
            Application.targetFrameRate = Screen.currentResolution.refreshRate;
            currentFPStarget = Screen.currentResolution.refreshRate;
        }
        else if(fpsController.index == 1)
        {
            playerInfo.fpsIndex = 1;
            Application.targetFrameRate = 30;
            currentFPStarget = 30;
        }
        else if(fpsController.index == 2)
        {
            playerInfo.fpsIndex = 2;
            Application.targetFrameRate = 60;
            currentFPStarget = 60;
        }
        else
        {
            playerInfo.fpsIndex = 3;
            Application.targetFrameRate = 300;
            currentFPStarget = 300;
        }
    }
}
