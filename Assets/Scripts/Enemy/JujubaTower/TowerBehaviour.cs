using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    [Header("Sair do chão")]
    public float goUpVelocity;
    public float playerDistance;
    public bool isPlayerOnRange;
    public bool switchingPlaces;
    public bool modelReady;
    private BoxCollider boxCollider;
    public Transform modelTransform;
    public Vector3 startPosition;
    public Vector3 endPosition;
    public Transform startObject;
    public Transform endObject;

    [Header("Variáveis de comportamento da torre")]
    public Transform slerpTarget;
    public Transform shootPoint;
    public GameObject spawnObject;
    public GameObject bulletObject;
    public float delayToShoot;
    public float timer;

    public int amountSpawned;
    public int maxAmountSpawned;

    public bool towerDamaged;

    public Transform player;

    void Awake()
    {   
        player = GameObject.Find("Player").GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider>();
        startPosition = startObject.position;
        endPosition = endObject.position;
    }

    void Start()
    {
        modelTransform.position = startPosition;
        boxCollider.enabled = true;
        
    }

    void Update()
    {
        if(modelReady)
        {
            timer += Time.deltaTime;
            if(timer > delayToShoot)
            {
                Shoot();
                timer = 0f;
            }
        }
        
        if(!isPlayerOnRange)
        {
            UpdatePlayerDistance();
        }

        if(switchingPlaces)
        {
            if(modelTransform.position != endPosition)
            {
                modelTransform.position = Vector3.MoveTowards(modelTransform.position, endPosition, goUpVelocity * Time.deltaTime);
            }
            else
            {
                modelReady = true;
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z), transform.up);
                boxCollider.enabled = true;
                switchingPlaces = false;
            }
        }
    }

    public void UpdatePlayerDistance()
    {
        if(Vector3.Distance(transform.position, player.position) < playerDistance)
        {
            isPlayerOnRange = true;
            switchingPlaces = true;
        }
    }

    public void Shoot()
    {   
        if(amountSpawned < maxAmountSpawned)
        {
            FindObjectOfType<SoundManager>().PlayOneShot("TowerThrow");
            amountSpawned++;
            GameObject bullet = Instantiate(bulletObject, shootPoint.position, Quaternion.identity);
            bullet.transform.SetParent(transform);
        }
    }
}
