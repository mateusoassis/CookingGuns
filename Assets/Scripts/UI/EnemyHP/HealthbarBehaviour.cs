using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehaviour : MonoBehaviour
{
    public bool isCanvasGroup;
    public BillboardCanvas canvas;
    public Image selfImage; // imagem da barra de HP
    public Image secondImage; // imagem da barra atrás da de HP
    public CanvasGroup canvasGroup;
    public Color selfColor; // cor da barra de HP
    public float r1, g1, b1;

    public Color secondColor; // cor da barra atrás da de HP
    public float r2, g2, b2;

    public float timeToVanish;
    public float timerToVanish;
    
    public bool startCount;

    void Start()
    {
        if(!isCanvasGroup)
        {
            selfImage.enabled = false;
        }
        else
        {
            canvasGroup.alpha = 0f;
        }
        canvas = GetComponent<BillboardCanvas>();
        selfColor = selfImage.color;
        r1 = selfImage.color.r;
        g1 = selfImage.color.g;
        b1 = selfImage.color.b;

        secondColor = secondImage.color;
        r2 = secondImage.color.r;
        g2 = secondImage.color.g;
        b2 = secondImage.color.b;
    }

    void Update()
    {
        if(startCount)
        {
            timerToVanish -= Time.deltaTime;

            if(timerToVanish < 0)
            {
                if(!isCanvasGroup)
                {
                    StartCoroutine(ChangeImageAlpha(1f, 0f, 1f));
                }
                else
                {
                    StartCoroutine(ChangeCanvasGroupAlpha(1f, 0f, 1f));
                }
                startCount = false;
            }
        }
        
    }

    public void StartCount()
    {
        if(!isCanvasGroup)
        {
            timerToVanish = timeToVanish;
            startCount = true;
            selfImage.enabled = true;
            selfImage.color = selfColor;
            StopAllCoroutines();
        }
        
        else
        {
            timerToVanish = timeToVanish;
            startCount = true;
            canvasGroup.alpha = 1f;
            StopAllCoroutines();
        }
    }

    public void PermanentlyShowHP()
    {
        if(!isCanvasGroup)
        {
            startCount = false;
            selfImage.enabled = true;
            selfImage.color = selfColor;
        }
        else
        {
            canvasGroup.alpha = 1f;
        }
    }
    
    // não apaga
    public IEnumerator HideHealthbar()
    {
        float savedAlpha = selfColor.a;
        Debug.Log("começou coroutine");
        while(selfColor.a > 0)
        {
            Debug.Log(selfColor.a);
            Debug.Log(savedAlpha);
            float alphaColor = Mathf.MoveTowards(savedAlpha, 0, Time.deltaTime);
            selfColor.a = savedAlpha;
            yield return null;
        }
        Debug.Log("acabou while");
        yield break;
    }


    // essa era a principal antes, não apaga pois serve de referência
    public IEnumerator ChangeImageAlpha(float oldValue, float newValue, float duration) 
    {
        for (float t = 0f; t < duration; t += Time.deltaTime) 
        {
            float y = Mathf.Lerp(oldValue, newValue, t / duration);
            selfImage.color = new Color(r1, g1, b1, y);
            yield return null;
        }
    }

    public IEnumerator ChangeCanvasGroupAlpha(float oldValue, float newValue, float duration) 
    {
        for (float t = 0f; t < duration; t += Time.deltaTime) 
        {
            float y = Mathf.Lerp(oldValue, newValue, t / duration);
            canvasGroup.alpha = y;
            yield return null;
        }
    }
}
