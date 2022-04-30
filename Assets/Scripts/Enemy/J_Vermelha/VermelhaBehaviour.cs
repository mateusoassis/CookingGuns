using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VermelhaBehaviour : MonoBehaviour
{
    public Transform playerTransform;
    // 1 = shooting + follow (o que fica de perto)
    // 2 = shooting + retreat (o que fica de longe)
    // 3 = follow + explode (o que explode)
    public bool isPlayerOnRange;

    private float enemySpeed;
    public float enemyMaxSpeed;

    [Header("Behaviour 3")]
    public GameObject explosionObject;
    public Transform explosionObjectTransform;
    public MeshRenderer explosionObjectMesh;
    public float scaleExplosion;
    public float explosionRange;
    public float explosionTimerReset;
    private float explosionTimer;
    public float explosionSpeed;
    private Vector3 targetedVector;
    private Vector3 scaleVector;
    public bool explosionCollision;
    private EnemySpawner enemySpawner;
    private bool blinking;
    public int explosionDamage;

    private Animator enemyAnimator;

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        targetedVector = new Vector3(1f * scaleExplosion, 0.25f, 1f * scaleExplosion);
        scaleVector = new Vector3(1f, 0.25f, 1f);
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        blinking = false;
        enemyAnimator = GetComponent<Animator>();
        enemySpeed = enemyMaxSpeed;
    }

    // 1 = shooting + follow (o que fica de perto)
    // 2 = shooting + retreat (o que fica de longe)
    // 3 = follow + explode (o que explode)

    void FixedUpdate()
    {
        if(!playerTransform.GetComponent<_PlayerManager>().isFading)
        {
            if(!isPlayerOnRange)
            {
                if(Vector3.Distance(transform.position, playerTransform.position) > explosionRange)
                {
                    Vector3 movePosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, movePosition, enemySpeed * Time.fixedDeltaTime);
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
                    explosionTimer -= Time.fixedDeltaTime;
                    scaleVector = Vector3.MoveTowards(scaleVector, targetedVector, explosionSpeed * Time.fixedDeltaTime);
                    explosionObjectTransform.localScale = scaleVector;
                }
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
