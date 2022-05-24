using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSManager : MonoBehaviour
{
    public ArrowOptions fullScreenController;
    public bool fullScreen;

    void Start()
    {
        fullScreen = true;
    }

    public void ChangeFullscreen()
    {
        if(fullScreenController.index == 0)
        {
            Screen.fullScreen = true;
            fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
            fullScreen = false;
        }
    }
}
