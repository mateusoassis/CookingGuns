using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    [SerializeField] private float trapActivationSpeed;
    [SerializeField] private float trapDelay;
    [SerializeField] private Transform spikeTarget;
    [SerializeField] private Transform spikeInitialPosition;
    [SerializeField] private GameObject spikeTrap;

    void Awake()
    {
        spikeTrap.transform.position = spikeInitialPosition.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"||other.gameObject.tag == "Enemy")
        {
            StartCoroutine("ActivateTrap");
        }
    }


    private IEnumerator ActivateTrap()
    {
        Vector3 target = spikeTarget.transform.position;
        yield return new WaitForSeconds(trapDelay);
        spikeTrap.transform.position = Vector3.MoveTowards(spikeTrap.transform.position, spikeTarget.transform.position, trapActivationSpeed);
        yield return new WaitForSeconds(trapDelay);
        spikeTrap.transform.position = Vector3.MoveTowards(spikeTrap.transform.position, spikeInitialPosition.transform.position, trapActivationSpeed);
    }
}
