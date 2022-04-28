using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    public float reloadTimeCounter;

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
    public TextMeshProUGUI ammoDisplay;
    public Slider reloadDisplay;
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
        ammoDisplay = GameObject.Find("AmmoDisplay").GetComponent<TextMeshProUGUI>();
        granadeLauncherTarget = GameObject.Find("SlerpTarget");
    }

    void Update()
    {
        if(reloading)
        {
            reloadTimeCounter += Time.deltaTime;
            reloadDisplay.value = reloadTimeCounter/reloadTime;
            if(reloadTimeCounter >= reloadTime)
            {
                reloading = false;
            }
        }
    }

    public void MyInput()
    {
        if(allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
            if(Input.GetKey(KeyCode.Mouse0)&& bulletsLeft > 0)
            {
                playerManager.playerMovement.PlayerAim();
            }
            
        }else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
            if(Input.GetKeyDown(KeyCode.Mouse0) && bulletsLeft > 0)
            {
                playerManager.playerMovement.PlayerAim();
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

            if(weaponHandler.weaponEquipped != 2)
            {
                //playerManager.animationHandler.GetWeaponInt();
                //playerManager.animationHandler.anim[playerManager.animationHandler.weapon].SetTrigger("Shoot");
                playerManager.playerMovement.PlayerAim();
                Shoot();
            }
            else
            {
                //playerManager.animationHandler.GetWeaponInt();
                //playerManager.animationHandler.anim[playerManager.animationHandler.weapon].SetTrigger("Shoot");
                Shoot();
                if(Input.GetKeyDown(KeyCode.Mouse0))
                {
                    playerManager.playerMovement.PlayerAim();
                }
            }
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

        Vector3 directionWithoutSpread = firePoint.forward;

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
        currentBullet.GetComponent<Rigidbody>().AddForce((firePoint.forward.normalized + directionWithSpread.normalized) * shootForce, ForceMode.Impulse);

        bulletsLeft--;
        bulletsShot++;

        if(allowInvoke)
        {
            playerManager.isShooting = true;
            StartCoroutine("ResetWalk");
            playerManager.animationHandler.GetWeaponInt();
            if(weaponHandler.weaponEquipped == 1)
            {
                playerManager.animationHandler.anim[playerManager.animationHandler.weapon].Play("ShootShotgun");
            }
            
            playerManager.animationHandler.anim[playerManager.animationHandler.weapon].SetTrigger("Shoot");
            playerManager.animationHandler.anim[playerManager.animationHandler.weapon].SetBool("Walking", false);

            Invoke("ResetShot", timeBetweenShooting);
        }

        if(bulletsShot < bulletPerTap && bulletsLeft > 0)
        {
            playerManager.isShooting = true;
            StartCoroutine("ResetWalk");
            playerManager.animationHandler.GetWeaponInt();
            
            if(weaponHandler.weaponEquipped == 1)
            {
                playerManager.animationHandler.anim[playerManager.animationHandler.weapon].Play("ShootShotgun");
            }
            playerManager.animationHandler.anim[playerManager.animationHandler.weapon].SetTrigger("Shoot");
            playerManager.animationHandler.anim[playerManager.animationHandler.weapon].SetBool("Walking", false);
    
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
        playerManager.isShooting = false;
    }

    private void Reload()
    {
        reloading = true;
        playerManager.ReloadDisplayUpdate();
        reloadDisplay.gameObject.SetActive(true);
        reloadTimeCounter = 0;
        //reloadTimeCounter += Time.deltaTime;
        //reloadDisplay.value = reloadTimeCounter/reloadTime;
        Invoke("ReloadFinished", reloadTime);
    }

    public IEnumerator ResetWalk()
    {
        yield return new WaitForSeconds(0.5f);
        playerManager.isShooting = false;
    }

    private void ReloadFinished()
    {
        reloadTimeCounter = 0;
        playerManager.ReloadEndDisplay();
        reloadDisplay.gameObject.SetActive(false);
        bulletsLeft = magazineSize;
        reloading = false;
    }

    public void AmmoDisplayUpdate()
    {
        if(ammoDisplay != null)
        {
            ammoDisplay.SetText(bulletsLeft/bulletPerTap + " / " + magazineSize/bulletPerTap);
        }
    }
}
