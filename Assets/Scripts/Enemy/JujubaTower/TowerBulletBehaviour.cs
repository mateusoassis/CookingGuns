using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBulletBehaviour : MonoBehaviour
{
    public Vector3 centerPivot;
    public float centerOffset;
    public Transform parent;
    public int jujubaType;

    public Vector3 savedSlerpPosition;

    public float slerpTime;
    public float currentSlerpTime;
    public float slerpSpeed;
    // 1 = azul
    // 2 = verde
    // 3 = vermelha

    void Start()
    {
        parent = transform.parent.GetComponent<Transform>();
        currentSlerpTime = 0f;
        savedSlerpPosition = parent.gameObject.GetComponent<TowerBehaviour>().slerpTarget.position;
    }

    void Update()
    {
        SlerpTimer();
        centerPivot = (transform.position + savedSlerpPosition)/2;
        centerPivot -= new Vector3(0, -centerOffset);

        Vector3 relativeStart = transform.position - centerPivot;
        Vector3 relativeEnd = savedSlerpPosition - centerPivot;

        transform.position = Vector3.Slerp(relativeStart, relativeEnd, (currentSlerpTime*currentSlerpTime)/5) + centerPivot;
        //transform.position = Vector3.Slerp(transform.position, parent.gameObject.GetComponent<TowerBehaviour>().slerpTarget.position, Time.deltaTime);
    }

    public void SlerpTimer()
    {
        currentSlerpTime += Time.deltaTime;
        if(currentSlerpTime > slerpTime)
        {
            currentSlerpTime = slerpTime;
        }

        slerpSpeed = currentSlerpTime / slerpTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            GameObject jujuba = Instantiate(parent.gameObject.GetComponent<TowerBehaviour>().spawnObject, transform.position + new Vector3(0f, 0.67f, 0f), parent.rotation);
            jujuba.transform.SetParent(parent);
            Destroy(this.gameObject);
        }
    }
}
