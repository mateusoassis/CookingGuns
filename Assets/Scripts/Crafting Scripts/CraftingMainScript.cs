using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class CraftingMainScript : MonoBehaviour
{   

    //public static CraftingMainScript instance;
    public Inventory inventory;
    //public List<Scriptable Object> Recipes;
    
    public List<Recipes> recipes;
    public List<Button> RecipesButton;
    public List<TextMeshProUGUI> RecipesTexts;
    public List<string> MadeWeapons;
    public _WeaponHandler weaponHandler;
    bool showedWeapons;

    int inventorySize;
    public List<int> recipeSize;

    /*void Awake(){
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }*/
    void Start(){
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        weaponHandler = GameObject.Find("Player").GetComponent<_WeaponHandler>(); 
        inventorySize = inventory.InventSize;
        for(int i = 0; i < recipes.Count; i++){
            recipeSize[i] = recipes[i].Ingredients.Count;
        }
    }


    
    public bool CanCraft(string position){
        int index = 0;
        for(int j = 0; j < recipes.Count; j++){
            int craftCounter = 0;
            if(position != recipes[j].Result){
                index++;
            } else {
                for(int i = 0; i <recipeSize[index]; i++){
                    if(inventory.GetItem(recipes[index].Ingredients[i]) >= 1){
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
            for(int j = 0; j < recipes.Count; j++){
                if(Item == recipes[j].Result) {
                    for(int i = 0; i < recipeSize[j]; i++){
                        inventory.RemoveItem(recipes[j].Ingredients[i]);
                    }

                }
            }
            int index = 0;
            for(int i = 0; i < recipes.Count; i++){
                if(Item != recipes[i].Result){
                    index++;
                } else {

                }
            }
            weaponHandler.unlockedWeapons[index] = true;
        } else {
            Debug.Log("Não pode Craftar!");
        }
    }

    public void ShowCraftOptions(){
        for(int i = 0; i < recipes.Count; i++){
            RecipesButton[i].interactable = CanCraft(recipes[i].Result);
        }
    }

    public void ShowRecipeIngredients(){
        if(!showedWeapons){
            for(int i = 0; i < recipes.Count; i++){
                for(int j = 0; j < recipeSize[i]; j++){
                    RecipesTexts[i].text = RecipesTexts[i].text +"<br>"+ recipes[i].Ingredients[j];
                }
            }
            showedWeapons = true;
        }
        
    }
}
