using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AzulBehaviour : MonoBehaviour
{
    public Transform playerTransform;
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

    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        canMove = true;
        enemySpeed = enemyMaxSpeed;
        timeBetweenShotsTimer = timeBetweenShots + Random.Range(-randomExtraTimeBetweenShots, randomExtraTimeBetweenShots);
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
            if(!playerTransform.GetComponent<_PlayerManager>().isFading)
            {          
                // if(!isPlayerOnRange)
                    
                if(canMove)
                {
                    // retreat bem baixo pra o inimigo nunca parar de "recuar", e stop muito alto
                    if(Vector3.Distance(transform.position, playerTransform.position) >= stopDistance)
                    {
                        Vector3 movePosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                        transform.position = Vector3.MoveTowards(transform.position, movePosition, enemySpeed * Time.fixedDeltaTime);
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
                        transform.position = Vector3.MoveTowards(transform.position, movePosition, -enemySpeed * Time.fixedDeltaTime);
                        retreating = true;
                        retreatingOnCooldown = true;
                    }
                }

                if(retreatingOnCooldown)
                {
                    retreatCooldownTimer += Time.fixedDeltaTime;
                    if(retreatCooldownTimer >= retreatCooldown)
                    {
                        canMove = false;
                    }

                    if(!canMove)
                    {
                        countToMoveTimer += Time.fixedDeltaTime;
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
        }
        
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
}
