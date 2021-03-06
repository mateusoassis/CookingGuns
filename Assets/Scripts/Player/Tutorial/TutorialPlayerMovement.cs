using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPlayerMovement : MonoBehaviour
{
    public Transform playerTransform;
    private T_PlayerManager tutorialPlayerManager;
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
    public Vector3 lastInput;

    void Start() 
    {
        playerRigidbody = GetComponent<Rigidbody>();
        tutorialPlayerManager = GetComponent<T_PlayerManager>();
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
        if(playerRigidbody.velocity.magnitude < playerMoveSpeed * multiplier && !tutorialPlayerManager.isRolling)
        {
            _input = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
            if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                lastInput = _input;
            }
        }

        // check de dash
        if(tutorialPlayerManager.isRolling)
        {
            if(rollTimer <= 0)
            {
                tutorialPlayerManager.isRolling = false;
                rollTimer = rollDuration;
                playerRigidbody.useGravity = true;
            }
            else
            {            
                rollTimer -= Time.fixedDeltaTime;
            }
        }
    }

    public void Move()
    {
        if(!tutorialPlayerManager.isRolling)
        {
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0,45,0));

            skewedInput = matrix.MultiplyPoint3x4(_input);
            
            playerRigidbody.MovePosition(transform.position + (skewedInput.normalized * playerMoveSpeed * Time.deltaTime));
        }
        else
        {
            playerRigidbody.MovePosition(transform.position + (skewedInput.normalized * rollSpeed * Time.deltaTime));
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

    /*
    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            tutorialPlayerManager.isGrounded = false;
            playerRigidbody.useGravity = false;
        }
    }

    public void OnCollisionExit(Collision other)
    {
        if(other.gameObject.tag == "Ground" && !tutorialPlayerManager.isRolling)
        {
            tutorialPlayerManager.isGrounded = true;
            playerRigidbody.useGravity = true;
        }
    }
    */
}

