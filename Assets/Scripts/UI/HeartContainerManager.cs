using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainerManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public _PlayerStats playerStats;
    List<_PlayerHeartManager> hearts = new List<_PlayerHeartManager>();
    
    public int sceneIndex;

    public GameObject[] heartsObjects;
    public int hpLost;
    private int index;

    void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<_PlayerStats>();
        
        //heartsObjects = new GameObject[playerStats.playerMaxHealth];
    }

    void Update()
    {
        hpLost = playerStats.playerMaxHealth - playerStats.playerCurrentHealth;
    }

    private void OnEnable()
    {
        //_PlayerStats.OnPlayerDamaged += DrawHearts;
    }

    private void OnDisable()
    {
        //_PlayerStats.OnPlayerDamaged -= DrawHearts;
    }

    private void Start()
    {
        hpLost = playerStats.playerMaxHealth - playerStats.playerCurrentHealth;
        
        if(playerStats.playerManager.playerInfo.currentSceneIndex != 1)
        {
            UpdateAllHearts();
        }
    }

    public void UpdateAllHearts()
    {
        if(playerStats.playerCurrentHealth > 0)
        {
            for(int i = 0; i < playerStats.playerMaxHealth; i++)
            {
                heartsObjects[i].GetComponent<HeartAnimatorScript>().CheckIfActiveOrNot();
            }
        } 
    }
    public void FullHeal()
    {
        playerStats.playerCurrentHealth = playerStats.playerMaxHealth;
        hpLost = 0;
        for(int i = 0; i < playerStats.playerMaxHealth; i++)
        {
            heartsObjects[i].GetComponent<HeartAnimatorScript>().HealThisHeart();
        }
    }

    /*
    public void DrawHearts()
    {
        ClearHearts();
        
        int heartsToMake = (int)playerStats.playerMaxHealth;
        for(int i = 0; i < heartsToMake; i++)
        {
            index = i;
            CreateEmptyHeart(index);
        }
    }

    public void CreateEmptyHeart(int i)
    {
        heartsObjects[i] = Instantiate(heartPrefab);
        heartsObjects[i].transform.SetParent(transform);
        heartsObjects[i].name = "Heart_" + index;
    }

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<_PlayerHeartManager>();
    }
    */
    
    
    
}
