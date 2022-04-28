using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartAnimatorScript : MonoBehaviour
{
    public Transform parent;
    public int state = 0;
    // 0 normal
    // 1 desativado
    public int index;

    void Awake()
    {
        parent = transform.parent.GetComponent<Transform>();
    }

    void Start()
    {
        CheckIfActiveOrNot();
    }

    public void ResetNormal()
    {
        GetComponent<Animator>().ResetTrigger("Normal");
    }

    public void ResetDisabled()
    {
        GetComponent<Animator>().ResetTrigger("Disabled");
    }

    public void CheckIfActiveOrNot()
    {
        
        if(index <= parent.gameObject.GetComponent<HeartContainerManager>().hpLost)
        {
            GetComponent<Animator>().SetTrigger("Disabled");
            state = 1;
        }
        
        if(index > parent.gameObject.GetComponent<HeartContainerManager>().hpLost)
        {
            GetComponent<Animator>().SetTrigger("Normal");
            state = 0;
        }
    }

    public void HealThisHeart()
    {
        GetComponent<Animator>().SetTrigger("Normal");
        state = 1;
    }
}
