using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public float delayToDissolve;
    public Material dissolveMaterial;
    public SkinnedMeshRenderer[] playerRenderers;

    public Material[] oldMaterials;

    void Awake()
    {
        oldMaterials = new Material[7];
    }

    void Start()
    {
        for(int i = 0; i < playerRenderers.Length; i++)
        {
            for(int n = 0; n< playerRenderers.Length; n++)
            {
                oldMaterials[n] = playerRenderers[n].GetComponent<SkinnedMeshRenderer>().material;
            }
        }

        StartCoroutine(DissolveNow());
    }

    public IEnumerator DissolveNow()
    {
        yield return new WaitForSeconds(delayToDissolve);
        foreach(SkinnedMeshRenderer k in playerRenderers)
        {
            k.GetComponent<SkinnedMeshRenderer>().material = dissolveMaterial;
        }
    }
}
