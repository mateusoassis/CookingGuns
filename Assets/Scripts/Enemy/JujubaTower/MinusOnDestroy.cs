using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusOnDestroy : MonoBehaviour
{
    public Transform parent;
    public ChargeJujubaBehaviour chargeJujubaBehaviour;

    void Start()
    {
        chargeJujubaBehaviour = GetComponent<ChargeJujubaBehaviour>();
        if(!chargeJujubaBehaviour.playerTransform.GetComponent<_PlayerManager>().testing)
        {
            parent = transform.parent.GetComponent<Transform>();
        }
        
    }

    void OnDestroy()
    {
        if(!chargeJujubaBehaviour.playerTransform.GetComponent<_PlayerManager>().testing)
        {
            parent.GetComponent<TowerBehaviour>().amountSpawned--;
        }
    }
}
