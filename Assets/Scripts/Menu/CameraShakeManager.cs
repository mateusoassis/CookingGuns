using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    public ArrowOptions cameraShakeManager;
    public PlayerInfo playerInfo;

    public void UpdateCameraShakeOnPlayerInfo()
    {
        if(cameraShakeManager.index == 0)
        {
            playerInfo.cameraShake = true;
        }
        else
        {
            playerInfo.cameraShake = false;
        }
    }
}
