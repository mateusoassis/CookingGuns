using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootBillboard : MonoBehaviour
{
    private Transform cam;

    public void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    public void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
