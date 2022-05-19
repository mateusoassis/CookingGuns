using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootPull : MonoBehaviour
{
    public LootContainer lootContainerScript;
    public float duration;

    public int quantity;
    public TextMeshProUGUI lootText;

    public Image lootImage;
    public CanvasGroup canvasGroup;

    void Awake()
    {
        lootContainerScript = GameObject.Find("LootTransform").GetComponent<LootContainer>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void UpdateValuesAndTexture(int indexForItem, int n, float j)
    {
        lootImage.sprite = lootContainerScript.dropIcons[n];
        lootText.text = quantity.ToString();
        duration = j;
    }

    void Update()
    {
        duration -= Time.deltaTime;
        if(duration < 0)
        {
            transform.position += transform.up * 2f * Time.deltaTime;
            canvasGroup.alpha = 1 + (duration)/2;
        }

        if(canvasGroup.alpha <= 0)
        {
            Destroy(gameObject);
        }
    }
}
