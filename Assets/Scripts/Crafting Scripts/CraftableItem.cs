using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemAmount{
    public GameObject item;
    [Range(1, 4)]
    public int amount;
}



[CreateAssetMenu(fileName = "CraftableItem", menuName = "Cooking Guns/CraftableItem", order = 1)]
public class CraftableItem : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;

    public bool CanCraft(){
        return false;
    }
    public void Craft(){
        
    }

}
