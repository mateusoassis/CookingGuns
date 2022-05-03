using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShake : MonoBehaviour
{
    public CameraShake shake;

    void Awake()
    {
        shake = GameObject.Find("Shake").GetComponent<CameraShake>();
    }

    void Start()
    {
        shake.Invoke("Shockwave" , 0f);
    }
}
