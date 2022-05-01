using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeScript : MonoBehaviour
{
    public Vector3 savedSlerpPosition;

    public GameObject explosionArea;
    public Transform explosionAreaTransform;

    public Vector3 centerPivot;
    public float centerOffset;
    public Transform parent;
    public float slerpTime;
    public float currentSlerpTime;
    public float slerpSpeed;
    public float spawnYOffset;
    public float projectileSpeed;

    void Start()
    {
        parent = transform.parent;
        currentSlerpTime = 0f;
        savedSlerpPosition = parent.gameObject.GetComponent<Transform>().position + new Vector3(0f, spawnYOffset, 0f);
    }

    void Update()
    {
        SlerpTimer();
        centerPivot = (transform.position + savedSlerpPosition)/2;
        centerPivot -= new Vector3(0, -centerOffset);

        Vector3 relativeStart = transform.position - centerPivot;
        Vector3 relativeEnd = savedSlerpPosition - centerPivot;

        float distance = Vector3.Distance(transform.position, savedSlerpPosition);
        float finalSpeed = (distance / projectileSpeed);

        transform.position = Vector3.Slerp(relativeStart, relativeEnd, Time.deltaTime/finalSpeed) + centerPivot;
    }

    public void SlerpTimer()
    {
        currentSlerpTime += Time.deltaTime;
        if(currentSlerpTime > slerpTime)
        {
            currentSlerpTime = slerpTime;
        }

        slerpSpeed = currentSlerpTime / slerpTime;
        transform.SetParent(null);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            transform.rotation = Quaternion.identity;
            explosionArea.SetActive(true);
        } 
    }
}
