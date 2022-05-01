using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLive : MonoBehaviour
{
    public float timeBeforeDestroy;

    void Start()
    {
        StartCoroutine("TimeToDestroy");
    }

    
    public IEnumerator TimeToDestroy()
    {
        yield return new WaitForSeconds(timeBeforeDestroy);
        Destroy(this.gameObject);
    }
}
