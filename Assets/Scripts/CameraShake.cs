using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraShake : MonoBehaviour
{
    [SerializeField]private UnityEvent shock;

    public void Shockwave()
    {
        shock.Invoke();
    }
}
