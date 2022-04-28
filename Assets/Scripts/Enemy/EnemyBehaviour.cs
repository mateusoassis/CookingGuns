using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 targetPosition;
    public int setBehaviour;
    // 1 = shooting + follow (o que fica de perto)
    // 2 = shooting + retreat (o que fica de longe)
    // 3 = follow + explode (o que explode)
    public bool isPlayerOnRange;
    public Rigidbody enemyBulletPrefab;

    public float enemySpeed;
    public float enemyMaxSpeed;
    
    public float stopDistance;
    public float retreatDistance;

    public Transform firePoint;

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
    public int explosionDamage;

    [Header("Tiros")]
    public float timeBetweenShots;
    public float timeBetweenShotsTimer;
    public float randomExtraTimeBetweenShots;

    [Header("Movimentação behaviour 2")]
    public float countToMove;
    private float countToMoveTimer;
    public float retreatCooldown;
    private float retreatCooldownTimer;
    public bool retreating;
    public bool retreatingOnCooldown;

    public Animator enemyAnimator;

    public bool canMove;

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        targetedVector = new Vector3(1f * scaleExplosion, 0.25f, 1f * scaleExplosion);
        scaleVector = new Vector3(1f, 0.25f, 1f);
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        blinking = false;
        enemyAnimator = GetComponent<Animator>();
        canMove = true;
        enemySpeed = enemyMaxSpeed;
        timeBetweenShotsTimer = timeBetweenShots + Random.Range(-randomExtraTimeBetweenShots, randomExtraTimeBetweenShots);
    }

    // 1 = shooting + follow (o que fica de perto)
    // 2 = shooting + retreat (o que fica de longe)
    // 3 = follow + explode (o que explode)

    void FixedUpdate()
    {

        if(!playerTransform.GetComponent<_PlayerManager>().isFading)
        {
            // stop bem baixo para o inimigo nunca parar de ir pra cima do jogador, e retreat muito alto
            if(setBehaviour == 1)
            {
                if(canMove)
                {
                    if(Vector3.Distance(transform.position, playerTransform.position) >= stopDistance)
                    {
                        Vector3 movePosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, movePosition, enemySpeed * Time.deltaTime);
                        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), Vector3.up);
                    }
                    else if(Vector3.Distance(transform.position, playerTransform.position) < stopDistance && Vector3.Distance(transform.position, playerTransform.position) > retreatDistance)
                    {
                        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), Vector3.up);
                    }
                    else if(Vector3.Distance(transform.position, playerTransform.position) <= retreatDistance)
                    {
                        Vector3 movePosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, movePosition, -enemySpeed * Time.deltaTime);
                        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), Vector3.up);
                    }
                }
                
                Shoot();
            }
            else if(setBehaviour == 2)
            {
                if(canMove)
                {
                    // retreat bem baixo pra o inimigo nunca parar de "recuar", e stop muito alto
                    if(Vector3.Distance(transform.position, playerTransform.position) >= stopDistance)
                    {
                        Vector3 movePosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, movePosition, enemySpeed * Time.deltaTime);
                        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), Vector3.up);
                        retreating = false;
                    }
                    else if(Vector3.Distance(transform.position, playerTransform.position) < stopDistance && Vector3.Distance(transform.position, playerTransform.position) > retreatDistance)
                    {
                        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), Vector3.up);
                        retreating = false;
                    }
                    else if(Vector3.Distance(transform.position, playerTransform.position) <= retreatDistance)
                    {
                        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), Vector3.up);
                        Vector3 movePosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, movePosition, -enemySpeed * Time.deltaTime);
                        retreating = true;
                        retreatingOnCooldown = true;
                    }
                }

                if(retreatingOnCooldown)
                {
                    retreatCooldownTimer += Time.deltaTime;
                    if(retreatCooldownTimer >= retreatCooldown)
                    {
                        canMove = false;
                    }

                    if(!canMove)
                    {
                        countToMoveTimer += Time.deltaTime;
                        if(countToMoveTimer >= countToMove)
                        {
                            canMove = true;
                            retreatingOnCooldown = false;
                            countToMoveTimer = 0f;
                            retreatCooldownTimer = 0f;
                        }
                    }
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
                        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), Vector3.up);
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
    }

    public IEnumerator CountToStopMoving()
    {
        yield return new WaitForSeconds(countToMove);
        canMove = false;
        yield return new WaitForSeconds(retreatCooldown);
        canMove = true;
    }

    public void StartExplosion()
    {
        enemyAnimator.SetTrigger("Explode");
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
        if(GetComponent<MinusOnDestroy>() == null)
        {
            enemySpawner.enemiesKilled++;
        }
        Destroy(this.gameObject);
    }

    public void Shoot()
    {
        if(timeBetweenShotsTimer <= 0 && canMove)
        {
            canMove = false;
            enemyAnimator.SetTrigger("Shoot");
        }
        else if(timeBetweenShotsTimer > 0 && canMove)
        {
            timeBetweenShotsTimer -= Time.fixedDeltaTime;
        }
    }
    public void ShootProjectile()
    {
        Instantiate(enemyBulletPrefab, firePoint.position, Quaternion.identity);
        float u = Random.Range(timeBetweenShots, timeBetweenShots + randomExtraTimeBetweenShots);
        timeBetweenShotsTimer = u;
        canMove = true;
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

    public void ZeroMovespeed()
    {
        enemySpeed = 0f;
    }
    public void NormalMovespeed()
    {
        enemySpeed = enemyMaxSpeed;
    }
}