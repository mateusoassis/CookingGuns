using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AzulBehaviour : MonoBehaviour
{
    private Transform playerTransform;
    public bool isPlayerOnRange;
    public float focusPlayerDistance;
    public float shootPlayerDistance;
    public Rigidbody enemyBulletPrefab;

    private float enemySpeed;
    public float enemyMaxSpeed;
    
    public float stopDistance;
    public float retreatDistance;

    public Transform firePoint;

    [SerializeField] private ParticleSystem enemySpawnParticle;

    [Header("Tiros")]
    public float timeBetweenShots;
    private float timeBetweenShotsTimer;
    public float randomExtraTimeBetweenShots;
    [SerializeField] private Transform firePointLeft;
    [SerializeField] private Transform firePointMiddle;
    [SerializeField] private Transform firePointRight;

    [Header("Movimentação behaviour 2")]
    public float countToMove;
    private float countToMoveTimer;
    public float retreatCooldown;
    private float retreatCooldownTimer;
    public bool retreating;
    public bool retreatingOnCooldown;

    private Animator enemyAnimator;
    private EnemyStats enemyStatsScript;

    private bool canMove;
    private NavMeshAgent navMesh;
    public bool ableToPatrol;

    [SerializeField] private float delayToPatrolAgain;
    public float delayToPatrolAgainTimer;
    public Vector3 targetWalk;
    public float randomRangeForPatrol;
    private Vector3 previousPosition;

    void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        enemySpawnParticle.Play();
    }

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        enemyStatsScript = GetComponent<EnemyStats>();
        canMove = true;
        enemySpeed = enemyMaxSpeed;
        timeBetweenShotsTimer = timeBetweenShots + Random.Range(-randomExtraTimeBetweenShots, randomExtraTimeBetweenShots);
        navMesh.speed = enemyMaxSpeed;
        ableToPatrol = true;
        previousPosition = transform.position;
        delayToPatrolAgainTimer = delayToPatrolAgain;
    }

    // 1 = shooting + follow (o que fica de perto)
    // 2 = shooting + retreat (o que fica de longe)
    // 3 = follow + explode (o que explode)

    void Update()
    {
        if(Vector3.Distance(playerTransform.position, transform.position) < focusPlayerDistance && !isPlayerOnRange)
        {
            navMesh.ResetPath();
            isPlayerOnRange = true;
        }
    }

    void FixedUpdate()
    {
        if(!playerTransform.GetComponent<_PlayerManager>().isFading)
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
                    if(Vector3.Distance(playerTransform.position, transform.position) < shootPlayerDistance)
                    {
                        transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), Vector3.up);
                        Shoot();
                    }
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
                    float randomRangeXMinimum = Random.Range(-randomRangeForPatrol, -randomRangeForPatrol+2f);
                    float randomRangeXMaximum = Random.Range(randomRangeForPatrol-2f, randomRangeForPatrol);
                    float randomRangeZMinimum = Random.Range(-randomRangeForPatrol, -randomRangeForPatrol+2f);
                    float randomRangeZMaximum = Random.Range(randomRangeForPatrol-2f, randomRangeForPatrol);
                    float randomRangeX = Random.Range(randomRangeXMinimum, randomRangeXMaximum);
                    float randomRangeZ = Random.Range(randomRangeZMinimum, randomRangeZMaximum);
                    targetWalk = transform.position + new Vector3(randomRangeX, 0f, randomRangeZ);
                    transform.LookAt(targetWalk, transform.up);

                    navMesh.isStopped = false;
                    NavMeshMove(targetWalk);
                    previousPosition = transform.position;
                        
                    ableToPatrol = false;  
                }
                else
                {
                    delayToPatrolAgainTimer -= Time.fixedDeltaTime;
                    if(delayToPatrolAgainTimer <= 0)
                    {
                        navMesh.isStopped = true;
                        ableToPatrol = true;
                        delayToPatrolAgainTimer = delayToPatrolAgain;
                    } 
                }
            }
        }
        
    }

    public void NavMeshMove(Vector3 target)
    {
        navMesh.SetDestination(target);
        transform.LookAt(target, transform.up);
        navMesh.isStopped = false;
        ableToPatrol = false;
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
    public void ShootProjectile(int index)
    {
        enemyStatsScript.EnemyFlash();
        if(index == 0)
        {
            Instantiate(enemyBulletPrefab, firePointLeft.transform.position, firePointLeft.transform.rotation);
        }
        else if(index == 1)
        {
            Instantiate(enemyBulletPrefab, firePointMiddle.transform.position, firePointMiddle.transform.rotation);
        }
        else if(index == 2)
        {
            Instantiate(enemyBulletPrefab, firePointRight.transform.position, firePointRight.transform.rotation);
        }
        FindObjectOfType<SoundManager>().PlayOneShot("EnemyShot");
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

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Wall")
        {
            //navMesh.isStopped = true;
            navMesh.ResetPath();

            navMesh.isStopped = false;
            NavMeshMove(previousPosition);
            ableToPatrol = false;
        }
    }
}
