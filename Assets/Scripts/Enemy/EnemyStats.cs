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
    public float dropPrefabYOffset;
    public List<float> dropChance;
    public bool isPudim;
    public PlayerInfo playerInfo;
    public bool hitRecently;
    public HealthbarBehaviour healthbarScript;
    public bool underOneFourthHP;
    public DamageFlash flashEffect;

    public int enemyType;
    // 0 torre
    // 1 jujuba
    // 2 pudim
    // 3 shieldoca

    void Start(){
        enemyHealth = enemyMaxHealth;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        healthbarScript = GetComponentInChildren<HealthbarBehaviour>();
        flashEffect = GetComponent<DamageFlash>();
    }
    
    public void TakeDamage(int damageTaken)
    {
        enemyHealth -= damageTaken;
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
        
        if(enemyHealth <= 0)
        {            
            Destroy(this.gameObject);
            if(GetComponent<MinusOnDestroy>() == null)
            {
                //Debug.Log(name);
                enemySpawner.enemiesKilled++;
            }
            playerInfo.totalEnemiesKilled++;
            
            if(dropPrefab[0] != null)
            {
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
                } else/* if(enemyType != 1)*/
                {
                    int u = Random.Range(0, 100);
                    if(u > 50){
                        Instantiate(dropPrefab[0], transform.position + new Vector3(0f, dropPrefabYOffset, 0f), Quaternion.identity);
                    } else {
                        Instantiate(dropPrefab[1], transform.position + new Vector3(0f, dropPrefabYOffset, 0f), Quaternion.identity);
                    }
                } 
            }
            
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBullet")
        {
            if((other.gameObject.TryGetComponent(out BulletScript bulletScript)))
            {
                //flashEffect.FlashStart();
                TakeDamage(bulletScript.damageDone);
            }

        }
        if(other.gameObject.tag == "PlayerGranade")
        {
            if((other.gameObject.TryGetComponent(out GranadeAreaDamage granadeAreaDamage)))
            {
                //flashEffect.FlashStart();
                TakeDamage(granadeAreaDamage.damageDone);
            }
        }
    }
}
