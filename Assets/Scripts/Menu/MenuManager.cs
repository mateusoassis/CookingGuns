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
    public Button continueButton;

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
            continueButton.interactable = true;
        }
        else if(playerInfo.playerCurrentRoom <= 0)
        {
            continueButton.interactable = false;
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
            }
            else
            {
                SceneManager.LoadScene("4_RoomScene", LoadSceneMode.Single);
                playerInfo.playerCurrentRoom = 1;
                playerInfo.healthFromLastRoom = 0;
            }
        }
    }

    public void ContinueGame()
    {
        if(playerInfo.playerCurrentRoom == 1)
        {
            SceneManager.LoadScene("4_RoomScene", LoadSceneMode.Single);
        }
        else if(playerInfo.playerCurrentRoom == 2)
        {
            SceneManager.LoadScene("5_RoomScene", LoadSceneMode.Single);
        }
        else if(playerInfo.playerCurrentRoom == 3)
        {
            SceneManager.LoadScene("3_RoomScene", LoadSceneMode.Single);
        }
        else if(playerInfo.playerCurrentRoom == 4)
        {
            SceneManager.LoadScene("2_RoomScene", LoadSceneMode.Single);
        }
        else if(playerInfo.playerCurrentRoom == 5)
        {
            SceneManager.LoadScene("1_RoomScene", LoadSceneMode.Single);
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
}
