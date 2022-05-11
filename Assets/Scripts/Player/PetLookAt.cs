using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetLookAt : MonoBehaviour
{
    [SerializeField] private Transform pistolButton;
    [SerializeField] private Transform shotgunButton;
    [SerializeField] private Transform machineGunButton;
    [SerializeField] private Transform grenadeLauncherButton;
    public Transform playerPos;

    public Vector3 lookAtPosition;
    public bool lookAtButton;

    public CanvasGroup canvasGroup;

    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void LookAtPistol()
    {
        lookAtPosition = pistolButton.position;
        lookAtButton = false;
    }
    public void LookAtShotgun()
    {
        lookAtPosition = shotgunButton.position;
        lookAtButton = false;
    }
    public void LookAtMachineGun()
    {
        lookAtPosition = machineGunButton.position;
        lookAtButton = false;
    }
    public void LookAtGrenadeLauncher()
    {
        lookAtPosition = grenadeLauncherButton.position;
        lookAtButton = false;
    }

    public void DisableButtonsCanvas()
    {
        gameObject.SetActive(false);
    }
}
