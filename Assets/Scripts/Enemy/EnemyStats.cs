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
    public GameObject dropPrefab;
    public float dropPrefabYOffset;
    public float dropChance;
    public PlayerInfo playerInfo;
    public bool hitRecently;
    public HealthbarBehaviour healthbarScript;
    public bool underOneFourthHP;

    public SimpleFlash flashEffect;

    void Start(){
        enemyHealth = enemyMaxHealth;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        healthbarScript = GetComponentInChildren<HealthbarBehaviour>();
        flashEffect = GetComponent<SimpleFlash>();
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
                Debug.Log(name);
                enemySpawner.enemiesKilled++;
            }
            playerInfo.totalEnemiesKilled++;

            float realDropChance = 100 - dropChance;
            int u = Random.Range(0, 101);
            if(u >= realDropChance)
            {
                Instantiate(dropPrefab, transform.position + new Vector3(0f, dropPrefabYOffset, 0f), Quaternion.identity);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerBullet")
        {
            if((other.gameObject.TryGetComponent(out BulletScript bulletScript)))
            {
                flashEffect.Flash();
                TakeDamage(bulletScript.damageDone);
            }

        }
        if(other.gameObject.tag == "PlayerGranade")
        {
            if((other.gameObject.TryGetComponent(out GranadeAreaDamage granadeAreaDamage)))
            {
                flashEffect.Flash();
                TakeDamage(granadeAreaDamage.damageDone);
            }
        }
    }
}
