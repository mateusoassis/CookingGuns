using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StalactiteHandler : MonoBehaviour
{   
    [Header("Delay de drop e randomizador")]
    [SerializeField] private float dropDelay;
    [SerializeField] private float randomizer;

    [Header("Não mexer")]
    [SerializeField] private Transform spawnTransform;
    [SerializeField] private GameObject stalactitePrefab;
    [SerializeField] private GameObject groundCirclePrefab;
    [SerializeField] private float YOffset;
    private float dropTimer;
    private bool isTutorial;

    void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            isTutorial = true;
        }
    }

    void Start()
    {
        dropTimer = dropDelay + randomizer;
    }

    void Update()
    {
        if(!isTutorial && !GetComponent<_PlayerManager>().gameManager.roomCleared)
        {
            dropTimer -= Time.deltaTime;
            {
                if(dropTimer <= 0)
                {
                    GameObject Stalactite = Instantiate(stalactitePrefab, transform.position + new Vector3(0f, YOffset, 0f), Quaternion.identity) as GameObject;
                    Stalactite.transform.localScale = new Vector3(7f, 0.06f, 7f);
                    GameObject StalactiteGroundArea = Instantiate(groundCirclePrefab, spawnTransform.position, Quaternion.identity) as GameObject;
                    //StalactiteGroundArea.transform.SetParent(spawnTransform);
                    StalactiteGroundArea.transform.localScale = new Vector3(9f, 0.0001f, 9f);
                    dropTimer = dropDelay + Random.Range(-randomizer, randomizer);
                }
            }
        } 
    }
}
