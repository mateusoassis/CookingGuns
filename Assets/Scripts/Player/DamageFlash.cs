using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Color origColor;
    float flashTime = 0.15f;


    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        origColor = meshRenderer.material.color;
    }

    public void FlashStart()
    {
        meshRenderer.material.color = Color.white;
        Invoke("FlashStop", flashTime);
    }
    public void FlashStop()
    {
        meshRenderer.material.color = origColor;
    }
}
