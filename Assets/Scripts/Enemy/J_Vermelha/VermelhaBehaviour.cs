using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VermelhaBehaviour : MonoBehaviour
{
    public Transform playerTransform;
    // 1 = shooting + follow (o que fica de perto)
    // 2 = shooting + retreat (o que fica de longe)
    // 3 = follow + explode (o que explode)
    public bool isPlayerOnRange;
    public float focusPlayerDistance;
    public float stoppingDistance;

    private float enemySpeed;
    [SerializeField] private float speedMultiplier;
    public float enemyMaxSpeed;

    [Header("Behaviour 3")]
    public GameObject explosionObject;
    public Transform explosionObjectTransform;
    public MeshRenderer explosionObjectMesh;
    public float sizeOfExplosionScale;
    public float startExplosionRange;
    public float explosionTime;
    public float explosionTimer;
    public float explosionSpeed;
    private Vector3 targetedVector;
    private Vector3 scaleVector;
    public bool explosionCollision;
    private EnemySpawner enemySpawner;
    private bool blinking;
    public int explosionDamage;

    private Animator enemyAnimator;
    private float speedMultiplied;

    [Header("NavMeshAgent")]
    private NavMeshAgent navMesh;

    public bool ableToPatrol;

    public bool exploding;

    [SerializeField] private float delayToPatrolAgain;
    public float delayToPatrolAgainTimer;
    public Vector3 targetWalk;
    public float randomRangeForPatrol;
    private Vector3 previousPosition;

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        targetedVector = new Vector3(1f * sizeOfExplosionScale, 0.25f, 1f * sizeOfExplosionScale);
        scaleVector = new Vector3(1f, 0.25f, 1f);
        //enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        blinking = false;
        enemyAnimator = GetComponent<Animator>();
        enemySpeed = enemyMaxSpeed;
        speedMultiplied = enemyMaxSpeed * speedMultiplier;
        navMesh = GetComponent<NavMeshAgent>();
        NormalMovespeed();
        explosionTimer = explosionTime;
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
        else if(Vector3.Distance(transform.position, playerTransform.position) < stoppingDistance && exploding)
        {
            Debug.Log("zera speed");
            ZeroMovespeed();
        }
        else if(Vector3.Distance(transform.position, playerTransform.position) >= stoppingDistance && !exploding)
        {
            Debug.Log("normal speed");
            NormalMovespeed();
        }
        else if(Vector3.Distance(transform.position, playerTransform.position) >= stoppingDistance && exploding)
        {
            Debug.Log("multiplied speed");
            MultipliedNormalMoveSpeed();
        }
    }

    void FixedUpdate()
    {
        if(!playerTransform.GetComponent<_PlayerManager>().isFading)
        {
            if(!isPlayerOnRange) // range mudou, agora é pra detectar player dentro
            {
                /*
                if(Vector3.Distance(transform.position, playerTransform.position) > startExplosionRange)
                {
                    Vector3 movePosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, movePosition, enemySpeed * Time.fixedDeltaTime);
                    transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), Vector3.up);
                }
                else if(Vector3.Distance(transform.position, playerTransform.position) <= startExplosionRange)
                {
                    StartExplosion();
                }
                */
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
            else if(isPlayerOnRange)
            {
                Vector3 directionToPlayer = playerTransform.position - transform.position;
                Vector3 newPos = transform.position + directionToPlayer.normalized;

                navMesh.isStopped = false;

                navMesh.SetDestination(newPos);
                transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), Vector3.up);
                
                if(Vector3.Distance(transform.position, playerTransform.position) <= startExplosionRange && !exploding)
                {
                    StartExplosion();
                }

                if(explosionTimer <= 0 && exploding)
                {
                    Explode();
                }
                else if(explosionTimer > 0f && exploding)
                {
                    explosionTimer -= Time.fixedDeltaTime;
                    scaleVector = Vector3.MoveTowards(scaleVector, targetedVector, explosionSpeed * Time.fixedDeltaTime);
                    explosionObjectTransform.localScale = scaleVector;
                    /*
                    Vector3 movePosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, movePosition, speedMultiplied * Time.fixedDeltaTime);
                    transform.LookAt(new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z), Vector3.up);
                    */
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

    /*
    public IEnumerator CountToStopMoving()
    {
        yield return new WaitForSeconds(countToMove);
        canMove = false;
        yield return new WaitForSeconds(retreatCooldown);
        canMove = true;
    }
    */

    public void StartExplosion()
    {
        explosionTimer = explosionTime;
        exploding = true;
        MultipliedNormalMoveSpeed();
        enemyAnimator.SetTrigger("Explode");
        StartCoroutine("BlinkExplosionRange");
        //isPlayerOnRange = true;
        explosionObjectMesh.enabled = true;
        //explosionTimer = explosionTime;
    }

    public void Explode()
    {
        if(explosionCollision)
        {
            Debug.Log("player toma dano");
        }
        if(GetComponent<MinusOnDestroy>() == null)
        {
            Debug.Log("Morreu kk");
        }
        Debug.Log("explodiu");

        if(explosionCollision)
        {
            if(playerTransform != null)
            {
                playerTransform.GetComponent<_PlayerStats>().TakeHPDamage(explosionDamage);
            }
        }

        Destroy(this.gameObject);
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
        navMesh.speed = 0f;
    }
    public void NormalMovespeed()
    {
        navMesh.speed = enemyMaxSpeed;
    }

    public void MultipliedNormalMoveSpeed()
    {
        navMesh.speed = speedMultiplied;
    }

    void OnDestroy()
    {
        explosionTimer = explosionTime;
    }
}
