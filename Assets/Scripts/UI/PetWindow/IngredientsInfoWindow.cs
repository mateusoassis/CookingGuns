using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientsInfoWindow : MonoBehaviour
{
    [Header("Preencher")]
    public float speed;
    public float vanishTimeAfterLoot;
    private float vanishTimer;

    [Header("De olho")]
    [SerializeField] private bool ingredientWindowOpen;

    [Header("Ignora")]
    public RectTransform thisRect;
    public PlayerInfo playerInfo;
    public Transform[] slots;
    public Vector2 initialPos;
    public float YOffset;
    public Vector2 targetPos;
    public GameManager gameManager;
    
    

    void Awake()
    {
        thisRect = GetComponent<RectTransform>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        vanishTimer = vanishTimeAfterLoot;
    }
    void Start()
    {
        initialPos = thisRect.anchoredPosition;
        targetPos = initialPos + new Vector2(0f, YOffset);

        UpdateAllSlots();
    }

    public void UpdateAllSlots()
    {
        UpdateSprites();
        UpdateTexts();
    }

    public void UpdateSprites()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponentInChildren<Image>().sprite = playerInfo.ingredientesIcons[i];
        }
    }
    public void UpdateTexts()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponentInChildren<TextMeshProUGUI>().text = "x" + playerInfo.ingredientes[i].ToString("D2");
        }
    }

    void Update()
    {
        HandleWindowOpenAndClose();

        if(gameManager.playerManager.isRecentlyLoot)
        {
            ingredientWindowOpen = true;
            UpdateTexts();
            vanishTimer -= Time.deltaTime;
            if(vanishTimer <= 0)
            {
                ResetTimer();
            }
        }
        else
        {
            if((gameManager.pausedGame || gameManager.playerManager.petHandler.craftingWindowOpen) && !gameManager.pauseBlock)
            {
                ingredientWindowOpen = true;
                UpdateTexts();
            }
            else
            {
                ingredientWindowOpen = false;
            }
        }
    }
    private void ResetTimer()
    {
        ingredientWindowOpen = false;
        vanishTimer = vanishTimeAfterLoot;
        gameManager.playerManager.isRecentlyLoot = false;
    }

    private void HandleWindowOpenAndClose()
    {
        if(ingredientWindowOpen)
        {
            thisRect.anchoredPosition = Vector2.Lerp(thisRect.anchoredPosition, targetPos, Time.unscaledDeltaTime * speed);
        }
        else
        {
            thisRect.anchoredPosition = Vector2.Lerp(thisRect.anchoredPosition, initialPos, Time.unscaledDeltaTime * speed);
        }
    }
}
