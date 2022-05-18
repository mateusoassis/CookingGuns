using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TimeSurvivalRoom : MonoBehaviour
{
    private GameManager gameManagerScript;

    [SerializeField] private float timeToStart;

    [SerializeField] public float timeBetweenWaves;

    [SerializeField] public float roomDuration;

    [SerializeField] public float elapsedTime, hours, minutes, seconds;

    [SerializeField] private GameObject[] enemyWaves;

    [SerializeField] private GameObject enemyDestroyer;

    [SerializeField] private TextMeshProUGUI timeHolderText;

    [HideInInspector] public int numberOfWaves;

    private int waveIndex;

    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        elapsedTime = roomDuration;
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        waveIndex = 0;
        numberOfWaves = enemyWaves.Length;
    }

    private void Start()
    {
        StartCoroutine("ControlSpawn");
    }

    private void Update()
    {
        ConvertElapsedTimeToHMS();

        if(elapsedTime <= 0)
        {
            Instantiate(enemyDestroyer, spawnPoint.position, Quaternion.identity);
            gameManagerScript.roomCleared = true;
            gameManagerScript.playerManager.petHandler.petBillboard.ActivateOnEnemiesKilled();
        }
        //OverwriteTimestamp();
    }

    public IEnumerator ControlSpawn()
    {
        yield return new WaitForSeconds(timeToStart);
        SpawnWave();
        timeToStart = 0;
        numberOfWaves -= 1;
        waveIndex = waveIndex + 1;
    }
    public void SpawnWave()
    {
        Instantiate(enemyWaves[waveIndex], spawnPoint.position, Quaternion.identity);
    }

    public void ConvertElapsedTimeToHMS()
    {
        elapsedTime -= Time.deltaTime;
        hours = (int)elapsedTime / 3600;
        minutes = (int)(elapsedTime - (hours * 3600)) / 60;
        seconds = (int)(elapsedTime - (hours * 3600) - (minutes * 60));
    }
    public void OverwriteTimestamp()
    {
        if (hours > 0)
        {
            timeHolderText.SetText(hours.ToString("D2") + "h " + minutes.ToString("D2") + "m " + seconds.ToString("D2") + "s");
            return;
        }
        else if (minutes > 0)
        {
            timeHolderText.SetText(minutes.ToString("D2") + "m " + seconds.ToString("D2") + "s");
            return;
        }
        else
        {
            timeHolderText.SetText(seconds.ToString("D2") + "s");
        }
    }
}
