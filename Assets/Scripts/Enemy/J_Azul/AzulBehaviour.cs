using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AzulBehaviour : MonoBehaviour
{
    private Transform playerTransform;
    public bool isPlayerOnRange;
    public float focusPlayerDistance;
    public Rigidbody enemyBulletPrefab;

    private float enemySpeed;
    public float enemyMaxSpeed;
    
    public float stopDistance;
    public float retreatDistance;

    public Transform firePoint;

    [Header("Tiros")]
    public float timeBetweenShots;
    private float timeBetweenShotsTimer;
    public float randomExtraTimeBetweenShots;

    [Header("Movimentação behaviour 2")]
    public float countToMove;
    private float countToMoveTimer;
    public float retreatCooldown;
    private float retreatCooldownTimer;
    public bool retreating;
    public bool retreatingOnCooldown;

    private Animator enemyAnimator;

    private bool canMove;
    private NavMeshAgent navMesh;
    public bool ableToPatrol;

    void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        
        enemyAnimator = GetComponent<Animator>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        canMove = true;
        enemySpeed = enemyMaxSpeed;
        timeBetweenShotsTimer = timeBetweenShots + Random.Range(-randomExtraTimeBetweenShots, randomExtraTimeBetweenShots);
        navMesh.speed = enemyMaxSpeed;
        ableToPatrol = true;
    }

    // 1 = shooting + follow (o que fica de perto)
    // 2 = shooting + retreat (o que fica de longe)
    // 3 = follow + explode (o que explode)

    void Update()
    {
        if(Vector3.Distance(playerTransform.position, transform.position) < focusPlayerDistance)
        {
            isPlayerOnRange = true;
        }
    }

    void FixedUpdate()
    {
        if(isPlayerOnRange)
        {
            ableToPatrol = false;
            if(!playerTransform.GetComponent<_PlayerManager>().isFading)
            {         
                if(canMove)
                {
                    // retreat bem baixo pra o inimigo nunca parar de "recuar", e stop muito alto
                    if(Vector3.Distance(transform.position, playerTransform.position) >= stopDistance)
                    {
                        Vector3 directionToPlayer = playerTransform.position - transform.position;
                        Vector3 newPos = transform.position + directionToPlayer.normalized;
                        /*
                        Vector3 movePosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, movePosition, enemySpeed * Time.fixedDeltaTime);
                        */
                        
                        
                        
                        navMesh.isStopped = false;
                        retreating = false;

                        navMesh.SetDestination(newPos);
                        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), Vector3.up);
                    }
                    else if(Vector3.Distance(transform.position, playerTransform.position) < stopDistance && Vector3.Distance(transform.position, playerTransform.position) > retreatDistance)
                    {
                        navMesh.isStopped = true;
                        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), Vector3.up);
                        retreating = false;
                    }
                    else if(Vector3.Distance(transform.position, playerTransform.position) <= retreatDistance)
                    {
                        Vector3 directionToPlayer = transform.position - playerTransform.position;
                        Vector3 newPos = transform.position + directionToPlayer.normalized;
                        
                        
                        /*
                        Vector3 movePosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, movePosition, -enemySpeed * Time.fixedDeltaTime);
                        */
                        navMesh.isStopped = false;
                        retreating = true;
                        retreatingOnCooldown = true;

                        navMesh.SetDestination(newPos);
                        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), Vector3.up);
                    }
                }

                if(retreatingOnCooldown)
                {
                    retreatCooldownTimer += Time.fixedDeltaTime;
                    if(retreatCooldownTimer >= retreatCooldown)
                    {
                        canMove = false;
                        navMesh.isStopped = true;
                    }

                    if(!canMove)
                    {
                        countToMoveTimer += Time.fixedDeltaTime;
                        if(countToMoveTimer >= countToMove)
                        {
                            canMove = true;
                            navMesh.isStopped = false;
                            retreatingOnCooldown = false;
                            countToMoveTimer = 0f;
                            retreatCooldownTimer = 0f;
                        }
                    }
                }
                Shoot();
            }
            else
            {
                navMesh.isStopped = true;
            }
        }
        else
        {
            if(ableToPatrol)
            {
                Vector3 targetWalk;
                float randomRangeX = Random.Range(-2f, 2f);
                float randomRangeZ = Random.Range(-2f, 2f);
                targetWalk = transform.position + new Vector3(randomRangeX, 0f, randomRangeZ);
                transform.LookAt(targetWalk, transform.up);
                StartCoroutine(WalkTowards(targetWalk));
                ableToPatrol = false;
            }
        }
        
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

    public void ZeroMovespeed()
    {
        enemySpeed = 0f;
    }
    public void NormalMovespeed()
    {
        enemySpeed = enemyMaxSpeed;
    }

    public IEnumerator WalkTowards(Vector3 walkTarget)
    {
        var t = 0f;
        var start = transform.position;
        var timeToWalk = 2f;

        while (t < timeToWalk)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, walkTarget, t);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        ableToPatrol = true;
    }
}
