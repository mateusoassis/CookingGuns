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

    [Header("Tipo de Sala (abre pra ler comentário)")]
    public int roomType;
    // 0 - sala regular
    // 1 - sala de sobrevivência

    [Header("Referências à objetos e scripts")]
    public GameObject pauseUI;
    public GameObject restartConfirmationWindow;
    public GameObject quitConfirmationWindow;
    private GameObject timeHolderObject;
    private TextMeshProUGUI timeHolderText;
    private TextMeshProUGUI levelCounterText;
    public _PlayerManager playerManager;
    private CameraShake shakeEffect;

    [Header("Slowdown ao finalizar sala")]
    [SerializeField] private float slowdownDuration;
    [SerializeField] private float slowdownFactor;
    private bool slowdown;
    private bool slowdownEnded;

    [Header("Slowdown ao tomar dano")]
    [SerializeField] private float damageSlowdownDuration;
    private float damageSlowdownTimer;
    [SerializeField] private float damageSlowdownFactor;
    private bool damageSlowdown;

    [Header("Cursores")]
    public CursorManager mainCursor;
    public CursorMiniManager miniCursor;

    [Header("Booleano de fim de sala")]
    public bool roomCleared;

    [Header("Variáveis de temporizador")]
    public float elapsedTime;
    public int hours;
    public int minutes;
    public int seconds;
    
    [Header("Variáveis de pause")]
    public bool pausedGame;
    public bool pauseBlock;
    public bool confirmationWindowOpen;

    [Header("Booleanos de fade")]
    public bool fadeToChangeScene;
    public bool stopFading;

    [Header("Booleano de queda na água")]
    public bool outOfBoundsCollider;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if(SceneManager.GetActiveScene().buildIndex == 3)
        {
            //Debug.Log("lul");
            playerInfo.isOnTutorial = true;
            //roomCleared = true;
        }
        else
        {
            playerInfo.isOnTutorial = false;
            //roomCleared = false;
        }
        roomCleared = false;
        /*pauseUI = GameObject.Find("PauseUI");
        restartConfirmationWindow = GameObject.Find("RestartConfirmationWindow");
        quitConfirmationWindow = GameObject.Find("QuitConfirmationWindow");*/
        timeHolderObject = GameObject.Find("TimeHolder");
        //timeHolderText = GameObject.Find("TimeHolderText").GetComponent<TextMeshProUGUI>();
        playerManager = GameObject.Find("Player").GetComponent<_PlayerManager>();
        levelCounterText = GameObject.Find("LevelCounterText").GetComponent<TextMeshProUGUI>();
        shakeEffect = GameObject.Find("Shake").GetComponent<CameraShake>();
        mainCursor = GameObject.Find("CursorManager").GetComponent<CursorManager>();
        miniCursor = mainCursor.gameObject.GetComponent<CursorMiniManager>();
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
            //Debug.Log("lul 2");
            levelCounterText.text = ("Tutorial");
        }
        else
        {
            levelCounterText.text = ("Room " + playerManager.playerInfo.playerCurrentRoom.ToString());
        }

        if(roomType == 0)
        {
            timeHolderObject.SetActive(false);
        }
    }

    void Update()
    {
        if(damageSlowdown)
        {
            DamageSlowtime();
        }
        //ConvertElapsedTimeToHMS();
        //OverwriteTimestamp();
        if(!roomCleared)
        {
            return;
        }
        else if(roomCleared && !slowdown && !playerManager.isFading && SceneManager.GetActiveScene().buildIndex != 3)
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

    public void DamageCausedSlowtime()
    {
        damageSlowdown = true;
        damageSlowdownTimer = damageSlowdownDuration;
        Time.timeScale = damageSlowdownFactor;
    }
    public void DamageSlowtimeEnded()
    {
        Time.timeScale = 1;
        damageSlowdown = false;
    }
    public void DamageSlowtime()
    {
        damageSlowdownTimer -= Time.unscaledDeltaTime;
        if(damageSlowdownTimer < 0)
        {
            DamageSlowtimeEnded();
        }
    }

    public void StartSlowTime()
    {
        slowdown = true;
        shakeEffect.Invoke("Shockwave" , 0f);
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void SlowTime()
    {
        if(!pausedGame)
        {
            if(slowdown && !slowdownEnded)
            {
                Time.timeScale += (1f / slowdownDuration) * Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
                if(Time.timeScale >= 1f)
                {
                    slowdownEnded = true;
                    DamageSlowtimeEnded();
                }
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
            DisableCursors();
            SoundButton();
            pausedGame = true;
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void PauseForDialogue()
    {
        if(!pausedGame)
        {
            DisableCursors();
            SoundButton();
            pauseBlock = true;
            pausedGame = true;
            Time.timeScale = 0;
        }
    }
    public void ResumeForDialogue()
    {
        StartCoroutine(ResumeDialogueCoroutine());
    }

    public IEnumerator ResumeDialogueCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if(pausedGame)
        {
            EnableCursors();
            pauseBlock = false;
            pausedGame = false;
            Time.timeScale = 1;
        }
    }

    public void PauseAndLose()
    {
        if(!pausedGame)
        {
            DisableCursors();
            pausedGame = true;
            pauseUI.SetActive(true);
            Time.timeScale = 0;
            playerManager.playerStats.youLoseHolder.PlayerLost();
        }
    }

    public void ResumeGame()
    {
        if(pausedGame && !pauseBlock)
        {
            EnableCursors();
            SoundButton();
            pausedGame = false;
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void RestartConfirmation()
    {
        if(!confirmationWindowOpen)
        {
            SoundButton();
            confirmationWindowOpen = true;
            restartConfirmationWindow.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SoundButton();
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
            SoundButton();
            confirmationWindowOpen = true;
            quitConfirmationWindow.SetActive(true);
        }
    }

    public void QuitGame()
    {
        SoundButton();
        SceneManager.LoadScene("1_MenuScene", LoadSceneMode.Single);
        //playerInfo.totalPlayedTime += (int)elapsedTime;
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

    public void SoundButton()
    {
        FindObjectOfType<SoundManager>().PlayOneShot("Butao");
    }

    public void DeadStuff()
    {
        playerManager.playerStats.DeadPlayer();
        DisableCursors();
    }

    public void DisableCursors()
    {
        miniCursor.DisableMiniCursor();
        mainCursor.DisableMainCursor();
    }

    public void EnableCursors()
    {
        miniCursor.EnableMiniCursor();
        mainCursor.EnableMainCursor();
    }
}
