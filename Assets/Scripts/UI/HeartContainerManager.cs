using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainerManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public _PlayerStats playerStats;
    List<_PlayerHeartManager> hearts = new List<_PlayerHeartManager>();

    public GameObject[] heartsObjects;
    public int hpLost;
    private int index;

    void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<_PlayerStats>();
        
        //heartsObjects = new GameObject[playerStats.playerMaxHealth];
    }

    void Start()
    {
        //hpLost = playerStats.playerMaxHealth - playerStats.playerCurrentHealth;
        hpLost = playerStats.playerMaxHealth - playerStats.playerManager.playerInfo.healthFromLastRoom;
        
        /*
        if(playerStats.playerManager.playerInfo.currentSceneIndex > 2)
        {
            //UpdateAllHearts();
        }
        */
    }

    void Update()
    {
        hpLost = playerStats.playerMaxHealth - playerStats.playerCurrentHealth;
        UpdateAllHearts();
    }

    private void OnEnable()
    {
        //_PlayerStats.OnPlayerDamaged += DrawHearts;
    }

    private void OnDisable()
    {
        //_PlayerStats.OnPlayerDamaged -= DrawHearts;
    }

    

    public void UpdateAllHearts()
    {
        if(playerStats.playerCurrentHealth >= 0)
        {
            for(int i = 0; i < playerStats.playerMaxHealth; i++)
            {
                heartsObjects[i].GetComponent<HeartAnimatorScript>().CheckIfActiveOrNot();
            }
        }
        else
        {
            for(int i = 0; i < heartsObjects.Length; i++)
            {
                heartsObjects[i].SetActive(false);
            }
        }
    }
    public void FullHeal()
    {
        playerStats.playerCurrentHealth = playerStats.playerMaxHealth;
        playerStats.playerManager.playerInfo.healthFromLastRoom = playerStats.playerMaxHealth;
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
