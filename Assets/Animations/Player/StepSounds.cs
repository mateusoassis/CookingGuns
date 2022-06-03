using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSounds : MonoBehaviour
{
    public void StepSoundNow()
    {
        FindObjectOfType<SoundManager>().PlayOneShot("StepRevise");
    }
}
