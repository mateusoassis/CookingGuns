using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CraftableItem", menuName = "Cooking Guns/CraftableItem", order = 1)]
public class CraftableItem : ScriptableObject
{
    public string[]  Recipe = new string[4];
    public string Name;
    public Sprite Img;


}
