using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeListener : MonoBehaviour
{
    private CinemachineImpulseSource source;

    private void Awake()
    {
        source = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake()
    {
        if(GameObject.Find("GameManager").GetComponent<GameManager>().playerInfo.cameraShake)
        {
            source.GenerateImpulse(10f);
        }
    }
}
