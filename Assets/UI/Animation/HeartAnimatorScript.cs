using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartAnimatorScript : MonoBehaviour
{
    public Transform parent;
    public int index;

    public void ResetNormal()
    {
        GetComponent<Animator>().ResetTrigger("Normal");
    }

    public void ResetDisabled()
    {
        GetComponent<Animator>().ResetTrigger("Disabled");
    }
}
