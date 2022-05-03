using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenHandler : MonoBehaviour
{
    public void ToMenu()
    {
        SceneManager.LoadScene("0_MenuScene", LoadSceneMode.Single);
    }
}
