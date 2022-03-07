using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_PlayerInventory", menuName = "Cooking Guns/PlayerInventory", order = 1)]
public class _PlayerInventory : ScriptableObject
{
    public int inventorySlot;
    public int itemId;
    public int amount;
}
