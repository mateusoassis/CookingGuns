using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerMovement : MonoBehaviour
{
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
    private float _turnSpeed = 360;

    void Start() 
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerManager = GetComponent<_PlayerManager>();
    }

    public void PlayerAim()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, float.MaxValue, playerAimLayerMask))
        {
            playerAimPosition = new Vector3(hit.point.x, 0f, hit.point.z);
        }

        newRotation = Quaternion.LookRotation(playerAimPosition - transform.position, Vector3.up);

        newRotation.x = 0f;
        newRotation.z = 0f;

        transform.rotation = Quaternion.Slerp(newRotation, transform.rotation, Time.deltaTime * 30);
    }

    public void HandleMovement()
    {
        // movement
        if(playerRigidbody.velocity.magnitude < playerMoveSpeed * multiplier && !playerManager.isRolling)
        {
            _input = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));

            /*vertical = Input.GetAxisRaw("Vertical");
            if(vertical != 0)
            {
                playerRigidbody.MovePosition(0f, 0f, vertical * Time.fixedDeltaTime * 100f * playerMoveSpeed);
            }

            horizontal = Input.GetAxisRaw("Horizontal");
            if(horizontal != 0)
            {
                playerRigidbody.MovePosition(horizontal * Time.fixedDeltaTime * 100f * playerMoveSpeed, 0f, 0f);
            }*/
        }

        // check de dash
        if(playerManager.isRolling)
        {
            if(rollTimer <= 0)
            {
                playerManager.isRolling = false;
                rollTimer = rollDuration;
                //playerRigidbody.velocity = Vector3.zero;
            }
            else
            {
                //playerRigidbody.velocity = direction * dashSpeed;
                
                
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
        
            playerRigidbody.MovePosition(transform.position + (skewedInput.normalized * _input.magnitude) * playerMoveSpeed * Time.deltaTime);
        }
        else
        {
            playerRigidbody.MovePosition(transform.position + (skewedInput.normalized * _input.magnitude) * rollSpeed * Time.deltaTime);
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
