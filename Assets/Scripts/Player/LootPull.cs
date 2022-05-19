using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootPull : MonoBehaviour
{
    public LootContainer lootContainerScript;
    public float duration;
    public float passedDuration;
    public int index;
    public float fadeoutDuration;

    public RectTransform targetParentRect;
    public Vector2 savedStartParentRect;

    public Vector3 savedPos;

    public bool vanishing;

    public int quantity;
    public TextMeshProUGUI lootText;

    public Image lootImage;
    public CanvasGroup canvasGroup;
    public Transform targetParent;

    private RectTransform rect;

    void Awake()
    {
        lootContainerScript = GameObject.Find("LootTransform").GetComponent<LootContainer>();
        canvasGroup = GetComponent<CanvasGroup>();
        rect = GetComponent<RectTransform>();
    }

    void Start()
    {
        //savedY = transform.position.y;
    }

    public void UpdateValuesAndTexture(int indexForItem, int n, float j)
    {
        lootImage.sprite = lootContainerScript.dropIcons[n];
        lootText.text = quantity.ToString();
        duration = j;
        passedDuration = j;
        targetParent = lootContainerScript.lootTransforms[indexForItem];
        //targetParentRect = targetParent.GetComponent<RectTransform>();
        //savedStartParentRect = targetParentRect.position;
        index = indexForItem;
    }

    void Update()
    {
        duration -= Time.deltaTime;

        if(duration < 0 && !vanishing)
        {
            savedPos = rect.position;
            lootContainerScript.DeleteThisIconFromEverything(index);
            transform.SetParent(targetParent, true);
            //transform.SetAsFirstSibling();
            rect.position = savedPos;
            vanishing = true;
        }

        if(vanishing)
        {
            rect.localPosition = new Vector2(rect.localPosition.x, rect.localPosition.y) + Vector2.up * 1000f * Time.deltaTime;
            //transform.position += transform.up * 2f * Time.deltaTime;
            canvasGroup.alpha = 1 + (duration/fadeoutDuration);
        }

        if(canvasGroup.alpha <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        //lootContainerScript.
        //targetParentRect.position = savedStartParentRect;
    }

    public void DurationRefresh()
    {
        duration = passedDuration;
        quantity++;
        lootText.text = quantity.ToString();
    }
}
