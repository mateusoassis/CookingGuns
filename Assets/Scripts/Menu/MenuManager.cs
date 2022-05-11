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

        if(playerInfo.playerCurrentRoom > 0)
        {
            continueButton.SetActive(true);
        }
        else if(playerInfo.playerCurrentRoom <= 0)
        {
            continueButton.SetActive(false);
        }
    }

    public void StartGame()
    {
        if(isOnMenu)
        {
            if(!playerInfo.hasPlayedTutorial)
            {
                playerInfo.hasPlayedTutorial = true;
                SceneManager.LoadScene("0_1_Tutorial", LoadSceneMode.Single);
                playerInfo.playerCurrentRoom = 0;
                playerInfo.healthFromLastRoom = 0;
                playerInfo.NewGameReset();
            }
            else
            {
                playerInfo.playerCurrentRoom = 1;
                playerInfo.healthFromLastRoom = 0;
                SceneManager.LoadScene("_1_RoomScene", LoadSceneMode.Single);
                playerInfo.NewGameReset();
            }
        }
    }

    public void ContinueGame()
    {
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
        SceneManager.LoadScene("New_Tutorial_RoomScene", LoadSceneMode.Additive);
    }
}
