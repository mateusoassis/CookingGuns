using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TimeSurvivalRoom : MonoBehaviour
{
    private GameManager gameManagerScript;

    private MoveWaveCounter moveWaveScript;

    private TextMeshProUGUI waveCounterText;

    private bool roomStarted;

    [SerializeField] private float timeToStart;

    [SerializeField] public float timeBetweenWaves;

    [SerializeField] public float roomDuration;

    [SerializeField] public float elapsedTime;

    [SerializeField] private GameObject[] enemyWaves;

    [SerializeField] private GameObject enemyDestroyer;

    [SerializeField] private TextMeshProUGUI timeHolderText;

    [HideInInspector] public int numberOfWaves;

    private int initialNumberofWaves;

    private bool enemyDestroyerActivated;

    private int waveIndex;

    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        timeHolderText = GameObject.Find("TimeHolderText").GetComponent<TextMeshProUGUI>();
        moveWaveScript = GameObject.Find("WaveCounter").GetComponent<MoveWaveCounter>();
        waveCounterText = moveWaveScript.gameObject.GetComponent<TextMeshProUGUI>();
        roomStarted = false;
        enemyDestroyerActivated = false;
        elapsedTime = roomDuration;
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
        waveIndex = 0;
        numberOfWaves = enemyWaves.Length;
        initialNumberofWaves = enemyWaves.Length;
    }

    private void Start()
    {
        waveCounterText.SetText(numberOfWaves + "/" + initialNumberofWaves);
        StartCoroutine("StartRoom");
    }

    private void Update()
    {
        if (roomStarted) 
        {
            OverwriteTimestamp();
        }

        if(elapsedTime <= 0)
        {
            if (enemyDestroyerActivated == false && !gameManagerScript.roomCleared) 
            {
                StartCoroutine("SpawnEnemyDestroyer");
                gameManagerScript.roomCleared = true;
                gameManagerScript.playerManager.petHandler.petBillboard.ActivateOnEnemiesKilled();
            }
        }
    }

    public IEnumerator ControlSpawn()
    {
        yield return new WaitForSeconds(timeToStart);
        if (!gameManagerScript.roomCleared) 
        {
            moveWaveScript.TriggerMovement();
        }
        SpawnWave();
        numberOfWaves -= 1;
        waveIndex = waveIndex + 1;
        waveCounterText.SetText((numberOfWaves + 1)  + "/" + initialNumberofWaves);
    }
    public void SpawnWave()
    {
        Instantiate(enemyWaves[waveIndex], spawnPoint.position, Quaternion.identity);
    }

    public void SubtractRoomDuration()
    {
        elapsedTime -= Time.deltaTime;
    }
    public void OverwriteTimestamp()
    {
        if(!gameManagerScript.roomCleared && !gameManagerScript.playerManager.isFading)
        {
            SubtractRoomDuration();

            if(elapsedTime > 0)
            {
                timeHolderText.SetText(elapsedTime.ToString("0.00") + " s");
                if (elapsedTime <= 10.0f) 
                {
                    timeHolderText.color = new Color(255, 0, 0, 255);
                }
            }
            else
            {
                timeHolderText.SetText("0 s");
            }
        }
    }

    private IEnumerator SpawnEnemyDestroyer() 
    {
        Instantiate(enemyDestroyer, spawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        enemyDestroyerActivated = true;
        yield break;
    }

    private IEnumerator StartRoom() 
    {
        yield return new WaitForSeconds(4.0f);
        StartCoroutine("ControlSpawn");
        roomStarted = true;
    }
}
