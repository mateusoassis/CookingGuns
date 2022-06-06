using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public float delayToDissolve;
    public float dissolveSpeed;
    public float delayTimer;

    public Material[] dissolveMaterialsCorrectNow;

    void Awake()
    {
        //oldMaterials = new Material[7];
        for(int i = 0; i < dissolveMaterialsCorrectNow.Length; i++)
        {
            dissolveMaterialsCorrectNow[i].SetFloat("AlphaControl_", -1.0f);
        }
    }

    void Start()
    {
        Debug.Log(dissolveMaterialsCorrectNow[0].GetFloat("AlphaControl_"));
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
        delayTimer += Time.deltaTime;

        if(delayTimer > delayToDissolve)
        {
            for(int i = 0; i < dissolveMaterialsCorrectNow.Length; i++)
            {
                dissolveMaterialsCorrectNow[i].SetFloat("AlphaControl_", Mathf.MoveTowards(dissolveMaterialsCorrectNow[i].GetFloat("AlphaControl_"), 1.0f, Time.deltaTime * dissolveSpeed/5));
            }
            Debug.Log(dissolveMaterialsCorrectNow[0].GetFloat("AlphaControl_"));
        }
    }
}
