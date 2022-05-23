using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerMovement : MonoBehaviour
{
    public _PlayerManager playerManager;

    [Header("Player Stats")]
    public float playerMoveSpeed;
    public float playerMaxMoveSpeed;
    public Rigidbody playerRigidbody;

    [Header("Roll")] // eu poderia ter criado as funções nesse script e chamado todas elas no playermanager ao invés de mudar tantas variáveis daqui direto lá no playermanager
    public float multiplier; // pode ser pra algum buff de movespeed talvez
    public float rollSpeed;
    public float rollTimer = 0f;
    public float rollDuration;
    public int maxRoll;
    public int rollCount;
    private float rollCountTimer = 0f;
    public float rollCountDuration;

    private Vector3 _input;
    private Vector3 skewedInput;
    private Vector3 lastInput;
    private Vector3 skewedLastInput;
    
    [Header("Smoke Settings")]
    public ParticleSystem rollSmokePrefab;
    //public Transform rollSmokePoint;

    [Header("Player LookAt Mouse")]
    private Vector3 playerAimPosition;
    [SerializeField] private LayerMask playerAimLayerMask;
    private Quaternion newRotation;
    
    //private float _turnSpeed = 360;

    void Start() 
    {
        playerMoveSpeed = playerMaxMoveSpeed;
    }

    public void PlayerAim()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, float.MaxValue, playerAimLayerMask))
        {
            playerAimPosition = new Vector3(hit.point.x , 0f, hit.point.z); 

            if(playerManager.playerWeaponHandler.weaponTypeEquipped == 3)
            {
                playerManager.playerShootingGranadeLauncher.granadeLauncherTarget.transform.position = new Vector3 (playerAimPosition.x , 0.85f, playerAimPosition.z);
            }
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
            if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                lastInput = _input;
            }
        }

        // check de dash
        if(playerManager.isRolling)
        {
            if(rollTimer <= 0)
            {
                // controle de cancelar comer arma
                playerManager.rmbHeldDown = false;
                playerManager.canceledEating = false;

                playerManager.isRolling = false;
                gameObject.layer = 18;                
                playerManager.playerRigidbody.useGravity = true;
                //playerManager.playerCapsuleCollider.enabled = true;
                rollTimer = rollDuration;
                playerManager.animationHandler.GetWeaponInt();
                playerManager.animationHandler.anim[playerManager.animationHandler.weapon].SetBool("Rolling", false);
            }
            else
            { 
                rollTimer -= Time.deltaTime;
            }
        }

        if(playerManager.isEatingWeapon && !playerManager.canceledEating)
        {
            playerMoveSpeed = playerMaxMoveSpeed/2f;
        }
        else
        {
                playerMoveSpeed = playerMaxMoveSpeed;
        }
    }

    public void Move()
    {
        if(_input.x != 0 || _input.z != 0)
        {
            if(!playerManager.isShooting)
            {
                if(!playerManager.isRolling)
                {   
                    playerManager.animationHandler.GetWeaponInt();
                    if(!playerManager.playerShootingMachineGun.shooting)
                    {
                        playerManager.isWalking = true;
                        playerManager.animationHandler.anim[playerManager.animationHandler.weapon].SetBool("Walking", true);
                        
                        /*
                        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0,45,0));

                        skewedInput = matrix.MultiplyPoint3x4(_input);
                        skewedLastInput = matrix.MultiplyPoint3x4(lastInput);
                        */
                        skewedInput = _input;
                        skewedLastInput = lastInput;
                    
                        playerRigidbody.MovePosition(transform.position + (skewedLastInput.normalized) * playerMoveSpeed * Time.fixedDeltaTime);
                        transform.forward = skewedLastInput.normalized;
                    }
                }
                else
                {
                    playerManager.isWalking = false;
                    //var matrix = Matrix4x4.Rotate(Quaternion.Euler(0,45,0));
                    //skewedLastInput = matrix.MultiplyPoint3x4(lastInput);
                    skewedLastInput = lastInput;

                    playerRigidbody.MovePosition(transform.position + (skewedLastInput.normalized) * rollSpeed * Time.fixedDeltaTime);
                    //Instantiate(rollSmokePrefab, rollSmokePoint.position, Quaternion.identity);
                    PlayRollParticle();
                    playerManager.animationHandler.GetWeaponInt();
                    playerManager.animationHandler.anim[playerManager.animationHandler.weapon].SetBool("Walking", false);
                }  
            }
        }
        else if(_input.x == 0 && _input.z == 0 && !playerManager.isRolling)
        {
            playerManager.isWalking = false;
            playerManager.animationHandler.GetWeaponInt();
            playerManager.animationHandler.anim[playerManager.animationHandler.weapon].SetBool("Walking", false);
        }
        else
        {

            playerRigidbody.MovePosition(transform.position + (transform.forward.normalized) * rollSpeed * Time.fixedDeltaTime);
         
            PlayRollParticle();
            playerManager.animationHandler.GetWeaponInt();
            playerManager.animationHandler.anim[playerManager.animationHandler.weapon].SetBool("Walking", false);
        }
    }

    public void RollCountTimer()
    {
        if(rollCountTimer <= 0)
        {
            rollCount = 0;
            rollCountTimer = rollCountDuration;
        }
        else if(rollCount > 0)
        {
            rollCountTimer -= Time.deltaTime;
        }
    }

    void PlayRollParticle() 
    {
        rollSmokePrefab.Play();
    }


    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "EnemyShield")
        {
            Debug.Log("encostei no shield");
        }
    }
}
