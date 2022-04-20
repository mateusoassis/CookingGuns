using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouLose : MonoBehaviour
{
    public GameObject youLoseWindow;

    public void PlayerLost()
    {
        if(youLoseWindow != null)
        {
            youLoseWindow.SetActive(true);
        }  
    }
}
