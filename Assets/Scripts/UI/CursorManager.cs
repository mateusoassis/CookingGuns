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

    void Awake()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<Transform>();
        crosshairImage = crosshair.GetComponent<Image>();
    }

    void Start()
    {
        Cursor.visible = mouseVisible;
        // UpdateCrosshair();  
        crosshairImage.sprite = playerInfo.crosshairImages[playerInfo.crosshairIndex];
        
    }

    void Update()
    {
        crosshair.position = Input.mousePosition;
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
