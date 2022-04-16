using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehaviour : MonoBehaviour
{
    public BillboardCanvas canvas;
    public Image selfImage;
    public Color selfColor;

    public float timeToVanish;
    public float timerToVanish;
    
    public bool startCount;

    void Start()
    {
        selfImage.enabled = false;
        canvas = GetComponent<BillboardCanvas>();
        selfColor = selfImage.color;
    }

    void Update()
    {
        if(startCount)
        {
            timerToVanish -= Time.deltaTime;

            if(timerToVanish < 0)
            {
                //selfImage.enabled = false;
                StartCoroutine(HideHealthbar());
                startCount = false;
            }
        }
        
    }

    public void StartCount()
    {
        timerToVanish = timeToVanish;
        startCount = true;
        selfImage.enabled = true;
    }

    public void PermanentlyShowHP()
    {
        startCount = false;
        selfImage.enabled = true;
    }

    public IEnumerator HideHealthbar()
    {
        Debug.Log("começou coroutine");
        while(selfColor.a > 0)
        {
            Debug.Log(selfColor.a);
            float savedAlpha = selfColor.a;
            float alphaColor = Mathf.MoveTowards(savedAlpha, 0, Time.deltaTime);
            selfColor.a = savedAlpha;
        }
        Debug.Log("acabou while");
        yield break;
    }
}
