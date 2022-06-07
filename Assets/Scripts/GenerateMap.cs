using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateMap : MonoBehaviour
{
    public bool test;

    public GameManager gameManager;
    public _PlayerStats playerStats;
    public GameObject mainUI;

    public bool healEveryRoom;

    public int numberOfRoomsToEndGame;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainUI = GameObject.Find("MainUI");
    }

    void RoomSelector()
    {   
        if(!playerStats.playerManager.testingCredits)
        {
            NextRoom();
        }
        else
        {
            FindObjectOfType<MusicPlayer>().PlayMenuMusic();
            gameManager.CreditsScene();
        }
    }    

    public void TestRoomSelector()
    {
        SceneManager.LoadScene("1_RoomScene");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Door" && gameManager.roomCleared == true)
        {            
            Debug.Log("porta");
            RoomSelector();
        }

        if(other.gameObject.tag == "T_Door" && !gameManager.playerManager.isRolling)
        {   
            LoadInitialGame();
        }
    }

    public void LoadInitialGame()
    {
        GetComponent<_PlayerManager>().playerInfo.endedTutorial = true;
        GetComponent<_PlayerManager>().playerInfo.hasPlayedTutorial = true;
        SceneManager.LoadScene("_Room01", LoadSceneMode.Single);
        GetComponent<_PlayerManager>().playerInfo.isOnTutorial = false;
        GetComponent<_PlayerManager>().playerInfo.NewGameReset();
    }

    public void NextRoom() // literalmente vai pra próxima sala a partir do build index
    {
        if(GetComponent<_PlayerManager>().playerInfo.playerCurrentRoom < numberOfRoomsToEndGame)
        {
            int buildIndex = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(buildIndex + 1);
            SceneManager.LoadScene(buildIndex + 1);
            GetComponent<_PlayerManager>().playerInfo.playerCurrentRoom++;
        }
        else
        {
            FindObjectOfType<MusicPlayer>().PlayCreditsMusic();
            SceneManager.LoadScene("2_CreditsScene", LoadSceneMode.Single);
        }
    }
}
