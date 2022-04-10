using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
/*public class Ingredients{
    public List<string> ingredients;
    [Range(1, 4)]
    public List<int> quantity;

}*/
[CreateAssetMenu(fileName = "CraftableItem", menuName = "Cooking Guns/CraftableItem", order = 1)]
public class CraftableItem : ScriptableObject
{
    public List<string> Recipe;   
    public string Result;
    //public GameObject ResultObj;
    //public Image Icon;

}
