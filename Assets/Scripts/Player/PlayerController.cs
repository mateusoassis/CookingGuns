using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private float multiplier;
    private float horizontal;
    private float vertical;
    public Rigidbody playerRigidbody;

    [Header("Dash")]
    [SerializeField] private float dashSpeed;
    private float dashTimer = 0f;
    [SerializeField] private float dashDuration;
    private Vector3 direction;
    [SerializeField] private bool isDashing;
    [SerializeField] private int maxDash;
    private int dashCount;
    private float dashCountTimer = 0f;
    [SerializeField] private float dashCountDuration;

    [Header("Player LookAt Mouse")]
    private Vector3 playerAimPosition;
    [SerializeField] private LayerMask playerAimLayerMask;

    [Header("Player Weapons")]
    [SerializeField] private GameObject ironBar;
    [SerializeField] private Animator ironBarAnim;
    [SerializeField] private GameObject ironAxe;
    [SerializeField] private GameObject pistol;
    [SerializeField] private Rigidbody pistolBulletPrefab;
    private int weaponActive;
    public bool canAttack;
    public float attackCooldown;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        canAttack = true;
        ActivateIronBar();
    }

    void Update()
    {
        // input responsivo de dash
        if(Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            if(dashCount < maxDash)
            {
                isDashing = true;
                dashTimer = dashDuration;
                direction = new Vector3(horizontal, 0f, vertical).normalized;
                dashCount++;
            }
        }

        // timer do dash
        DashCountTimer();

        // mouse + raycast player.lookat
        PlayerAim();
        WeaponHandler();
        WeaponBehaviour();
    }

    //
    //
    // MIRA DO JOGADOR
    //
    //
    public void PlayerAim()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, float.MaxValue, playerAimLayerMask))
        {
            playerAimPosition = new Vector3(hit.point.x, 0f, hit.point.z);
        }

        Quaternion newRotation = Quaternion.LookRotation(playerAimPosition - transform.position, Vector3.up);

        newRotation.x = 0f;
        newRotation.z = 0f;

        transform.rotation = Quaternion.Slerp(newRotation, transform.rotation, Time.deltaTime * 30);
    }

    //
    //
    // ARMAS
    //
    //

    // 1 = iron bar
    // 2 = iron axe
    // 3 = pistol
    public void WeaponHandler()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            ActivateIronBar(); 
        }
        if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            ActivateIronAxe();
        }
        if(Input.GetKeyDown(KeyCode.Keypad3))
        {
            ActivatePistol(); 
        }
    }

    public void ActivateIronBar()
    {
        ironBar.SetActive(true);
        ironAxe.SetActive(false);
        pistol.SetActive(false);
        if(ironBar.activeSelf){
            Debug.Log("iron bar equipada");
        }
        weaponActive = 1;
    }
    public void ActivateIronAxe()
    {
        ironBar.SetActive(false);
        ironAxe.SetActive(true);
        pistol.SetActive(false);
        if(ironAxe.activeSelf){
            Debug.Log("iron axe equipado");
        }
        weaponActive = 2;
    }
    public void ActivatePistol()
    {
        ironBar.SetActive(false);
        ironAxe.SetActive(false);
        pistol.SetActive(true);
        if(pistol.activeSelf){
            Debug.Log("pistol equipada");
        }  
        weaponActive = 3;
    }

    public void WeaponBehaviour()
    {
        if(weaponActive == 1)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(canAttack)
                {
                    IronBarAttack();
                }
            }
        }
        else if(weaponActive == 2)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                
            }
        }
        else if(weaponActive == 3)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                
            }
        }
    }

    public void IronBarAttack()
    {
        canAttack = false;
        ironBarAnim.SetTrigger("Attack");
        StartCoroutine(ResetAttackCooldown());
    }
    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void FixedUpdate()
    {
        HandleMovement();   
    }

    //
    //
    // MOVIMENTAÇÃO E DASH
    //
    //
    public void HandleMovement()
    {
        // movement
        if(playerRigidbody.velocity.magnitude < playerMoveSpeed * multiplier && !isDashing)
        {
            vertical = Input.GetAxisRaw("Vertical");
            if(vertical != 0)
            {
                playerRigidbody.AddForce(0f, 0f, vertical * Time.fixedDeltaTime * 100f * playerMoveSpeed);
            }

            horizontal = Input.GetAxisRaw("Horizontal");
            if(horizontal != 0)
            {
                playerRigidbody.AddForce(horizontal * Time.fixedDeltaTime * 100f * playerMoveSpeed, 0f, 0f);
            }
        }

        // check de dash
        if(isDashing)
        {
            if(dashTimer <= 0)
            {
                isDashing = false;
                dashTimer = dashDuration;
                playerRigidbody.velocity = Vector3.zero;
            }
            else
            {
                playerRigidbody.velocity = direction * dashSpeed;
                
                dashTimer -= Time.fixedDeltaTime;
            }
        }
    }

    public void DashCountTimer()
    {
        if(dashCountTimer <= 0)
        {
            dashCount = 0;
            dashCountTimer = dashCountDuration;
        }
        else
        {
            dashCountTimer -= Time.deltaTime;
        }
    }
}
