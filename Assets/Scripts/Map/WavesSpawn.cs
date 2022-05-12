using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyWave;

    [SerializeField] public int numberOfWaves;

    [SerializeField] private int waveIndex;

    [SerializeField] private Transform mapCenter;

    private void Awake()
    {
        waveIndex = 0;
        numberOfWaves = enemyWave.Length;
    }

    private void Start()
    {
        if (numberOfWaves>0) 
        {
            SpawnWave();
        }
    }
    public void SpawnWave()
    {
        Instantiate(enemyWave[waveIndex], mapCenter.position, Quaternion.identity);
        waveIndex = waveIndex + 1;
    }
    private IEnumerator ControlSpawn()
    {
        if (numberOfWaves > 0)
        {
            yield return new WaitForSeconds(1.0f);
            SpawnWave();
            yield return new WaitForSeconds(0.5f);
            yield break;
        }else if(waveIndex > enemyWave.Length)
        {
            waveIndex = enemyWave.Length;
            Debug.Log("Matou todos");
        }
    }
}
