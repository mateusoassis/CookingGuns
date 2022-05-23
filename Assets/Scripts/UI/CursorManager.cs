using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public PlayerInfo playerInfo;
    public Image crosshairImage;
    public Transform crosshair;
    public bool mouseVisible;
    private GameManager gameManager;

    private Color normalColor;
    private Color zeroAlphaColor;

    private Animator crosshairAnim;

    [Header("Variáveis de animação")]
    public float newAnimationSpeed = 1f;
    //public float currentReloadDuration;
    public float currentAnimationSpeed;
    public float baseReloadDuration = 1f;
    public float baseAnimationSpeed = 1f;

    void Awake()
    {
        crosshair = GameObject.Find("Crosshair").GetComponent<Transform>();
        crosshairImage = crosshair.GetComponent<Image>();
        if(SceneManager.GetActiveScene().buildIndex > 2)
        {
            crosshairAnim = crosshair.GetComponent<Animator>();
        }
        normalColor = crosshairImage.color;
        zeroAlphaColor = new Color(normalColor.r, normalColor.g, normalColor.b, 0f);
        if(SceneManager.GetActiveScene().buildIndex > 2)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }

    void Start()
    {
        Cursor.visible = mouseVisible;
        if(SceneManager.GetActiveScene().buildIndex > 2)
        {
            currentAnimationSpeed = crosshairAnim.GetFloat("speed");
        }
        // UpdateCrosshair(); 
    }

    void Update()
    {
        crosshair.position = Input.mousePosition;
        if(SceneManager.GetActiveScene().buildIndex > 2)
        {
            if(gameManager.pausedGame || gameManager.playerManager.petHandler.craftingWindowOpen)
            {
                crosshairImage.color = zeroAlphaColor;
                Cursor.visible = true;
            }
            else
            {
                crosshairImage.color = normalColor;
                Cursor.visible = mouseVisible;
            }
        }

        /*
        if(currentReloadDuration != baseReloadDuration || currentAnimationSpeed != baseAnimationSpeed)
        {
            newAnimationSpeed = baseAnimationSpeed / (currentReloadDuration/baseReloadDuration);
            crosshairAnim.SetFloat("speed", newAnimationSpeed);
        }
        */
    }

    void FixedUpdate()
    {
        //crosshair.position = Input.mousePosition;
    }

    public void UpdateCrosshair()
    {
        Cursor.SetCursor(playerInfo.crosshairTextures2D[playerInfo.crosshairIndex], Vector2.zero, CursorMode.Auto);
    }

    public void ReloadCrosshairAnimation(float reloadDuration)
    {
        if(reloadDuration != baseReloadDuration || currentAnimationSpeed != baseAnimationSpeed)
        {
            newAnimationSpeed = baseAnimationSpeed / (reloadDuration/baseReloadDuration);
        }
        crosshairAnim.SetFloat("speed", newAnimationSpeed);
        crosshairAnim.ResetTrigger("CancelReloadAnim");
        crosshairAnim.SetTrigger("Reload");
    }

    public void InterruptReloadAnim()
    {
        crosshairAnim.ResetTrigger("Reload");
        crosshairAnim.SetTrigger("CancelReloadAnim");
    }
}