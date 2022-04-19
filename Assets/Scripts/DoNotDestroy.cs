using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy : MonoBehaviour
{
    /*public static DoNotDestroy instance;


    void Awake(){
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }*/
    void Start(){
        this.gameObject.SetActive(false);
    }
}
