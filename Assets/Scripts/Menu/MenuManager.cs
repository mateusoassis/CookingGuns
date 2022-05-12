using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public PlayerInfo playerInfo;
    public GameObject continueButton;

    public bool isOnMenu;

    void Awake()
    {
        Time.timeScale = 1f;
    }

    void Start() 
    {
        isOnMenu = true;

        /*
        if(playerInfo.playerCurrentRoom > 0)
        {
            continueButton.SetActive(true);
        }
        else if(playerInfo.playerCurrentRoom <= 0)
        {
            continueButton.SetActive(false);
        }
        */
    }

    public void StartGame()
    {
        if(isOnMenu)
        {
            if(!playerInfo.hasPlayedTutorial)
            {
                StraightToTutorial();
            }
            else
            {
                playerInfo.playerCurrentRoom = 1;
                playerInfo.healthFromLastRoom = 0;
                SceneManager.LoadScene("_Room_01", LoadSceneMode.Single);
                playerInfo.NewGameReset();
            }
        }
    }

    public void ContinueGame() // depois reativo o menino, falta inclusive ativar ele no start
    {
        /*
        if(playerInfo.playerCurrentRoom == 1)
        {
            SceneManager.LoadScene("_1_RoomScene", LoadSceneMode.Single);
        }
        else if(playerInfo.playerCurrentRoom == 2)
        {
            SceneManager.LoadScene("_2_RoomScene", LoadSceneMode.Single);
        }
        else if(playerInfo.playerCurrentRoom == 3)
        {
            SceneManager.LoadScene("_3_RoomScene", LoadSceneMode.Single);
        }
        else if(playerInfo.playerCurrentRoom == 4)
        {
            SceneManager.LoadScene("_4_RoomScene", LoadSceneMode.Single);
        }
        else if(playerInfo.playerCurrentRoom == 5)
        {
            SceneManager.LoadScene("_5_RoomScene", LoadSceneMode.Single);
        }
        */
    }
    
    public void OpenOptions()
    {
        if(isOnMenu)
        {
            optionsPanel.SetActive(true);
            isOnMenu = false;
        } 
    }
    public void OpenCredits()
    {
        if(isOnMenu)
        {
            creditsPanel.SetActive(true);
            isOnMenu = false;
        }
    }

    public void ReturnToMenu()
    {
        if(!isOnMenu)
        {
            CloseWindows();
        }
        
    }

    public void CloseWindows()
    {
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        isOnMenu = true;
    }

    public void QuitGame()
    {
        if(isOnMenu)
        {
            Application.Quit();
        }
    }

    public void StraightToTutorial()
    {
        playerInfo.TutorialReset();
        SceneManager.LoadScene("2_TutorialScene", LoadSceneMode.Single);
    }
}
