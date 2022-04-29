using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouLoseHolder : MonoBehaviour
{
    public GameObject youLoseObject;
    
    void Awake()
    {
        youLoseObject.GetComponentInChildren<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
    }
}
