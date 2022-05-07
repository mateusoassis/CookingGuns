using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorRandomizer : MonoBehaviour
{
    public string target;
    [SerializeField] private Material[] foraMaterial;
    [SerializeField] private Material[] recheioMaterial;

    [SerializeField] private MeshRenderer[] foraObjects;
    [SerializeField] private MeshRenderer[] recheioObjects;

    private int foraRandomizer;
    private int recheioRandomizer;

    void Start()
    {
        for(int i = 0; i < foraObjects.Length; i++)
        {
            foraRandomizer = Random.Range(0, foraMaterial.Length);
            foraObjects[i].material = foraMaterial[foraRandomizer];
        }
        for(int i = 0; i < recheioObjects.Length; i++)
        {
            recheioRandomizer = Random.Range(0, recheioMaterial.Length);
            recheioObjects[i].material = recheioMaterial[recheioRandomizer];
        }
    }
}
