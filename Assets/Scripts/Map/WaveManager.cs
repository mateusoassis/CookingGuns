using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private WavesSpawn wavesSpawnScript;


    private void Awake()
    {
        wavesSpawnScript = GameObject.Find("WaveManager").GetComponent<WavesSpawn>();
    }
    private void Update()
    {
        if (transform.childCount <= 0)
        {
            wavesSpawnScript.StartCoroutine("ControlSpawn");
            Destroy(this.gameObject);
        }
    }
}
