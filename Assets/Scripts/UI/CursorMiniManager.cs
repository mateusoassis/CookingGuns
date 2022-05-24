using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorMiniManager : MonoBehaviour
{
    public Transform targetGroup;
    public Transform miniCrosshair;
    private Vector3 pos;
    private Vector3 realPos;
    public bool miniAim;

    public Animator thisAnimator;

    void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex > 2)
        {
            targetGroup = GameObject.Find("MiniCrosshairTargetGroup").GetComponent<Transform>();
            miniCrosshair = GameObject.Find("MiniCrosshair").GetComponent<Transform>();
            thisAnimator = miniCrosshair.GetComponent<Animator>();
        }
    }

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex > 2)
        {
            miniAim = true;
        }
        else
        {
            miniAim = false;
        }
    }

    void LateUpdate()
    {
        if(miniAim && SceneManager.GetActiveScene().buildIndex > 2)
        {   
            miniCrosshair.position = Camera.main.WorldToScreenPoint(targetGroup.position);
        }
    }

    public void DisableMiniCursor()
    {
        miniAim = false;
        miniCrosshair.gameObject.SetActive(false);
    }
    public void EnableMiniCursor()
    {
        miniAim = true;
        miniCrosshair.gameObject.SetActive(true);
    }
}
