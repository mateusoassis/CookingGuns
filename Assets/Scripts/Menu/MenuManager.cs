using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public PlayerInfo playerInfo;

    public bool isOnMenu;

    void Start() 
    {
        isOnMenu = true;
    }

    public void StartGame()
    {
        if(isOnMenu)
        {
            if(!playerInfo.hasPlayedTutorial)
            {
                SceneManager.LoadScene("_TutorialScene", LoadSceneMode.Single);
            }
            else
            {
                SceneManager.LoadScene("1_RoomScene", LoadSceneMode.Single);
            }
            
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
