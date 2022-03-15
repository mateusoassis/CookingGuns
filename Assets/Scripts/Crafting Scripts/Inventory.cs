using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseItem: ScriptableObject {
        public string Name;
        public GameObject Item;
        public int Quantity;
}
public class Inventory : MonoBehaviour
{

    public List<BaseItem> Invent;

    public int InventSize;

    void Update(){
        InventSize = Invent.Count;
    }
    
    public bool GetItem(string name){
        for(int i = 0; i < InventSize; i++){
                 if(name == Invent[i].Name){
                 return true; 
                }
        }
        return false;
    }

    public void AddItem(BaseItem item){
        for(int i = 0; i < InventSize; i++){
            BaseItem temp = Invent[i];
            temp.Quantity++;
                if(item.Name == Invent[i].Name){
                    Invent[i] = temp;
                } else {
                    Invent.Add(item);
                }
        }

    }

    public void RemoveItem(BaseItem item){
        for(int i = 0; i < InventSize; i++){
            BaseItem temp = Invent[i];
            temp.Quantity--;
                if(item.Name == Invent[i].Name){
                    Invent[i] = temp;
                }
        }

    }

}

