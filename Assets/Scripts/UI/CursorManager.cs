using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public Image crosshairImage;
    public Transform crosshair;
    public bool mouseVisible;
    private GameManager gameManager;

    private Color normalColor;
    private Color zeroAlphaColor;

    void Awake()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<Transform>();
        crosshairImage = crosshair.GetComponent<Image>();
        normalColor = crosshairImage.color;
        zeroAlphaColor = new Color(normalColor.r, normalColor.g, normalColor.b, 0f);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        Cursor.visible = mouseVisible;
        // UpdateCrosshair();  
        
        
    }

    void Update()
    {
        crosshair.position = Input.mousePosition;
        if(gameManager.pausedGame)
        {
            crosshairImage.color = zeroAlphaColor;
        }
        else
        {
            crosshairImage.color = normalColor;
        }
    }

    void FixedUpdate()
    {
        //crosshair.position = Input.mousePosition;
    }

    public void UpdateCrosshair()
    {
        Cursor.SetCursor(playerInfo.crosshairTextures2D[playerInfo.crosshairIndex], Vector2.zero, CursorMode.Auto);
    }
}
