using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusOnDestroy : MonoBehaviour
{
    public Transform parent;

    void Start()
    {
        parent = transform.parent.GetComponent<Transform>();
    }

    void OnDestroy()
    {
        parent.GetComponent<TowerBehaviour>().amountSpawned--;
    }
}
