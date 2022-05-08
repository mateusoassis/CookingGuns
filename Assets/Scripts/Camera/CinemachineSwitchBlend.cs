using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitchBlend : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private CinemachineVirtualCamera petCamera;
    private bool mainCameraMode;

    [Header("Inserir mesma duração das trocas")]
    public float mainToPetDuration;
    public float petToMainDuration;

    void Awake()
    {

    }

    void Start()
    {
        SwitchPriority();
    }

    public void SwitchPriority()
    {
        if(mainCameraMode)
        {
            mainCamera.Priority = 0;
            petCamera.Priority = 10;
        }
        else
        {
            mainCamera.Priority = 10;
            petCamera.Priority = 0;
        }
        mainCameraMode = !mainCameraMode;
    }
}
