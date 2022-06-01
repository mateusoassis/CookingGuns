using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{


    public void PlayMenuMusic(){
        FindObjectOfType<SoundManager>().Play("Menu Music");
        
        FindObjectOfType<SoundManager>().StopSound("Game Music");
        
    }

    public void PlayGameMusic(){
        FindObjectOfType<SoundManager>().Play("Game Music");
        
        FindObjectOfType<SoundManager>().StopSound("Menu Music");
        
    }
}
