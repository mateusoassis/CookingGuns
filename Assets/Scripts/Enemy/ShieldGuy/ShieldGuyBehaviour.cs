using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGuyBehaviour : MonoBehaviour
{
    public int state;
    // 1 = parado + olhando para o player
    // 2 = andando + olhando para o player
    public float turnSpeed;
    public float maxTurnSpeed;
    public float moveSpeed;
    public float maxMoveSpeed;
    public float minDistance;
    public float guardTime;

    public Transform player;
    public Quaternion selfRotation;
    public Transform[] allies;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        moveSpeed = maxMoveSpeed;
        turnSpeed = maxTurnSpeed;
    }

    void Update()
    {
        CheckPlayerDistance();

        if(state == 1)
        {
            LookAtPlayer();
        }
        else if(state == 2)
        {
            WalkAndLookAtPlayer();
        }
    }

    public void LookAtPlayer()
    {
        selfRotation = Quaternion.LookRotation(player.position - transform.position, Vector3.up);

        selfRotation.x = 0f;
        selfRotation.z = 0f;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, selfRotation, turnSpeed);
    }
    public void WalkAndLookAtPlayer()
    {
        selfRotation = Quaternion.LookRotation(player.position - transform.position, Vector3.up);

        selfRotation.x = 0f;
        selfRotation.z = 0f;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, selfRotation, turnSpeed);

        Vector3 movePosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.position = Vector3.MoveTowards(transform.position, movePosition, moveSpeed * Time.deltaTime);
    }
    public void CheckPlayerDistance()
    {
        if(Vector3.Distance(transform.position, player.position) < minDistance)
        {
            state = 1;
        }
        else if(Vector3.Distance(transform.position, player.position) >= minDistance)
        {
            state = 2;
        }
    }
}
