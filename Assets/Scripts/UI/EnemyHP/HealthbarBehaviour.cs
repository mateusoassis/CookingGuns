using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehaviour : MonoBehaviour
{
    public BillboardCanvas canvas;
    public Image selfImage;
    public Color selfColor;
    public float r;
    public float g;
    public float b;

    public float timeToVanish;
    public float timerToVanish;
    
    public bool startCount;

    void Start()
    {
        selfImage.enabled = false;
        canvas = GetComponent<BillboardCanvas>();
        selfColor = selfImage.color;
        r = selfImage.color.r;
        g = selfImage.color.g;
        b = selfImage.color.b;
    }

    void Update()
    {
        if(startCount)
        {
            timerToVanish -= Time.deltaTime;

            if(timerToVanish < 0)
            {
                //selfImage.enabled = false;
                StartCoroutine(ChangeImageAlpha(1f, 0f, 1f));
                startCount = false;
            }
        }
        
    }

    public void StartCount()
    {
        timerToVanish = timeToVanish;
        startCount = true;
        selfImage.enabled = true;
        selfImage.color = selfColor;
        StopAllCoroutines();
    }

    public void PermanentlyShowHP()
    {
        startCount = false;
        selfImage.enabled = true;
        selfImage.color = selfColor;
    }

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

    public IEnumerator ChangeImageAlpha(float oldValue, float newValue, float duration) 
    {
        for (float t = 0f; t < duration; t += Time.deltaTime) 
        {
            float y = Mathf.Lerp(oldValue, newValue, t / duration);
            selfImage.color = new Color(r, g, b, y);
            yield return null;
        }
    }
}
