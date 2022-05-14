using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesSpawn : MonoBehaviour
{
    private GameManager gameManagerScript;

    [SerializeField] private float timeBetweenWaves;

    [SerializeField] private GameObject[] enemyWaves;

    [HideInInspector] public int numberOfWaves;

    private int waveIndex;

    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        waveIndex = 0;
        numberOfWaves = enemyWaves.Length;
    }

    private void Start()
    {
        StartCoroutine("ControlSpawn");
    }
    public void SpawnWave()
    {
        Instantiate(enemyWaves[waveIndex], spawnPoint.position, Quaternion.identity);
    }
    private IEnumerator ControlSpawn()
    {
        //yield return new WaitForSeconds(timeBetweenWaves);
        if (numberOfWaves > 0)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            SpawnWave();
            numberOfWaves -= 1;
            waveIndex = waveIndex + 1;
            yield break;
        }else if (numberOfWaves <= 0)
        {
            gameManagerScript.roomCleared = true;
        }
    }
}
