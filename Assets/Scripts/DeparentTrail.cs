using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeparentTrail : MonoBehaviour
{
    public Transform trailParent;
    public Vector3 fixedDistance;

    [SerializeField] private float deparentTime;
    void Awake()
    {
        trailParent = transform.parent;
    }

    private void Start()
    {
        StartCoroutine("WaitBeforeDeparent");
    }
    private void LateUpdate()
    {
        if(trailParent != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, trailParent.position, Time.deltaTime * 100f);
        }
    }

    public IEnumerator WaitBeforeDeparent() 
    {
        yield return new WaitForSeconds(deparentTime);
        transform.SetParent(null);
    }
}
