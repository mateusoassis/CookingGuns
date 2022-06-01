using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public ArrowOptions resolutionController;
    public FullScreen fullScreen;
    public FPSManager fpsManager;

    public Resolution[] resolutions;

    void Awake()
    {
        //print(Screen.currentResolution);
        //SearchCurrentResolutionIndex();
        resolutions = Screen.resolutions;
        resolutionController.optionsArray = new string[resolutions.Length];
    }

    void Start()
    {
        for(int i = 0; i < resolutions.Length; i++)
        {
            resolutionController.optionsArray[i] = resolutions[i].width.ToString() + " x " + resolutions[i].height.ToString();
        }
        //resolutionController.index = resolutions.Length - 1;
        
        SearchCurrentResolutionIndex();

        UpdateText();

        SendIndexToApplyResolution();
    }

    public void UpdateText()
    {
        resolutionController.textComponent.text = resolutionController.optionsArray[resolutionController.index];
    }

    public void SetResolutionOnApp(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, fullScreen.fullScreen, fpsManager.currentFPStarget);
    }
    public void SendIndexToApplyResolution()
    {
        SetResolutionOnApp(resolutionController.index);
    }

    public void SearchCurrentResolutionIndex()
    {
        for(int i = 0; i < resolutions.Length; i++)
        {
            if(Screen.currentResolution.width == resolutions[i].width && Screen.currentResolution.height == resolutions[i].height)
            {
                resolutionController.index = i;
            }
        }
    }
}
