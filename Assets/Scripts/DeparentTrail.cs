using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeparentTrail : MonoBehaviour
{
    private Transform trailParent;
    private Vector3 fixedDistance;

    [SerializeField] private float deparentTime;
    void Awake()
    {
        trailParent = GetComponentInParent<Transform>();
    }

    private void Start()
    {
        StartCoroutine("WaitBeforeDeparent");
    }
    private void LateUpdate()
    {
        transform.position = trailParent.position;
    }

    public IEnumerator WaitBeforeDeparent() 
    {
        yield return new WaitForSeconds(deparentTime);
        trailParent.SetParent(null);
    }
}
