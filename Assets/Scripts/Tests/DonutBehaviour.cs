using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutBehaviour : MonoBehaviour
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
    public float cooldownTimer;
    public float timeToStartRolling;
    public bool isCooldown;
    public float rollDuration;

    public DonutAnimation donutAnimation;

    public bool rolling;
    public float rollSpeed;
    public Vector3 rollDirection;

    public bool reset;

    void Start()
    {
        lookingAtPlayer = false;
        cooldownTimer = cooldown;
        canWalk = true;
        state = 1;
    }

    void Update()
    {
        HandleState();

        if(rolling)
        {
            transform.position += rollDirection.normalized * rollSpeed * Time.deltaTime;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if(distanceToPlayer < minRangeToLockTarget)
        {
            state = 2;
        }
        
        /*
        if(!canWalk && !reset && !lookingAtPlayer && !isCooldown && !rolling && !reset)
        {
            StartCoroutine(CanWalkAgain());
        }
        */

        if(isCooldown)
        {
            DoRollCooldown();
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
        //lookingAtPlayer = false;
        yield return new WaitForSeconds(timeToStartRolling);
        donutAnimation.StartRoll();
        yield return new WaitForSeconds(0.2f);
        state = 4;
        yield return new WaitForSeconds(rollDuration);
        donutAnimation.StopRoll();
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Wall" && rolling)
        {
            GetComponent<Rigidbody>().AddForce(-transform.forward.normalized * 80f, ForceMode.VelocityChange);
            Debug.Log("parede caralho");
            StopAllCoroutines();
            donutAnimation.StopRoll();
            //canWalk = true;
            canWalk = true;
        }
        else if(other.gameObject.tag == "Wall" && !rolling)
        {
            //GetComponent<Rigidbody>().AddForce(-transform.forward * rollSpeed, ForceMode.Impulse);
            //GetComponent<Rigidbody>().AddForce(-rollDirection * rollSpeed/10f, ForceMode.Impulse);
            Debug.Log("encostou normal na parede");
        }

        if(other.gameObject.tag == "Player" && rolling)
        {
            Debug.Log("player porra");
            StopAllCoroutines();
            donutAnimation.StopRoll();
            //other.GetComponent<Rigidbody>().AddForce(new Vector3(transform.position.x, other.transform.position.y, transform.position.z) * rollSpeed, ForceMode.Impulse);
            //canWalk = true;
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
