using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainerManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public _PlayerStats playerStats;
    List<_PlayerHeartManager> hearts = new List<_PlayerHeartManager>();

    private void Awake()
    {
        playerStats = GameObject.Find("Player").GetComponent<_PlayerStats>();
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
        
        int heartsToMake = (int)playerStats.playerCurrentHealth;
        for(int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);
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
