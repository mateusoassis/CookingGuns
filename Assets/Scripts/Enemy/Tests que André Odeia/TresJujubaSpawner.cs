using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresJujubaSpawner : MonoBehaviour
{
    public Transform[] jujubas;
    public Transform[] spawns;

    public void Start()
    {
        for (int i = 0; i < jujubas.Length; i++)
        {
             int randomInt = Random.Range(0, jujubas.Length);
             Transform tempTransform = jujubas[randomInt];
             jujubas[randomInt] = jujubas[i];
             jujubas[i] = tempTransform;
        }

        for(int i = 0; i < jujubas.Length; i++)
        {
            jujubas[i].position = spawns[i].position;
        }
    }
}
