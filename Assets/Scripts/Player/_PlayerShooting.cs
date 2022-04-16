using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerShooting : MonoBehaviour
{
    public GameObject bullet;

    public GameObject granadeLauncherTarget;

    //public GameObject granade;
    //bullet force
    public float shootForce, upwardForce;
    //gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletPerTap;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading; 

    [HideInInspector]
    public Vector3 directionWithSpread;

    //reference
    public Camera isometricCam;
    public Transform firePoint;
    //Graphics
    public GameObject muzzleFlash;
    //bug fix
    public bool allowInvoke = true;

    public _PlayerManager playerManager;
    public _WeaponHandler weaponHandler;

    private void Awake()
    {   
        //make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
        playerManager = GetComponentInParent<_PlayerManager>();
        weaponHandler = GetComponentInParent<_WeaponHandler>();
        granadeLauncherTarget = GameObject.Find("SlerpTarget");
    }

    public void MyInput()
    {
        if(allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                playerManager.playerMovement.PlayerAim();
            }
            
        }else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                playerManager.playerMovement.PlayerAim();

                /*if(weaponHandler.weaponEquipped == 3)
                {
                    granadeLauncherTarget.transform.position = playerManager.playerMovement.playerAimPosition;
                    Debug.Log("troca porra");
                }*/
            }     
        }

        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        if(readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            Reload();
        }

        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;

            /*
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                playerManager.playerMovement.PlayerAim();
            }
            */
            if(weaponHandler.weaponEquipped != 2)
            {
                Shoot();
            }
            else
            {
                Shoot();
                if(Input.GetKey(KeyCode.Mouse0))
                {
                    playerManager.playerMovement.PlayerAim();
                    /*
                    Vector3 lookTowards = playerManager.GetComponent<Transform>().transform.forward;
                    lookTowards.x = 0f;
                    lookTowards.z = 0f;
            
                    playerManager.playerMovement.lastInput = lookTowards.normalized;
                    */
                }
            }
            
            

            //Granade();
        }
    }

    public void Shoot()
    {
        readyToShoot = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        Vector3 targetPoint;

        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }else
        {
            targetPoint = ray.GetPoint(75);
        }

        Vector3 directionWithoutSpread = targetPoint - firePoint.position;

        float x = Random.Range(-spread, spread);
        float z = Random.Range(-spread, spread);

        directionWithSpread = directionWithoutSpread + new Vector3(x, 0, z);

        GameObject currentBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
        
        if(weaponHandler.weaponEquipped == 3)
        {
            currentBullet.transform.SetParent(granadeLauncherTarget.transform);
        }

        currentBullet.transform.forward = directionWithSpread.normalized;

        Vector3 forwardShooting = new Vector3(transform.forward.x, 0f, transform.forward.z) + new Vector3(x, 0, z);
        currentBullet.GetComponent<Rigidbody>().AddForce(/*directionWithSpread.normalized*/forwardShooting.normalized * shootForce, ForceMode.Impulse);
        //currentBullet.GetComponent<RigidBody>().AddForce(isometricCam.transform.up * upwardForce, ForceMode.Impulse);

        bulletsLeft--;
        bulletsShot++;

        if(allowInvoke)
        {
            playerManager.isShooting = true;

            Invoke("ResetShot", timeBetweenShooting);
        }

        if(bulletsShot < bulletPerTap && bulletsLeft > 0)
        {
            playerManager.isShooting = true;

            Invoke("Shoot", timeBetweenShots);
        }
    }

    /*public void Granade()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        Vector3 granadePoint;

        if(Physics.Raycast(ray, out hit))
        {
            granadePoint = hit.point;
        }else
        {
            granadePoint = ray.GetPoint(75);
        }

        GameObject currentGranade = Instantiate(granade, firePoint.position, Quaternion.identity);
        currentGranade.transform.position = Vector3.MoveTowards(currentGranade.transform.position, granadePoint, 100* Time.deltaTime);
    }*/

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
        playerManager.isShooting = false;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
