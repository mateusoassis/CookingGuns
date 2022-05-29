using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class _PlayerShooting : MonoBehaviour
{
    public string[] randomNames; // à implementar no futuro, confia
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

    public int bulletsLeft, bulletsShot;

    //bools
    public bool shooting, readyToShoot, reloading; 

    [HideInInspector]
    public Vector3 directionWithSpread;

    //reference
    public Camera isometricCam;
    public Transform firePoint;
    //Graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammoDisplay;
    public TextMeshProUGUI weaponNameDisplay;
    public Slider reloadDisplay;
    //bug fix
    public bool allowInvoke = true;

    public _PlayerManager playerManager;
    public _WeaponHandler weaponHandler;

    public int sceneInt;

    public CursorManager cursorManager;
    private bool emptyWeapon;
    private bool reloadGun;

    private void Awake()
    {   
        sceneInt = SceneManager.GetActiveScene().buildIndex;
        //make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
        playerManager = GetComponentInParent<_PlayerManager>();
        weaponHandler = GetComponentInParent<_WeaponHandler>();
        if(sceneInt != 0 && sceneInt != 1)
        {
            ammoDisplay = GameObject.Find("AmmoDisplay").GetComponent<TextMeshProUGUI>();
            weaponNameDisplay = GameObject.Find("WeaponNameDisplay").GetComponent<TextMeshProUGUI>();
        }
        granadeLauncherTarget = GameObject.Find("SlerpTarget");
        cursorManager = GameObject.Find("CursorManager").GetComponent<CursorManager>();
    }

    private void Start()
    {

    }

    void Update()
    {
        if(playerManager.isRolling)
        {
            ReloadInterrupted();
        }
        
        if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q)))
        {
            Debug.Log("interrompe");
            if(_WeaponHandler.CountBool(weaponHandler.freeSlotArray, true) < 2)
            {
                Debug.Log("aqui");
                ReloadInterrupted();
            }
            Debug.Log("foi");
        }

        if(reloading)
        {
            reloadTimeCounter += Time.deltaTime;
            reloadDisplay.value = reloadTimeCounter/reloadTime;
            if(reloadTimeCounter >= reloadTime)
            {
                reloading = false;
            }
        }
        AmmoDisplayUpdate();
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
            cursorManager.ReloadCrosshairAnimation(reloadTime);
        }

        if(readyToShoot && !reloading && bulletsLeft <= 0)
        {
            if(playerManager.playerWeaponHandler.weaponTypeEquipped != 3)
            {
                if(!emptyWeapon)
                {
                    FindObjectOfType<SoundManager>().PlayOneShot("EmptyGunPistolAndMachineGun");
                    emptyWeapon = true;
                }
            }
            Reload();
            cursorManager.ReloadCrosshairAnimation(reloadTime);
        }

        if(readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;

            if(weaponHandler.weaponTypeEquipped != 2)
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
        if(weaponHandler.weaponTypeEquipped != 3){
            FindObjectOfType<SoundManager>().PlayOneShot("PistolShot");
        }
        
        if(weaponHandler.weaponTypeEquipped == 3)
        {
            currentBullet.transform.SetParent(granadeLauncherTarget.transform);
            FindObjectOfType<SoundManager>().PlayOneShot("GranadeLauncherShot");
        }

        if(muzzleFlash != null)
        {
            Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
        }

        currentBullet.transform.forward = directionWithSpread.normalized;

        Vector3 forwardShooting = new Vector3(transform.forward.x, 0f, transform.forward.z) + new Vector3(x, 0, z);
        currentBullet.GetComponent<Rigidbody>().AddForce((firePoint.forward.normalized + directionWithSpread.normalized) * shootForce, ForceMode.Impulse);

        bulletsLeft--;
        bulletsShot++;

        AmmoDisplayUpdate();

        if(allowInvoke)
        {
            playerManager.isShooting = true;

            StartCoroutine("ResetWalk");
            
            playerManager.animationHandler.GetWeaponInt();
            if(weaponHandler.weaponTypeEquipped == 1)
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
            
            if(weaponHandler.weaponTypeEquipped == 1)
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
        // som do reloading
        if(!reloading)
        {
            if(playerManager.playerWeaponHandler.weaponTypeEquipped == 0)
            {
                FindObjectOfType<SoundManager>().PlayOneShot("PistolReload");
            }
            else if(playerManager.playerWeaponHandler.weaponTypeEquipped == 1)
            {
                FindObjectOfType<SoundManager>().PlayOneShot("ShotgunReload");
            }
            else if(playerManager.playerWeaponHandler.weaponTypeEquipped == 2)
            {
                FindObjectOfType<SoundManager>().PlayOneShot("MachineGunReload");
            }
            else if(playerManager.playerWeaponHandler.weaponTypeEquipped == 3)
            {
                FindObjectOfType<SoundManager>().PlayOneShot("GrenadeLauncherReload");
            }
        }
        reloading = true;
        playerManager.ReloadDisplayUpdate();
        reloadDisplay.gameObject.SetActive(true);
        reloadTimeCounter = 0;
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
        cursorManager.InterruptReloadAnim();
        emptyWeapon = false;
    }

    public void ReloadInterrupted()
    {
        CancelInvoke("ReloadFinished");
        reloadTimeCounter = 0;
        playerManager.ReloadEndDisplay();
        reloadDisplay.gameObject.SetActive(false);
        reloading = false;
        // som do reload
        if(playerManager.playerWeaponHandler.weaponTypeEquipped == 0)
        {
            FindObjectOfType<SoundManager>().StopSound("PistolReload");
        }
        else if(playerManager.playerWeaponHandler.weaponTypeEquipped == 1)
        {
            FindObjectOfType<SoundManager>().StopSound("ShotgunReload");
        }
        else if(playerManager.playerWeaponHandler.weaponTypeEquipped == 2)
        {
            FindObjectOfType<SoundManager>().StopSound("MachineGunReload");
        }
        else if(playerManager.playerWeaponHandler.weaponTypeEquipped == 3)
        {
            FindObjectOfType<SoundManager>().StopSound("GrenadeLauncherReload");
        } 
        cursorManager.InterruptReloadAnim();
        Debug.Log("cancela reload");
    }

    public void AmmoDisplayUpdate()
    {
        if(ammoDisplay != null)
        {
            ammoDisplay.SetText(bulletsLeft/bulletPerTap + " / " + magazineSize/bulletPerTap);
            if(weaponHandler.weaponTypeEquipped == 0)
            {
                weaponNameDisplay.SetText("Pistol");
            }
            else if(weaponHandler.weaponTypeEquipped == 1)
            {
                weaponNameDisplay.SetText("Shotgun");
            }
            else if(weaponHandler.weaponTypeEquipped == 2)
            {
                weaponNameDisplay.SetText("Machine Gun");
            }
            else if(weaponHandler.weaponTypeEquipped == 3)
            {
                weaponNameDisplay.SetText("Grenade Launcher");
            }
        }
    }
}
