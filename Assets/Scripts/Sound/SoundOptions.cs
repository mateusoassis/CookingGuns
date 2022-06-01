using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundOptions : MonoBehaviour
{
    public SoundManager volManager;
    public Slider genVolSlider;
    public Slider fxVolSlider;

    public Slider sgVolSlider;
    void Start() {
        volManager = GameObject.Find("AudioManager").GetComponent<SoundManager>();

        //Ajusta os Sliders para ficarem no lugar certo
        genVolSlider.value = PlayerPrefs.GetFloat("genVol", 1f);
        sgVolSlider.value = PlayerPrefs.GetFloat("sgVol", 1f);
        fxVolSlider.value = PlayerPrefs.GetFloat("fxVol", 1f);
    }

    public void SetGeneralVol(){
        volManager.generalMultiplier = genVolSlider.value;
        PlayerPrefs.SetFloat("genVol", genVolSlider.value);
    }
    public void SetFXVol(){
        volManager.fxMultiplier = fxVolSlider.value;
        PlayerPrefs.SetFloat("fxVol", fxVolSlider.value);
    }

    public void SetSongVol(){
        volManager.songMultiplier = sgVolSlider.value;
        PlayerPrefs.SetFloat("sgVol", sgVolSlider.value);
    }
}
