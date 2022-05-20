using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Player Info")]
    public PlayerInfo playerInfo;

    [Header("Referências")]
    public GameObject pauseUI;
    public GameObject restartConfirmationWindow;
    public GameObject quitConfirmationWindow;
    private TextMeshProUGUI timeHolderText;
    private TextMeshProUGUI levelCounterText;

    [Header("Slowdown ao finalizar sala")]
    public float slowdownDuration;
    public float slowdownFactor;
    public bool slowdown;
    public bool slowdownEnded;

    [Header("Booleano de fim de sala")]
    public bool roomCleared;

    [Header("Variáveis de temporizador")]
    public float elapsedTime;
    public int hours;
    public int minutes;
    public int seconds;
    
    [Header("Variáveis de pause")]
    public bool pausedGame;
    public bool confirmationWindowOpen;
    public _PlayerManager playerManager;
    public CameraShake shakeEffect;

    [Header("Booleanos de fade")]
    public bool fadeToChangeScene;
    public bool stopFading;

    [Header("Booleano de queda na água")]
    public bool outOfBoundsCollider;

    void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            Debug.Log("lul");
            playerInfo.isOnTutorial = true;
        }
        else
        {
            playerInfo.isOnTutorial = false;
        }
        roomCleared = false;
        /*pauseUI = GameObject.Find("PauseUI");
        restartConfirmationWindow = GameObject.Find("RestartConfirmationWindow");
        quitConfirmationWindow = GameObject.Find("QuitConfirmationWindow");*/
        timeHolderText = GameObject.Find("TimeHolderText").GetComponent<TextMeshProUGUI>();
        playerManager = GameObject.Find("Player").GetComponent<_PlayerManager>();
        levelCounterText = GameObject.Find("LevelCounterText").GetComponent<TextMeshProUGUI>();
        shakeEffect = GameObject.Find("Shake").GetComponent<CameraShake>();
    }

    void Start()
    {
        slowdown = false;
        ResumeGame();
        Time.timeScale = 1;
        confirmationWindowOpen = false;
        elapsedTime = 0f;
        if(playerManager.playerInfo.isOnTutorial)
        {
            Debug.Log("lul 2");
            levelCounterText.text = ("Tutorial");
        }
        else
        {
            levelCounterText.text = ("Room " + playerManager.playerInfo.playerCurrentRoom.ToString());
        }
    }

    void Update()
    {
        ConvertElapsedTimeToHMS();
        OverwriteTimestamp();
        if(!roomCleared)
        {
            return;
        }
        else if(roomCleared && !slowdown && !playerManager.isFading)
        {
            StartSlowTime();
        }

        SlowTime();
    }

    /*
    public void Restart()
    {
        if(playerInfo.playerCurrentRoom < 1)
        {
            playerInfo.playerCurrentRoom = 0;
            playerInfo.healthFromLastRoom = 0;
            SceneManager.LoadScene("0_1_Tutorial", LoadSceneMode.Single);
        }
        else
        {
            playerInfo.playerCurrentRoom = 1;
            playerInfo.healthFromLastRoom = 0;
            SceneManager.LoadScene("_1_RoomScene", LoadSceneMode.Single);
        }
    }
    */

    public void StartSlowTime()
    {
        slowdown = true;
        shakeEffect.Invoke("Shockwave" , 0f);
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

    public void PauseAndLose()
    {
        if(!pausedGame)
        {
            pausedGame = true;
            pauseUI.SetActive(true);
            Time.timeScale = 0;
            playerManager.playerStats.youLoseHolder.PlayerLost();
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
        if(playerInfo.isOnTutorial)
        {
            playerInfo.TutorialReset();
            SceneManager.LoadScene("3_TutorialScene", LoadSceneMode.Single);
        }
        else
        {
            playerInfo.NewGameReset();
            SceneManager.LoadScene("_Room01");
        }
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
        SceneManager.LoadScene("1_MenuScene", LoadSceneMode.Single);
        playerInfo.totalPlayedTime += (int)elapsedTime;
    }

    public void CloseAllConfirmationWindows()
    {
        confirmationWindowOpen = false;
        restartConfirmationWindow.SetActive(false);
        quitConfirmationWindow.SetActive(false);
    }

    public void CreditsScene()
    {
        if(playerInfo.fastestRunSoFar == 0)
        {
            playerInfo.fastestRunSoFar = (int)elapsedTime;
        }
        else if(playerInfo.fastestRunSoFar > (int)elapsedTime)
        {
            playerInfo.fastestRunSoFar = (int)elapsedTime;
        }
        SceneManager.LoadScene("2_CreditsScene", LoadSceneMode.Single);
    }
}
