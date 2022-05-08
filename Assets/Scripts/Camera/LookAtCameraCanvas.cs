using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LookAtCameraCanvas : MonoBehaviour
{
    private Transform petCamera;

    void Awake()
    {
        petCamera = GameObject.Find("Pet VCAM").GetComponent<Transform>();
    }

    void Start()
    {
        //gameObject.SetActive(false);
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + petCamera.forward);
    }
}
