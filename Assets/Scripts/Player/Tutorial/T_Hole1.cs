using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Hole1 : MonoBehaviour
{
    public Transform targetPos;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = targetPos.position;
        }
    }
}
