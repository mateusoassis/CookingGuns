using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChargeJujubaBehaviour : MonoBehaviour
{
    // 4 states
    // 1 = idle 
    // 2 = procurando player
    // 3 = preparando pra rolar
    // 4 = rolando
    // se fora do range, é idle
    // se dentro do range, inicia o preparamento pra rolar, que inclui salvar posição do jogador, pegar direção do roll e ir só naquela direção
    // no preparamento pra rolar, tem o trigger pra ativar animação
    // após acabar a duração do rolamento, para a animação de rolar e volta pro idle
    public bool isPlayerOnRange;
    public bool motherDamaged;
    public float randomRangeForPatrol;
    public int damage;
    public int state;
    public float timerToWalk;
    public float timeToWalk;
    public bool canWalk;
    public Vector3 targetWalk;
    public Vector3 thisTransformUp;

    public float minRangeToLockTarget;
    public Transform playerTransform;
    public bool lookingAtPlayer;

    public float cooldown;
    public float cooldownFromHittingPlayer;
    public float cooldownTimer;
    public float timeToStartCharging;
    public bool isCooldown;
    public float rollDuration;

    public ChargeJujubaAnimator chargeJujubaAnimator;
    public ParticleSystem trailParticle;
    private EnemyStats enemyStatsScript;

    public bool rolling;
    public float rollSpeed;
    public Vector3 rollDirection;

    public float backwardForce;

    public bool reset;
    public Transform parent;
    private NavMeshAgent navMesh;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        navMesh = GetComponent<NavMeshAgent>();
        enemyStatsScript = GetComponent<EnemyStats>();
    }

    void Start()
    {
        lookingAtPlayer = false;
        cooldownTimer = cooldown;
        canWalk = true;
        state = 1;
        parent = transform.parent;
        trailParticle.Stop();
    }

    void FixedUpdate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rolling && !playerTransform.GetComponent<_PlayerManager>().isFading)
        {
            rb.MovePosition(transform.position + rollDirection.normalized * rollSpeed * Time.deltaTime);
        }

    }
    void Update()
    {
        if(!playerTransform.GetComponent<_PlayerManager>().isFading)
        {
            HandleState();

            float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if(distanceToPlayer < minRangeToLockTarget || motherDamaged)
            {
                motherDamaged = true;
                state = 2;
                isPlayerOnRange = true;
            }

            if(isCooldown)
            {
                DoRollCooldown();
            } 
        }

        if(parent.GetComponent<TowerBehaviour>() != null)
        {
            if(parent.GetComponent<TowerBehaviour>().towerDamaged == true && !isPlayerOnRange && !motherDamaged)
            {
                state = 2;
                isPlayerOnRange = true;
                motherDamaged = true;
            }
        }        
    }

    public void HandleState()
    {
        
        if(state == 2)
        {
            if(!lookingAtPlayer && !rolling)
            {
                LockOnPlayer();
                navMesh.isStopped = true;                
            }
        }
        else if(state == 1)
        {
            if(canWalk && !isPlayerOnRange)
            {
                float randomRangeXMinimum = Random.Range(-randomRangeForPatrol, -randomRangeForPatrol+2f);
                float randomRangeXMaximum = Random.Range(randomRangeForPatrol-2f, randomRangeForPatrol);
                float randomRangeZMinimum = Random.Range(-randomRangeForPatrol, -randomRangeForPatrol+2f);
                float randomRangeZMaximum = Random.Range(randomRangeForPatrol-2f, randomRangeForPatrol);
                float randomRangeX = Random.Range(randomRangeXMinimum, randomRangeXMaximum);
                float randomRangeZ = Random.Range(randomRangeZMinimum, randomRangeZMaximum);
                //float randomRangeX = Random.Range(-2f, 2f);
                //float randomRangeZ = Random.Range(-2f, 2f);
                targetWalk = transform.position + new Vector3(randomRangeX, 0f, randomRangeZ);
                transform.LookAt(targetWalk, transform.up);
                canWalk = false;
                if(!isCooldown)
                {
                    StartCoroutine(WalkTowards(targetWalk));
                }
                
            }
            else if(canWalk && isPlayerOnRange)
            {
                targetWalk = playerTransform.position;
                navMesh.isStopped = false;
                transform.LookAt(targetWalk, transform.up);
                canWalk = false;
                if(!isCooldown)
                {
                    StartCoroutine(WalkTowards(targetWalk));
                }
                
            }
        }
    }

    public IEnumerator WalkTowards(Vector3 walkTarget)
    {
        var t = 0f;
        var start = transform.position;

        trailParticle.Play();
        while (t < timeToWalk)
        {
            t += Time.deltaTime;
            //transform.position = Vector3.Lerp(start, walkTarget, t);
            navMesh.SetDestination(walkTarget);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        canWalk = true;
    }

    public void DoRollCooldown()
    {
        cooldownTimer -= Time.deltaTime;
        if(cooldownTimer < 0)
        {
            cooldownTimer = cooldown;
            isCooldown = false;
        }
    }

    public void LockOnPlayer()
    {
        state = 3; // PREPARANDO PRA ROLAR
        if(!isCooldown)
        {
            Vector3 lookAtVector = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
            transform.LookAt(lookAtVector, transform.up);
            rollDirection = new Vector3(lookAtVector.x - transform.position.x, 0f, lookAtVector.z - transform.position.z);
            lookingAtPlayer = true;
            StartCoroutine(WaitAFewSeconds());
        } 
    }

    public IEnumerator WaitAFewSeconds()
    {
        enemyStatsScript.EnemyFlash();
        yield return new WaitForSeconds(timeToStartCharging);
        chargeJujubaAnimator.StartRoll();
        trailParticle.Play();
        yield return new WaitForSeconds(0.2f);
        FindObjectOfType<SoundManager>().PlayOneShot("JujubaVerdeAttack");
        state = 4;
        yield return new WaitForSeconds(rollDuration);
        chargeJujubaAnimator.StopRoll();
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Wall" && rolling)
        {
            FindObjectOfType<SoundManager>().StopSound("JujubaVerdeAttack");
            GetComponent<Rigidbody>().AddForce(-transform.forward.normalized * backwardForce, ForceMode.VelocityChange);
            Debug.Log(name + "toma knockback");
            StopAllCoroutines();
            chargeJujubaAnimator.StopRoll();
            canWalk = true;
            navMesh.isStopped = false;
        }
        else if(other.gameObject.tag == "Player" && rolling)
        {
            FindObjectOfType<SoundManager>().StopSound("JujubaVerdeAttack");
            GetComponent<Rigidbody>().AddForce(-transform.forward.normalized * backwardForce/2, ForceMode.VelocityChange);
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * backwardForce, ForceMode.Impulse);
            other.gameObject.GetComponent<_PlayerStats>().TakeHPDamage(damage);

            StopAllCoroutines();
            chargeJujubaAnimator.StopRoll();
            cooldownTimer = cooldownFromHittingPlayer;
            canWalk = true;
            navMesh.isStopped = false;
        }
        else if(other.gameObject.tag == "Wall" && !rolling && canWalk)
        {
            StopAllCoroutines();
            canWalk = true;
            navMesh.isStopped = false;
        }
        else if(other.gameObject.tag == "Barrel" && rolling)
        {
            FindObjectOfType<SoundManager>().StopSound("JujubaVerdeAttack");
            GetComponent<Rigidbody>().AddForce(-transform.forward.normalized * backwardForce/2, ForceMode.VelocityChange);
            //other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * backwardForce, ForceMode.Impulse);
            other.gameObject.GetComponent<BarrelScript>().ActivateBarrel();
            //other.gameObject.GetComponent<_PlayerStats>().TakeHPDamage(damage);

            StopAllCoroutines();
            chargeJujubaAnimator.StopRoll();
            cooldownTimer = cooldownFromHittingPlayer;
            canWalk = true;
            navMesh.isStopped = false;
        }

        else if(other.gameObject.tag == "Enemy" && rolling)
        {
            FindObjectOfType<SoundManager>().StopSound("JujubaVerdeAttack");
            GetComponent<Rigidbody>().AddForce(-transform.forward.normalized * backwardForce/2, ForceMode.VelocityChange);
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * backwardForce, ForceMode.Impulse);

            StopAllCoroutines();
            chargeJujubaAnimator.StopRoll();
            cooldownTimer = cooldownFromHittingPlayer;
            canWalk = true;
            navMesh.isStopped = false;
        }
    }

    public IEnumerator CanWalkAgain()
    {
        yield return new WaitForSeconds(1f);
        canWalk = true;
        reset = false;
    }
}
