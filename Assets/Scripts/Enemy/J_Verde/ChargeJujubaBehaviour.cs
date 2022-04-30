using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public bool rolling;
    public float rollSpeed;
    public Vector3 rollDirection;

    public float backwardForce;

    public bool reset;

    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Start()
    {
        lookingAtPlayer = false;
        cooldownTimer = cooldown;
        canWalk = true;
        state = 1;
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
            if(distanceToPlayer < minRangeToLockTarget)
            {
                state = 2;
            }

            if(isCooldown)
            {
                DoRollCooldown();
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
            }
        }
        else if(state == 1)
        {
            if(canWalk)
            {
                float randomRangeX = Random.Range(-2f, 2f);
                float randomRangeZ = Random.Range(-2f, 2f);
                targetWalk = transform.position + new Vector3(randomRangeX, 0f, randomRangeZ);
                transform.LookAt(targetWalk, transform.up);
                if(!isCooldown)
                {
                    StartCoroutine(WalkTowards(targetWalk));
                }
                canWalk = false;
            }
        }
    }

    public IEnumerator WalkTowards(Vector3 walkTarget)
    {
        var t = 0f;
        var start = transform.position;

        while (t < timeToWalk)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(start, walkTarget, t);
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
        yield return new WaitForSeconds(timeToStartCharging);
        chargeJujubaAnimator.StartRoll();
        yield return new WaitForSeconds(0.2f);
        state = 4;
        yield return new WaitForSeconds(rollDuration);
        chargeJujubaAnimator.StopRoll();
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Wall" && rolling)
        {
            GetComponent<Rigidbody>().AddForce(-transform.forward.normalized * backwardForce, ForceMode.VelocityChange);
            Debug.Log(name + "toma knockback");
            StopAllCoroutines();
            chargeJujubaAnimator.StopRoll();
            canWalk = true;
        }
        else if(other.gameObject.tag == "Player" && rolling)
        {
            GetComponent<Rigidbody>().AddForce(-transform.forward.normalized * backwardForce/2, ForceMode.VelocityChange);
            other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward.normalized * backwardForce, ForceMode.Impulse);

            other.gameObject.GetComponent<_PlayerStats>().TakeHPDamage(damage);

            StopAllCoroutines();
            chargeJujubaAnimator.StopRoll();
            cooldownTimer = cooldownFromHittingPlayer;
            canWalk = true;
        }
    }

    public IEnumerator CanWalkAgain()
    {
        yield return new WaitForSeconds(1f);
        canWalk = true;
        reset = false;
    }
}
