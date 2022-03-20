using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 targetPosition;
    public int setBehaviour;
    public bool isPlayerOnRange;
    public Rigidbody enemyBulletPrefab;

    public float enemySpeed;
    public float stopDistance;
    public float retreatDistance;

    [Header("Behaviour 3")]
    public GameObject explosionObject;
    public Transform explosionObjectTransform;
    public MeshRenderer explosionObjectMesh;
    public float scaleExplosion;
    public float explosionRange;
    public float explosionTimerReset;
    public float explosionTimer;
    public float explosionSpeed;
    private Vector3 targetedVector;
    private Vector3 scaleVector;
    public bool explosionCollision;
    private EnemySpawner enemySpawner;
    [SerializeField] private bool blinking;

    [Header("Tiros")]
    public float timeBetweenShots;
    private float timeBetweenShotsTimer = 5f;

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        targetedVector = new Vector3(1f * scaleExplosion, 0.25f, 1f * scaleExplosion);
        scaleVector = new Vector3(1f, 0.25f, 1f);
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        blinking = false;
    }

    // 1 = shooting + follow
    // 2 = shooting + retreat
    // 3 = follow + explode

    void FixedUpdate()
    {
        // stop bem baixo para o inimigo nunca parar de ir pra cima do jogador, e retreat muito alto
        if(setBehaviour == 1)
        {
            if(Vector3.Distance(transform.position, playerTransform.position) > stopDistance)
            {
                Vector3 movePosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, movePosition, enemySpeed * Time.deltaTime);
                transform.LookAt(playerTransform.position, Vector3.up);
            }
            else if(Vector3.Distance(transform.position, playerTransform.position) < stopDistance && Vector3.Distance(transform.position, playerTransform.position) > retreatDistance)
            {
                transform.LookAt(playerTransform.position, Vector3.up);
            }
            else if(Vector3.Distance(transform.position, playerTransform.position) < retreatDistance)
            {
                Vector3 movePosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, movePosition, -enemySpeed * Time.deltaTime);
                transform.LookAt(playerTransform.position, Vector3.up);
            }
            Shoot();
        }
        else if(setBehaviour == 2)
        {
            // retreat bem baixo pra o inimigo nunca parar de "recuar", e stop muito alto
            if(Vector3.Distance(transform.position, playerTransform.position) > stopDistance)
            {
                Vector3 movePosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, movePosition, enemySpeed * Time.deltaTime);
                transform.LookAt(playerTransform.position, Vector3.up);
            }
            else if(Vector3.Distance(transform.position, playerTransform.position) < stopDistance && Vector3.Distance(transform.position, playerTransform.position) > retreatDistance)
            {
                transform.LookAt(playerTransform.position, Vector3.up);
            }
            else if(Vector3.Distance(transform.position, playerTransform.position) < retreatDistance)
            {
                transform.LookAt(playerTransform.position, Vector3.up);
                Vector3 movePosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, movePosition, -enemySpeed * Time.deltaTime);
            }
            Shoot();
        }
        else if(setBehaviour == 3)
        {
            if(!isPlayerOnRange)
            {
                if(Vector3.Distance(transform.position, playerTransform.position) > explosionRange)
                {
                    Vector3 movePosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, movePosition, enemySpeed * Time.deltaTime);
                    transform.LookAt(playerTransform.position, Vector3.up);
                }
                else if(Vector3.Distance(transform.position, playerTransform.position) <= explosionRange)
                {
                    StartExplosion();
                }
            }
            else if(isPlayerOnRange)
            {
                if(explosionTimer <= 0)
                {
                    Explode();
                }
                else if(explosionTimer > 0f)
                {
                    explosionTimer -= Time.deltaTime;
                    scaleVector = Vector3.MoveTowards(scaleVector, targetedVector, explosionSpeed * Time.fixedDeltaTime);
                    explosionObjectTransform.localScale = scaleVector;
                }
            }
        }
    }

    public void StartExplosion()
    {
        StartCoroutine("BlinkExplosionRange");
        isPlayerOnRange = true;
        explosionObjectMesh.enabled = true;
        explosionTimer = explosionTimerReset;
    }

    public void Explode()
    {
        if(explosionCollision)
        {
            Debug.Log("player toma dano");
        }
        enemySpawner.enemiesKilled++;
        Destroy(this.gameObject);
    }

    public void Shoot()
    {
        if(timeBetweenShotsTimer <= 0)
        {
            ShootProjectile();
        }
        else if(timeBetweenShotsTimer > 0)
        {
            timeBetweenShotsTimer -= Time.fixedDeltaTime;
        }
    }
    public void ShootProjectile()
    {
        Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        float u = Random.Range(timeBetweenShots-2f, timeBetweenShots+4f);
        timeBetweenShotsTimer = u;
    }

    public void Blink()
    {
        if(blinking)
        {
            explosionObjectMesh.enabled = false;
            blinking = false;
        }
        else if(!blinking)
        {
            explosionObjectMesh.enabled = true;
            blinking = true;
        }
    }
    
    public IEnumerator BlinkExplosionRange()
    {
        InvokeRepeating("Blink", 0.1f, 0.1f);
        yield return new WaitForSeconds(0.01f);
    }
}