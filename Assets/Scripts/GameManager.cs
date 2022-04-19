using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject restartConfirmationWindow;
    [SerializeField] private GameObject quitConfirmationWindow;
    [SerializeField] private TextMeshProUGUI timeHolderText;

    [Header("Slowdown ao finalizar sala")]
    public EnemySpawner enemySpawner;
    public float slowdownDuration;
    public float slowdownFactor;
    public bool slowdown;
    public bool slowdownEnded;

    private float elapsedTime;
    public int hours;
    public int minutes;
    public int seconds;
    
    public bool pausedGame;
    public bool confirmationWindowOpen;
    public _PlayerManager playerManager;

    void Awake()
    {
        /*pauseUI = GameObject.Find("PauseUI");
        restartConfirmationWindow = GameObject.Find("RestartConfirmationWindow");
        quitConfirmationWindow = GameObject.Find("QuitConfirmationWindow");*/
        timeHolderText = GameObject.Find("TimeHolderText").GetComponent<TextMeshProUGUI>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        playerManager = GameObject.Find("Player").GetComponent<_PlayerManager>();
    }

    void Start()
    {
        slowdown = false;
        ResumeGame();
        Time.timeScale = 1;
        confirmationWindowOpen = false;
        elapsedTime = 0f;
    }

    void Update()
    {
        ConvertElapsedTimeToHMS();
        OverwriteTimestamp();
        if(!enemySpawner.roomCleared)
        {
            return;
        }
        else if(enemySpawner.roomCleared && !slowdown && !playerManager.isFading)
        {
            StartSlowTime();
        }

        SlowTime();
    }

    public void StartSlowTime()
    {
        slowdown = true;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void SlowTime()
    {
        if(slowdown && !slowdownEnded)
        {
            Time.timeScale += (1f / slowdownDuration) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            if(Time.timeScale >= 1f)
            {
                slowdownEnded = true;
            }
        }
    }

    public void ConvertElapsedTimeToHMS()
    {
        if(!pausedGame)
        {
            elapsedTime += Time.deltaTime;
            hours = (int)elapsedTime/3600;
            minutes = (int)(elapsedTime - (hours * 3600))/60;
            seconds = (int)(elapsedTime - (hours * 3600) - (minutes * 60));
        }
    }

    public void OverwriteTimestamp()
    {
        if(hours > 0)
        {
            timeHolderText.SetText(hours.ToString("D2") + "h " + minutes.ToString("D2") + "m " + seconds.ToString("D2") + "s");
            return;
        }
        else if(minutes > 0)
        {
            timeHolderText.SetText(minutes.ToString("D2") + "m " + seconds.ToString("D2") + "s");
            return;
        }
        else
        {
            timeHolderText.SetText(seconds.ToString("D2") + "s");
        }
    }

    public void PauseGame()
    {
        if(!pausedGame)
        {
            pausedGame = true;
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeGame()
    {
        if(pausedGame)
        {
            pausedGame = false;
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void RestartConfirmation()
    {
        if(!confirmationWindowOpen)
        {
            confirmationWindowOpen = true;
            restartConfirmationWindow.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void QuitConfirmation()
    {
        if(!confirmationWindowOpen)
        {
            confirmationWindowOpen = true;
            quitConfirmationWindow.SetActive(true);
        }
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("0_MenuScene");
    }

    public void CloseAllConfirmationWindows()
    {
        confirmationWindowOpen = false;
        restartConfirmationWindow.SetActive(false);
        quitConfirmationWindow.SetActive(false);
    }
}
