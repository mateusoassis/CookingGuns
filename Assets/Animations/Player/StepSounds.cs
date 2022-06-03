using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StepSounds : MonoBehaviour
{
    public void StepSoundNow()
    {
        if(SceneManager.GetActiveScene().buildIndex != 1)
        {
            FindObjectOfType<SoundManager>().PlayOneShot("StepRevise");
        }
    }
}
