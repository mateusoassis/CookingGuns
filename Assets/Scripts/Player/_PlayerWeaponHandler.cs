using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerWeaponHandler : MonoBehaviour
{
    private _PlayerManager playerManager;
    [Header("Player Weapons")]
    [SerializeField] private GameObject pistol;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletShootDelay;
    [SerializeField] private Rigidbody pistolBulletPrefab;
    [SerializeField] private Transform bulletSpawnerTransform;
    public int weaponActive;
    public bool canShoot;
    public float shootCooldown;
    [SerializeField] private _PlayerWeaponHandler weaponHandler;

    void Start() 
    {
        playerManager = GetComponent<_PlayerManager>();
        weaponHandler = GameObject.Find("WeaponIcons").GetComponent<_PlayerWeaponHandler>();
    }

    // 1 = pistola
    // 2 = shotgun
    // 3 = metralhadora
    public void WeaponHandler()
    {
        /*
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(weaponHandler.pistolUnlocked)
            {
                ActivatePistol(); 
            }
        }
        */
    }

    /*public void ActivateIronBar()
    {
        ironBar.SetActive(true);
        ironAxe.SetActive(false);
        pistol.SetActive(false);
        if(ironBar.activeSelf){
            Debug.Log("iron bar equipada");
        }
        weaponActive = 1;
    }*/
    /*public void ActivateIronAxe()
    {
        ironBar.SetActive(false);
        ironAxe.SetActive(true);
        pistol.SetActive(false);
        if(ironAxe.activeSelf){
            Debug.Log("iron axe equipado");
        }
        weaponActive = 2;
    }*/
    public void ActivatePistol()
    {   
        /*ironBar.SetActive(false);
        ironAxe.SetActive(false);
        */
        pistol.SetActive(true);
        if(pistol.activeSelf){
            Debug.Log("pistol equipada");
        }  
        weaponActive = 1;
        canShoot = true;
    }

    public void WeaponBehaviour()
    {
        /*if(weaponActive == 1)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(canShoot)
                {
                    IronBarAttack();
                }
            }
        }
        else if(weaponActive == 2)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(canAttack)
                {
                    IronAxeAttack();
                }
            }
        }
        else*/
        if(weaponActive == 1)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(canShoot)
                {
                    PistolShoot();
                }
            }
        }
    }

    public void PistolShoot()
    {
        //PlayerAim();
        Rigidbody rb = Instantiate(pistolBulletPrefab, bulletSpawnerTransform.position, bulletSpawnerTransform.rotation);
        rb.velocity = (bulletSpawnerTransform.forward).normalized * bulletSpeed;
        StartCoroutine(ResetAttackCooldown(bulletShootDelay));
    }
    IEnumerator ResetAttackCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
        playerManager.isShooting = false;
    }
}
