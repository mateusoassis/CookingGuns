using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallExplosionSoundOnEnable : MonoBehaviour
{
    void OnEnable()
    {
        FindObjectOfType<SoundManager>().PlayOneShot("ExplosionSound");
    }
}
