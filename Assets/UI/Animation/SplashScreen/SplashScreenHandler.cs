using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SplashScreenHandler : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float animatorSpeedMultiplier;
    [SerializeField] private TextMeshProUGUI multiplierText;
    private CreditsBugController credits;

    void Awake(){
        if(SceneManager.GetActiveScene().buildIndex == 0){
            PlayerPrefs.DeleteAll();
        }

    }
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            credits = GameObject.Find("5_Bugs").GetComponent<CreditsBugController>();
        }
        multiplierText.text = ("Hold SPACE to " + animatorSpeedMultiplier + "x speed");
        anim = GetComponent<Animator>();
    }

    public void ToMenu()
    {
        FindObjectOfType<SoundManager>().Play("Menu Music");
        SceneManager.LoadScene("1_MenuScene", LoadSceneMode.Single);
        
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            anim.speed = animatorSpeedMultiplier;
        }
        else
        {
            anim.speed = 1;
        }

        if(Input.GetKey(KeyCode.Escape) && credits != null)
        {
            ToMenu();
        }
    }

    public void StartVids()
    {
        credits.StartVideoLoop();
    }
}
