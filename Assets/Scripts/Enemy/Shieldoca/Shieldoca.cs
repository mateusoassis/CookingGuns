using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shieldoca : MonoBehaviour
{
    public int state;
    // 0 = parado sem olhar pra nada
    // 1 = parado + olhando para o player
    // 2 = andando + olhando para o player

    public Rigidbody selfRigidbody;
    public Quaternion selfRotation;

    public float turnSpeed;
    public float maxTurnSpeed;

    public float moveSpeed;
    public float maxMoveSpeed;

    public float minDistance;

    public Transform player;
    public bool sitStill;

    public Animator anim;
    public ParticleSystem attackParticle;

    private EnemyStats enemyStatsScript;

    public bool isPlayerInsideArea;
    public int hitDamage;
    public bool canAttack;
    public bool playerOnRange;
    public float attackTimer;
    public float attackCooldown;
    public bool attacking;

    void Start()
    {
        attackParticle = transform.Find("ShieldocaAttack").GetComponent<ParticleSystem>();
        selfRigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        enemyStatsScript = GetComponent<EnemyStats>();
        moveSpeed = maxMoveSpeed;
        turnSpeed = maxTurnSpeed;
    }

    void Update()
    {
        CheckPlayerDistance();
        if(state == 1 || state == 2)
        {
            if(!attacking)
            {
                LookAtPlayer();
            }
            
        }

        HandleAttack();
    }

    void FixedUpdate()
    {
        if(state == 2)
        {
            WalkToPlayer();
        }
    }

    public void LookAtPlayer()
    {
        selfRotation = Quaternion.LookRotation(player.position - transform.position, transform.up);

        selfRotation.x = 0f;
        selfRotation.z = 0f;

        //selfRigidbody.MoveRotation(selfRotation);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, selfRotation, turnSpeed);
    }

    public void HandleAttack()
    {
        if(!canAttack && !attacking)
        {
            attackTimer += Time.deltaTime;
        }
        
        if(attackTimer > attackCooldown)
        {
            canAttack = true;
        }

        if(canAttack && playerOnRange)
        {
            anim.SetTrigger("Attack");
            enemyStatsScript.EnemyFlash();
            attackTimer = 0f;
            canAttack = false;
        }
    }

    public void DamageIfPlayerInside()
    {
        if(isPlayerInsideArea && !player.GetComponent<_PlayerManager>().isRolling)
        {
            player.gameObject.GetComponent<_PlayerStats>().TakeHPDamage(hitDamage);
        }
    }

    public void WalkToPlayer()
    {
        Vector3 moveTowards = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.position = Vector3.MoveTowards(transform.position, moveTowards, moveSpeed * Time.fixedDeltaTime);
    }

    public void CheckPlayerDistance()
    {
        if(!sitStill)
        {
            if(Vector3.Distance(transform.position, player.position) < minDistance)
            {
                state = 1;
            }
            else
            {
                state = 2;
            }
        }
        else
        {
            state = 0;
        } 
    }

    public void ShieldyWasHit()
    {
        Debug.Log("shieldy was hit");
        FindObjectOfType<SoundManager>().PlayOneShot("ShieldocaAbsorb");
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("parent was hit");
        }
    }

    public void AttackingTrue()
    {
        attacking = true;
    }
    public void AttackingFalse()
    {
        attacking = false;
        FindObjectOfType<SoundManager>().PlayOneShot("ShieldocaKaboom");
    }

    public void ActivateAttackParticle() 
    {
        attackParticle.Play();
    }
}
