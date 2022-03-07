using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Cooking Guns/PlayerInventory", order = 1)]
public class _PlayerInventory : ScriptableObject
{
    public int[] itemId;
    public int[] amount;

    // currency 1 itemId = 1
    // currency 2 itemId = 2
    // currency 3 itemId = 3
}
