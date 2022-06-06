using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public float delayToDissolve;
    public float dissolveSpeed;
    public float delayTimer;

    private int i;

    private float sliderValue;

    [SerializeField] private float decreaseSpeed;

    [SerializeField] private SkinnedMeshRenderer[] meshParts;

    [SerializeField] private Material[] dissolveMaterials;

    void Awake()
    {
        dissolveMaterials = new Material[meshParts.Length];

        for (i = 0; i < meshParts.Length; i++)
        {
            dissolveMaterials[i] = meshParts[i].GetComponent<SkinnedMeshRenderer>().material;
        }

        /*for (int i = 0; i < dissolveMaterials.Length; i++)
        {
            dissolveMaterials[i].SetFloat("AlphaControl_", -1.0f);
        }*/
    }

    void Start()
    {
        //Debug.Log(dissolveMaterials[0].GetFloat("AlphaControl_"));

        /*
        for(int i = 0; i < playerRenderers.Length; i++)
        {
            for(int n = 0; n< playerRenderers.Length; n++)
            {
                oldMaterials[n] = playerRenderers[n].GetComponent<SkinnedMeshRenderer>().material;
            }
        }

        StartCoroutine(DissolveNow());
        */
    }
    
    /*
    public IEnumerator DissolveNow()
    {
        yield return new WaitForSeconds(delayToDissolve);
        foreach(SkinnedMeshRenderer k in playerRenderers)
        {
            k.GetComponent<SkinnedMeshRenderer>().material = dissolveMaterial;
        }
    }
    */

    void Update()
    {
        //sliderValue -= Time.deltaTime;

        AlphaSlider();

        /*delayTimer += Time.deltaTime;

        if(delayTimer > delayToDissolve)
        {
            for(int i = 0; i < dissolveMaterials.Length; i++)
            {
                dissolveMaterials[i].SetFloat("AlphaControl_", Mathf.MoveTowards(dissolveMaterials[i].GetFloat("AlphaControl_"), 1.0f, Time.deltaTime * dissolveSpeed/5));
            }
            Debug.Log(dissolveMaterials[0].GetFloat("AlphaControl_"));
        }*/
    }

    private void AlphaSlider() 
    {
        Color color = dissolveMaterials[0].color;
        color.a -= decreaseSpeed * Time.deltaTime;
        dissolveMaterials[0].color = color;

        Color color1 = dissolveMaterials[1].color;
        color1.a -= decreaseSpeed * Time.deltaTime;
        dissolveMaterials[1].color = color1;

        Color color2 = dissolveMaterials[2].color;
        color2.a -= decreaseSpeed * Time.deltaTime;
        dissolveMaterials[2].color = color2;

        Color color3 = dissolveMaterials[3].color;
        color3.a -= decreaseSpeed * Time.deltaTime;
        dissolveMaterials[3].color = color3;

        Color color4 = dissolveMaterials[4].color;
        color4.a -= decreaseSpeed * Time.deltaTime;
        dissolveMaterials[4].color = color4;

        Color color5 = dissolveMaterials[5].color;
        color5.a -= decreaseSpeed * Time.deltaTime;
        dissolveMaterials[5].color = color5;

        Color color6 = dissolveMaterials[6].color;
        color6.a -= decreaseSpeed * Time.deltaTime;
        dissolveMaterials[6].color = color6;

        Destroy(meshParts[2].gameObject);
        Destroy(meshParts[4].gameObject);
        Destroy(meshParts[6].gameObject);

    }
}
