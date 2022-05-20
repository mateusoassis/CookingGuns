using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusOnDestroy : MonoBehaviour
{
    public Transform parent;
    public ChargeJujubaBehaviour chargeJujubaBehaviour;
    public Transform playerTransform;

    void Awake()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Start()
    {
        //chargeJujubaBehaviour = GetComponent<ChargeJujubaBehaviour>();
        if(!playerTransform.GetComponent<_PlayerManager>().testing)
        {
            parent = transform.parent.GetComponent<Transform>();
        }
        
    }

    void OnDestroy()
    {
        if(playerTransform != null)
        {
            if(!playerTransform.GetComponent<_PlayerManager>().testing)
            {
                if(parent != null)
                {
                    if(parent.gameObject.TryGetComponent(out TowerBehaviour towerBehaviour))
                    {
                        towerBehaviour.amountSpawned--;   
                    }
                }
            }
        }  
    }
}
