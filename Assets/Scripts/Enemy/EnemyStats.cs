using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public int enemyMaxHealth;
    public int enemyHealth;
    [SerializeField] private PlayerController playerController;
    
    public List<GameObject> dropPrefab;
    public GameObject smokePrefab;
    public float dropPrefabYOffset;
    public List<float> dropChance;
    public bool isPudim;
    public PlayerInfo playerInfo;
    public bool hitRecently;
    public HealthbarBehaviour healthbarScript;
    public bool underOneFourthHP;
    [SerializeField] private ParticleSystem damageParticle;

    [SerializeField] private GameObject[] enemyMeshParts;

    [SerializeField] private Material[] oldMaterials;


    [SerializeField] private float flashDuration;
    [SerializeField] private GameObject enemyFlashingPart;
    [SerializeField] private Material flashMaterial;
    private Material oldMaterial;
    private Coroutine flashRoutine;

    public int enemyType;
    // 0 torre
    // 1 jujuba
    // 2 pudim
    // 3 shieldoca

    private void Awake()
    {
        oldMaterials = new Material[enemyMeshParts.Length];
    }

    void Start(){
        enemyHealth = enemyMaxHealth;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        healthbarScript = GetComponentInChildren<HealthbarBehaviour>();
        if(enemyType != 0)
        {
            oldMaterial = enemyFlashingPart.GetComponent<MeshRenderer>().material;
        }

        for (int n = 0; n < enemyMeshParts.Length; n++)
        {
            oldMaterials[n] = enemyMeshParts[n].GetComponent<MeshRenderer>().material;
        }
    }
    
    public void TakeDamage(int damageTaken)
    {
        enemyHealth -= damageTaken;
        EnemyFlash();
        if(TryGetComponent<TowerBehaviour>(out TowerBehaviour towerBehaviour))
        {
            towerBehaviour.towerDamaged = true;
        }
        if((float)enemyMaxHealth/4 > (float)enemyHealth)
        {
            underOneFourthHP = true;
        }

        if(underOneFourthHP)
        {
            healthbarScript.PermanentlyShowHP();
        }
        else
        {
            healthbarScript.StartCount();
        }
        FindObjectOfType<SoundManager>().PlayOneShot("EnemyShot");

        if(enemyHealth <= 0)
        {            
            FindObjectOfType<SoundManager>().PlayOneShot("AnyEnemyDying");
            Destroy(this.gameObject);
            Instantiate(smokePrefab, transform.position , Quaternion.identity);
            if(GetComponent<MinusOnDestroy>() == null)
            {
                Debug.Log(name);
            }
            playerInfo.totalEnemiesKilled++;
            playerInfo.totalEnemiesKilledPerWeapon[playerInfo.lastWeaponTypeEquipped]++;
            
            if(dropPrefab[0] != null)
            {
                for(int i = 0; i < dropPrefab.Count; i++)
                {
                    Instantiate(dropPrefab[i], transform.position + new Vector3(0f, dropPrefabYOffset, 0f), Quaternion.identity);
                }
                /*
                if(!isPudim)
                {
                    for(int i = 0; i < dropPrefab.Count; i++)
                    {
                        float realDropChance = 100 - dropChance[i];
                        int u = Random.Range(0, 101);
                        if(u >= realDropChance)
                        {
                            Instantiate(dropPrefab[i], transform.position + new Vector3(0f, dropPrefabYOffset, 0f), Quaternion.identity);
                        }
                    }
                }
                else
                {
                    int u = Random.Range(0, 100);
                    if(u > 50){
                        Instantiate(dropPrefab[0], transform.position + new Vector3(0f, dropPrefabYOffset, 0f), Quaternion.identity);

                    } else {
                        Instantiate(dropPrefab[1], transform.position + new Vector3(0f, dropPrefabYOffset, 0f), Quaternion.identity);
                    }
                } 
                */
            }
        }
    }

    public void EnemyFlash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine(EnemyFlashRoutine());
        Debug.Log("fez o flash");
    }

    private IEnumerator EnemyFlashRoutine()
    {
        /*enemyFlashingPart.GetComponent<MeshRenderer>().material = flashMaterial;

        yield return new WaitForSeconds(flashDuration);

        enemyFlashingPart.GetComponent<MeshRenderer>().material = oldMaterial;

        flashRoutine = null;
        */

        foreach (GameObject k in enemyMeshParts)
        {
            k.GetComponent<MeshRenderer>().material = flashMaterial;
        }

        yield return new WaitForSeconds(flashDuration);

        for (int n = 0; n < enemyMeshParts.Length; n++)
        {
            enemyMeshParts[n].GetComponent<MeshRenderer>().material = oldMaterials[n];
        }
        flashRoutine = null;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBullet"|| other.gameObject.tag == "EnemyDestroyer")
        {
            if(other.gameObject.TryGetComponent(out BulletScript bulletScript))
            {
                if (damageParticle != null) 
                {
                    damageParticle.Play();
                }
                TakeDamage(bulletScript.damageDone);
            }

        }
        if(other.gameObject.tag == "PlayerGranade")
        {
            if(other.gameObject.TryGetComponent(out GranadeAreaDamage granadeAreaDamage))
            {
                if (damageParticle != null)
                {
                    damageParticle.Play();
                }
                TakeDamage(granadeAreaDamage.damageDone);
            }
        }
        if (other.gameObject.tag == "BarrelExplosion")
        {
            if (other.gameObject.TryGetComponent(out BarrelTrapExplosion barrelAreaDamage))
            {
                if (damageParticle != null)
                {
                    damageParticle.Play();
                }
                TakeDamage(barrelAreaDamage.damageDoneInEnemy);
            }
        }
    }
}
