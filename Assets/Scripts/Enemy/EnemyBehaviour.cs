using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform playerTransform;
    private Transform targetTransform;
    public int setBehaviour;
    public bool isPlayerOnRange;
    public Rigidbody enemyBulletPrefab;

    public float enemySpeed;
    public float stopDistance;
    public float retreatDistance;

    [Header("Explosão do behaviour 3")]
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

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        targetedVector = new Vector3(1f * scaleExplosion, 0.25f, 1f * scaleExplosion);
        scaleVector = new Vector3(1f, 0.25f, 1f);
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
                Debug.Log("nada");
            }
            else if(Vector3.Distance(transform.position, playerTransform.position) < retreatDistance)
            {
                Vector3 movePosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, movePosition, -enemySpeed * Time.deltaTime);
                transform.LookAt(playerTransform.position, Vector3.up);
            }
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
                Debug.Log("nada");
            }
            else if(Vector3.Distance(transform.position, playerTransform.position) < retreatDistance)
            {
                transform.LookAt(playerTransform.position, Vector3.up);
                Vector3 movePosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, movePosition, -enemySpeed * Time.deltaTime);
            }
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
        Destroy(this.gameObject);
    }
}
