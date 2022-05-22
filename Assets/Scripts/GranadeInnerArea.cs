using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeInnerArea : MonoBehaviour
{
    [SerializeField] private float increaseSizeDuration;
    public bool isMaxSize;

    void Start()
    {
        isMaxSize = false;
        increaseSizeDuration = 0f;
        transform.rotation = Quaternion.identity;
        transform.localScale = new Vector3(0f, 0f, 0f);
    }

    void Update()
    {
        if (!isMaxSize)
        {
            increaseSizeDuration = 0.6f * Time.deltaTime;
            transform.localScale = transform.localScale + new Vector3(increaseSizeDuration, increaseSizeDuration, increaseSizeDuration);
        }
    }
}
