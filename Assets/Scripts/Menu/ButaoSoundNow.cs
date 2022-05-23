using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButaoSoundNow : MonoBehaviour
{
    public void ButaoScriptSound()
    {
        FindObjectOfType<SoundManager>().PlayOneShot("Butao");
    }
}
