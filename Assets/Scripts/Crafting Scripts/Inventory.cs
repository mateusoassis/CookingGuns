using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{

    public List<BaseItem> Invent;

    public int InventSize;

    //public BaseItem temp;

    public List<TextMeshProUGUI> InventoryQuantityText;

    void Update(){
        InventSize = Invent.Count;
    }
    
    public int GetItem(string name){
        for(int i = 0; i < InventSize; i++){
                 if(name == Invent[i].Name){
                 return Invent[i].Quantity; 
                }
        }
        return 0;
    }

    public void AddItem(string item){

        

        for(int i = 0; i < InventSize; i++){
            if (item == Invent[i].Name){

                Invent[i].Quantity++;
                Debug.Log("Você tem " + Invent[i].Quantity + " " + Invent[i].Name);
                UpdateItem();
                return;
            } /*else if (i == InventSize - 1){
                BaseItem Adder = temp;
                Adder.Name = item;
                Adder.Quantity = 1;
                Debug.Log("Você tem problemas cara");
                Invent.Add(Adder);
            }*/
        }

    }

    public void RemoveItem(string item){
        for(int i = 0; i < InventSize; i++){

                if(item == Invent[i].Name){

                    if(Invent[i].Quantity == 0){
                        Debug.Log("Já não tem mais boy, calma ae");

                        //Invent.Remove(Invent[i]);

                    } else if (Invent[i].Quantity > 0) {

                        Invent[i].Quantity--;
                        UpdateItem();

                    }
                }
                     
                
        }

    }
    
    public void UpdateItem(){
        for(int i = 0; i < InventSize; i++){
            InventoryQuantityText[i].text = Invent[i].Quantity.ToString() + " x " + Invent[i].Name;
        }
    }

}