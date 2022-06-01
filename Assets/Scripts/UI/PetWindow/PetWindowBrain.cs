using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PetWindowBrain : MonoBehaviour
{
    [Header("Preencher")]
    [SerializeField] private float openSpeed;
    [SerializeField] private float XOffset;

    [Header("Analisar")]
    [SerializeField] private bool[] openArray;

    [Header("Ignorar")]
    [SerializeField] private RectTransform[] backImageArray;
    [SerializeField] private bool[] openToTheRight;

    [SerializeField] private Vector2[] initialPosArray;

    [SerializeField] private Vector2[] targetPosArray;

    [SerializeField] private TextMeshProUGUI[] petWindowTexts;
    [SerializeField] private String[] petWindowStrings;
    [SerializeField] private PlayerInfo playerInfo;

    void Awake()
    {
        SaveInitialPos();
    }
    void Start()
    {
        SaveOpenPos();
    }

    void Update()
    {
        HandleOpenAndClose();
    }

    

    private void HandleOpenAndClose()
    {
        for(int i = 0; i < backImageArray.Length; i++)
        {
            if(openArray[i])
            {
                backImageArray[i].anchoredPosition = Vector2.Lerp(backImageArray[i].anchoredPosition, targetPosArray[i], Time.deltaTime * openSpeed);
            }
            else
            {
                backImageArray[i].anchoredPosition = Vector2.Lerp(backImageArray[i].anchoredPosition, initialPosArray[i], Time.deltaTime * openSpeed);
            }
        }
    }

    private void SaveInitialPos()
    {
        for(int i = 0; i < backImageArray.Length; i++)
        {
            initialPosArray[i] = backImageArray[i].anchoredPosition;
        }
    }

    private void SaveOpenPos()
    {
        for(int i = 0; i < backImageArray.Length; i++)
        {
            if(openToTheRight[i])
            {
                targetPosArray[i] = initialPosArray[i] + new Vector2(XOffset, 0f);
            }
            else
            {
                targetPosArray[i] = initialPosArray[i] + new Vector2(-XOffset, 0f);
            }
        }
    }

    // abrir e fechar
    public void UpdateIngredientAmount()
    {
        for(int i = 0; i < petWindowStrings.Length; i++)
        {
            petWindowTexts[i].text = petWindowStrings.ToString();
        }
    }
    public void CloseAll()
    {
        for(int i = 0; i < openArray.Length; i++)
        {
            openArray[i] = false;
        }
    }

    // pointer enter e exit nos botões
    public void OpenPistol()
    {
        openArray[0] = true;
    }
    public void ClosePistol()
    {
        openArray[0] = false;
    }
    public void OpenShotgun()
    {
        openArray[1] = true;
    }
    public void CloseShotgun()
    {
        openArray[1] = false;
    }
    public void OpenMachineGun()
    {
        openArray[2] = true;
    }
    public void CloseMachineGun()
    {
        openArray[2] = false;
    }
    public void OpenGrenadeLauncher()
    {
        openArray[3] = true;
    }
    public void CloseGrenadeLauncher()
    {
        openArray[3] = false;
    }
}
