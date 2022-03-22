using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject restartConfirmationWindow;
    [SerializeField] private GameObject quitConfirmationWindow;
    
    public bool pausedGame;
    public bool confirmationWindowOpen;
    
    void Start()
    {
        confirmationWindowOpen = false;
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
