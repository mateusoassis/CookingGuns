//using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSwitchBlend : MonoBehaviour
{
    public bool conceptualCameraBool;
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private CinemachineVirtualCamera petCamera;
    [SerializeField] private CinemachineVirtualCamera conceptualCamera;
    private bool mainCameraMode;
    private bool conceptualMode;

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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J) && conceptualCameraBool)
        {
            SwitchToConceptual();
        }
    }

    public void SwitchPriority()
    {
        if(mainCameraMode)
        {
            mainCamera.Priority = 0;
            petCamera.Priority = 10;
            conceptualCamera.Priority = 0;
        }
        else
        {
            mainCamera.Priority = 10;
            petCamera.Priority = 0;
            conceptualCamera.Priority = 0;
        }
        mainCameraMode = !mainCameraMode;
    }

    public void SwitchToConceptual()
    {
        if(conceptualMode)
        {
            mainCamera.Priority = 0;
            petCamera.Priority = 0;
            conceptualCamera.Priority = 10;
        }
        else
        {
            mainCamera.Priority = 10;
            petCamera.Priority = 0;
            conceptualCamera.Priority = 0;
        }
        conceptualMode = !conceptualMode;
    }
}
