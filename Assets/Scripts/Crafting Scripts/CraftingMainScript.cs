using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingMainScript : MonoBehaviour
{   

    public Inventory inventory;
    //public List<Scriptable Object> Recipes;
    
    public List<Recipes> recipess;
    public List<string> MadeWeapons;

    int inventorySize;
    public List<int> recipeSize;
    void Start(){
        inventory = GameObject.Find("InventoryHandler").GetComponent<Inventory>();
        inventorySize = inventory.InventSize;
        for(int i = 0; i < recipess.Count; i++){
            recipeSize[i] = recipess[i].Ingredients.Count;
        }
    }
    
    public bool CanCraft(string position){
        
        for(int j = 0; j < recipess.Count; j++){
            int craftCounter = 0;
            int index = 0;
            if(position != recipess[j].Result){
                index++;
            } else {
                for(int i = 0; i <recipeSize[index]; i++){
                if(inventory.GetItem(recipess[index].Ingredients[i]) >= 1){
                    craftCounter++;
                    if(craftCounter == recipeSize[index]){
                        return true;
                    }
                   
                }
            }
            
            }
        }        
        return false;
    }
    public void Craft(string Item){
        if(CanCraft(Item)){
            for(int j = 0; j < recipess.Count; j++){
                if(Item == recipess[j].Result) {
                    for(int i = 0; i < recipeSize[j]; i++){
                        inventory.RemoveItem(recipess[j].Ingredients[i]);
                    }

                }
            }

            MadeWeapons.Add(Item);
        } else {
            Debug.Log("Não pode Craftar!");
        }
    }


}
