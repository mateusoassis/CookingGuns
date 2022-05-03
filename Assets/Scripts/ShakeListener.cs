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
    
    void Start()
    {
        //Invoke("Shake", 0f);
    }

    public void Shake()
    {
        source.GenerateImpulse(10f);
    }
}
