using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerMovement : MonoBehaviour
{
    public Transform playerTransform;
    private _PlayerManager playerManager;
    [Header("Player Stats")]
    public float playerMoveSpeed;
    public Rigidbody playerRigidbody;

    [Header("Roll")]
    public float multiplier; // PROVAVELMENTE VAI MUDAR
    public float rollSpeed;
    public float rollTimer = 0f;
    public float rollDuration;
    public int maxRoll;
    public int rollCount;
    public float rollCountTimer = 0f;
    public float rollCountDuration;
    public Vector3 skewedInput;

    [Header("Player LookAt Mouse")]
    private Vector3 playerAimPosition;
    [SerializeField] private LayerMask playerAimLayerMask;
    private Quaternion newRotation;
    private Vector3 _input;
    //private float _turnSpeed = 360;

    void Start() 
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerManager = GetComponent<_PlayerManager>();
        playerTransform = GetComponent<Transform>();
    }

    public void PlayerAim()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, float.MaxValue, playerAimLayerMask))
        {
            playerAimPosition = new Vector3(hit.point.x , 0f, hit.point.z);
        }

        newRotation = Quaternion.LookRotation(playerAimPosition - transform.position, Vector3.up);

        newRotation.x = 0f;
        newRotation.z = 0f;

        transform.rotation = Quaternion.Slerp(newRotation, transform.rotation, Time.deltaTime);
    }

    public void HandleMovement()
    {
        // movement
        if(playerRigidbody.velocity.magnitude < playerMoveSpeed * multiplier && !playerManager.isRolling)
        {
            _input = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
        }

        // check de dash
        if(playerManager.isRolling)
        {
            if(rollTimer <= 0)
            {
                playerManager.isRolling = false;
                playerManager.playerCapsuleCollider.enabled = true;
                rollTimer = rollDuration;
            }
            else
            { 
                rollTimer -= Time.fixedDeltaTime;
            }
        }
    }

    public void Move()
    {
        if(!playerManager.isRolling)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0,45,0));

            skewedInput = matrix.MultiplyPoint3x4(_input);
        
            playerRigidbody.MovePosition(transform.position + (skewedInput.normalized) * playerMoveSpeed * Time.deltaTime);
        }
        else
        {
            playerRigidbody.MovePosition(transform.position + (skewedInput.normalized) * rollSpeed * Time.deltaTime);
        }  
    }

    public void RollCountTimer()
    {
        if(rollCountTimer <= 0)
        {
            rollCount = 0;
            rollCountTimer = rollCountDuration;
        }
        else
        {
            rollCountTimer -= Time.deltaTime;
        }
    }
}
