using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingMainScript : MonoBehaviour
{   

    public Inventory inventory;
    //public List<Scriptable Object> Recipes;
    
    public List<string> plo;

    int inventorySize;
    void Start(){
        inventory = GameObject.Find("InventoryHandler").GetComponent<Inventory>();
        inventorySize = inventory.InventSize;
    }
    
    public bool CanCraft(){
        /*for(int i = 0; i < inventorySize; i++){

        }*/
        return false;
    }
    public void Craft(){
        
    }


}
