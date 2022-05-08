using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void BackToMenuAfterCredits()
    {
        Debug.Log("osh");
        SceneManager.LoadScene("_0_MenuScene", LoadSceneMode.Single);
    }
}
