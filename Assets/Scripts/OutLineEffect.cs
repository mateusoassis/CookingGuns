using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutLineEffect : MonoBehaviour
{
    public Material mat;
    public float thickness = 1.03f;
    [ColorUsage(true, true)]
    public Color colorOutline;
    private Renderer rend;

    private void Awake()
    {
        GameObject outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation);
        outlineObject.transform.localScale = new Vector3(1, 1, 1);
        Renderer rend = outlineObject.GetComponent<Renderer>();
        rend.material = mat;
        rend.material.SetFloat("Thickness_", thickness);
        rend.material.SetColor("OutlineColor_", colorOutline);
        rend.enabled = false;
        outlineObject.GetComponent<Collider>().enabled = false;
        outlineObject.GetComponent<OutLineEffect>().enabled = false;
        this.rend = rend;
    }

    private void OnMouseExit()
    {
        rend.enabled = false;
    }

    private void OnMouseEnter()
    {
        rend.enabled = true;
    }
}
