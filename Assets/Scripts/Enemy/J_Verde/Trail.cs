using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
    public Transform parent;

    void Awake()
    {
        transform.parent = null;
    }

    void Start()
    {
        if(parent.transform == null)
        {
            Destroy(gameObject);
        }
    }   

    void LateUpdate()
    {
        if(parent.transform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, parent.position, Time.deltaTime * 25f);
            transform.LookAt(parent.position);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
