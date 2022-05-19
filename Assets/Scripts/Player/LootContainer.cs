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
        Debug.Log("criou item");
        //if(_WeaponHandler.FindAny(itemTypeOnSlot, n) != -1)
        //{
            Debug.Log("check");
            GameObject clone = Instantiate(lootPrefab, lootContainer.position, lootContainer.localRotation) as GameObject;
            clone.transform.SetParent(lootContainer, false);
            clone.transform.SetAsFirstSibling();
            

            indexForItem = _WeaponHandler.FindFirstFalseIndex(hasItemOnSlot);
            hasItemOnSlot[indexForItem] = true;
            itemTypeOnSlot[indexForItem] = n;
            itemQuantityOnSlot[indexForItem] = 1;

            clone.transform.GetComponent<LootPull>().quantity = 1;
            clone.transform.GetComponent<LootPull>().UpdateValuesAndTexture(indexForItem, n, durationUntilFadeout);
        //}
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerLoot")
        {
            if(other.gameObject.TryGetComponent(out EnemyDrop enemyDrop))
            {
                Debug.Log("colidiu com drop");
                CreateNewLoot(enemyDrop.itemType);
                Debug.Log("pegou lul " + enemyDrop.itemType);
                Destroy(other.gameObject);
            }
            
        }
    }
}
