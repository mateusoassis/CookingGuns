using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    public Transform slerpTarget;
    public Transform shootPoint;
    public GameObject spawnObject;
    public GameObject bulletObject;
    public float delayToShoot;
    public float timer;

    public int amountSpawned;
    public int maxAmountSpawned;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > delayToShoot)
        {
            Shoot();
            timer = 0f;
        }
    }

    public void Shoot()
    {   
        if(amountSpawned < maxAmountSpawned)
        {
            amountSpawned++;
            GameObject bullet = Instantiate(bulletObject, shootPoint.position, Quaternion.identity);
            bullet.transform.SetParent(transform);
        }
    }
}
