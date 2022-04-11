using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject restartConfirmationWindow;
    [SerializeField] private GameObject quitConfirmationWindow;
    [SerializeField] private TextMeshProUGUI timeHolderText;
    private float elapsedTime;
    public int hours;
    public int minutes;
    public int seconds;
    
    public bool pausedGame;
    public bool confirmationWindowOpen;
    
    void Start()
    {
        ResumeGame();
        Time.timeScale = 1;
        confirmationWindowOpen = false;
        elapsedTime = 0f;

    }

    void Update()
    {
        ConvertElapsedTimeToHMS();
        OverwriteTimestamp();
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
