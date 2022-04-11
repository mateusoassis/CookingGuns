using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHeartContainer : MonoBehaviour
{
    public GameObject heartPrefab;
    public _PlayerStats playerStats;
    List<_PlayerHeartManager> hearts = new List<_PlayerHeartManager>();

    private void Awake()
    {
        playerStats = GameObject.Find("TutorialPlayer").GetComponent<_PlayerStats>();
    }

    private void OnEnable()
    {
        _PlayerStats.OnPlayerDamaged += DrawHearts;
    }

    private void OnDisable()
    {
        _PlayerStats.OnPlayerDamaged -= DrawHearts;
    }

    private void Start()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {
        ClearHearts();

        float maxHealthRemainder = playerStats.playerMaxHealth % 2;
        int heartsToMake = (int)((playerStats.playerMaxHealth/ 2)  + maxHealthRemainder);

        for(int i = 0;i<heartsToMake;i++)
        {
            CreateEmptyHeart();
        }

        for(int i = 0; i < hearts.Count ;i++)
        {
            int heartStatusRemainder = (int )Mathf.Clamp(playerStats.playerCurrentHealth - (i*2), 0, 2);
            hearts[i].SetHeartImage((_PlayerHeartManager.HeartStatus)heartStatusRemainder);
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        _PlayerHeartManager heartComponent = newHeart.GetComponent<_PlayerHeartManager>();
        heartComponent.SetHeartImage(_PlayerHeartManager.HeartStatus.Empty);
        hearts.Add(heartComponent);
    }

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<_PlayerHeartManager>();
    }
}

