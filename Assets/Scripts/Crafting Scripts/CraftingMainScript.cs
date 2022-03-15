using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingMainScript : MonoBehaviour
{   

    public Inventory inventory;
    public List<ScriptableObject> Recipe;
    void Start(){
        inventory = GameObject.Find("Inventory Manager").GetComponent<Inventory>();

    }

    public bool CanCraft(){
        return false;
    }
    public void Craft(){
        
    }


}
