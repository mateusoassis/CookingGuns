using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingMainScript : MonoBehaviour
{   

    public List<CraftableItem> itensList;

    private _Item currentItem;
    public Image customCursor;

    void OnMouseDown(_Item item){
        if(currentItem == null){
            currentItem = item;
        }
    }
}
