using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootContainer : MonoBehaviour
{
    [Header("Array com texturas de loot")]
    public Sprite[] dropIcons;
    public int indexForItem;

    public bool[] hasItemOnSlot;
    public int[] itemTypeOnSlot;
    public int[] itemQuantityOnSlot;

    public GameObject lootPrefab;
    public Transform lootContainer;

    public float durationUntilFadeout;

    public void CreateNewLoot(int n)
    {
        if(_WeaponHandler.FindAny(itemTypeOnSlot, n) != -1)
        {
            GameObject clone = Instantiate(lootPrefab, lootContainer.position, Quaternion.identity);
            clone.transform.SetParent(lootContainer);
            clone.transform.SetAsFirstSibling();
            

            indexForItem = _WeaponHandler.FindFirstFalseIndex(hasItemOnSlot);
            hasItemOnSlot[indexForItem] = true;
            itemTypeOnSlot[indexForItem] = n;
            itemQuantityOnSlot[indexForItem] = 1;

            clone.transform.GetComponent<LootPull>().quantity = 1;
            clone.transform.GetComponent<LootPull>().UpdateValuesAndTexture(indexForItem, n, durationUntilFadeout);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerLoot")
        {
            CreateNewLoot(other.gameObject.GetComponent<EnemyDrop>().itemType);
        }
    }
}
