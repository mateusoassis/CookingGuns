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

    void Update()
    {
        hpLost = playerStats.playerMaxHealth - playerStats.playerCurrentHealth;
    }

    private void OnEnable()
    {
        //_PlayerStats.OnPlayerDamaged += DrawHearts;
        /*for(int i = 0; i < playerStats.playerMaxHealth; i++)
        {
            if(i > playerStats.playerCurrentHealth)
            {
                heartsObjects[i].GetComponent<Animator>().SetTrigger("Normal");
            }
            
        }*/
        
    }

    private void OnDisable()
    {
        /*for(int i = 0; i < playerStats.playerMaxHealth; i++)
        {
            if(i > playerStats.playerCurrentHealth)
            {
                heartsObjects[i].GetComponent<Animator>().SetTrigger("Disabled");
            }
            
        }*/
        
    }

    private void Start()
    {
        //DrawHearts();
        hpLost = playerStats.playerMaxHealth - playerStats.playerCurrentHealth;
        //UpdateAllHearts();
        /*
        if(hpLost == 0)
        {
            return;
        }
        else
        {
            while(hpLost - heartController != 0)
            {
                UpdateHPonUI(playerStats.playerMaxHealth - heartController);
                heartController--;
            }
        }
        */
    }

    public void UpdateAllHearts()
    {
        for(int i = 0; i < playerStats.playerMaxHealth; i++)
        {
            heartsObjects[i].GetComponent<HeartAnimatorScript>().CheckIfActiveOrNot();
        }
    }

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

    public void UpdateHPonUI(int n)
    {
        //for(int i = 0; i < playerStats.playerMaxHealth; i++)
        //{
            //if(i+1 > playerStats.playerCurrentHealth)
            //{
                //Debug.Log("disable " + i);
                heartsObjects[n-1].GetComponent<Animator>().SetTrigger("Disabled");
            //}
        //}
    }

    public void FullHeal(int n)
    {
        for(int i = 0; i < playerStats.playerMaxHealth; i++)
        {
            if(n < i)
            {
                heartsObjects[i].GetComponent<Animator>().SetTrigger("Normal");
                heartsObjects[i].GetComponent<HeartAnimatorScript>().state = 0;
            }
        }
    }
}
