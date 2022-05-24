using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSManager : MonoBehaviour
{
    public ArrowOptions fpsController;
    public int currentFPStarget;
    public int currentRefreshRate;

    void Start()
    {
        ChangeTargetFPS();
        currentRefreshRate = Screen.currentResolution.refreshRate;
    }

    public void ChangeTargetFPS()
    {
        if(fpsController.index == 0)
        {
            Application.targetFrameRate = Screen.currentResolution.refreshRate;
            currentFPStarget = Screen.currentResolution.refreshRate;
        }
        else if(fpsController.index == 1)
        {
            Application.targetFrameRate = 30;
            currentFPStarget = 30;
        }
        else if(fpsController.index == 2)
        {
            Application.targetFrameRate = 60;
            currentFPStarget = 60;
        }
        else
        {
            Application.targetFrameRate = 300;
            currentFPStarget = 300;
        }
    }
}
