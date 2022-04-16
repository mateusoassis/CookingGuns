using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGirl : MonoBehaviour
{
    public int state;
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
    public bool canMove;

    void Start()
    {
        selfRigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").GetComponent<Transform>();
        moveSpeed = maxMoveSpeed;
        turnSpeed = maxTurnSpeed;
        canMove = true;
    }

    void Update()
    {
        CheckPlayerDistance();
        if(state == 1 || state == 2)
        {
            LookAtPlayer();
        }
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
    public void WalkToPlayer()
    {
        Vector3 moveTowards = new Vector3(player.position.x, transform.position.y, player.position.z);
        selfRigidbody.MovePosition(transform.position + moveTowards * moveSpeed * Time.fixedDeltaTime);
    }

    public void CheckPlayerDistance()
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

    public void ShieldyWasHit()
    {
        Debug.Log("shieldy was hit");
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "PlayerBullet")
        {
            Debug.Log("parent was hit");
        }
    }
}
