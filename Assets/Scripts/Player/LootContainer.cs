using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootContainer : MonoBehaviour
{
    public PlayerInfo playerInfo;
    [Header("Array com texturas de loot")]
    public Sprite[] dropIcons;
    public int indexForItem;
    public Transform[] createdLoot;

    public bool[] hasItemOnSlot;
    public int[] itemTypeOnSlot;
    public int[] itemQuantityOnSlot;
    public Transform[] lootTransforms;

    public GameObject lootPrefab;
    public Transform lootContainer;

    public float durationUntilFadeout;

    public void CreateNewLoot(int n)
    {
        Debug.Log("criou item");
        Debug.Log(_WeaponHandler.FindAny(itemTypeOnSlot,n));
        if(_WeaponHandler.FindAny(itemTypeOnSlot, n) != -1)
        {
            Debug.Log("check");
            //GameObject clone = Instantiate(lootPrefab, lootContainer.position, lootContainer.localRotation) as GameObject;
            //clone.transform.SetParent(lootContainer, false);
            //clone.transform.SetAsFirstSibling();
            

            indexForItem = _WeaponHandler.FindAny(itemTypeOnSlot, n);
            //hasItemOnSlot[indexForItem] = true;
            //itemTypeOnSlot[indexForItem] = n;
            itemQuantityOnSlot[indexForItem]++;
            if(!createdLoot[indexForItem].GetComponent<LootPull>().vanishing)
            {
                createdLoot[indexForItem].GetComponent<LootPull>().DurationRefresh();
            }
            else
            {
                Debug.Log("check else");
                GameObject clone = Instantiate(lootPrefab, lootContainer.position, lootContainer.localRotation) as GameObject;
                clone.transform.SetParent(lootContainer, false);
                clone.transform.SetAsFirstSibling();

                indexForItem = _WeaponHandler.FindFirstFalseIndex(hasItemOnSlot);
                hasItemOnSlot[indexForItem] = true;
                itemTypeOnSlot[indexForItem] = n;
                itemQuantityOnSlot[indexForItem] = 1;
                createdLoot[indexForItem] = clone.transform;

                clone.transform.GetComponent<LootPull>().quantity = 1;
                clone.transform.GetComponent<LootPull>().UpdateValuesAndTexture(indexForItem, n, durationUntilFadeout);
            }
            
        }
        else
        {
            Debug.Log("else");
            GameObject clone = Instantiate(lootPrefab, lootContainer.position, lootContainer.localRotation) as GameObject;
            clone.transform.SetParent(lootContainer, false);
            clone.transform.SetAsFirstSibling();

            indexForItem = _WeaponHandler.FindFirstFalseIndex(hasItemOnSlot);
            hasItemOnSlot[indexForItem] = true;
            itemTypeOnSlot[indexForItem] = n;
            itemQuantityOnSlot[indexForItem] = 1;
            createdLoot[indexForItem] = clone.transform;

            clone.transform.GetComponent<LootPull>().quantity = 1;
            clone.transform.GetComponent<LootPull>().UpdateValuesAndTexture(indexForItem, n, durationUntilFadeout);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerLoot")
        {
            if(other.gameObject.TryGetComponent(out EnemyDrop enemyDrop))
            {
                FindObjectOfType<SoundManager>().PlayOneShot("LootPickUp2");
                Debug.Log("colidiu com drop");
                CreateNewLoot(enemyDrop.itemType);
                Debug.Log("pegou lul " + enemyDrop.itemType);
                enemyDrop.AddIngredient();
                Destroy(other.gameObject);
            }
        }
    }

    public void DeleteThisIconFromEverything(int index)
    {
        hasItemOnSlot[index] = false;
        itemTypeOnSlot[index] = -1;
        itemQuantityOnSlot[index] = 0;
        createdLoot[index] = null;
    }
}
