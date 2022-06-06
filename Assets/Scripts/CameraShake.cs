using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraShake : MonoBehaviour
{
    [SerializeField]private UnityEvent shock;


    public void Shockwave()
    {
        if(GameObject.Find("GameManager").GetComponent<GameManager>().playerInfo.cameraShake)
        {
            shock.Invoke();
        }
        else
        {
            Debug.Log("Camera Shake desligado");
        }
    }
}
