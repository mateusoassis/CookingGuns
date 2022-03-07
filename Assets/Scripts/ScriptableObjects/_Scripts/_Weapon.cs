using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_Weapon", menuName = "Cooking Guns/Weapon", order = 3)]
public class _Item : ScriptableObject
{
    public string itemName;
    public string description;
    public int damage;
}


