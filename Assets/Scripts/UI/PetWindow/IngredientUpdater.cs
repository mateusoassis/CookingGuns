using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientUpdater : MonoBehaviour
{
    [Header("Fill it out")]
    [Tooltip("Name of the gun")]
    [SerializeField] private string weaponName;
    [Tooltip("Type of the gun based on WeaponHandler")]
    [SerializeField] private int weaponType;
    [Tooltip("How many different ingredients does it use")]
    [SerializeField] private int differentIngredients;
    [Tooltip("What are the types of ingredients based on PlayerInfo")]
    [SerializeField] private int[] typeOfIngredients;
    [Tooltip("How many ingredients does it use")]
    [SerializeField] private int[] amountOfIngredients;
    

    [Header("Hide and Show texts")]
    [SerializeField] private GameObject[] textContainers;
    [SerializeField] private TextMeshProUGUI[] textOwnAmount;
    [SerializeField] private TextMeshProUGUI[] textAmountRequired;

    [Tooltip("Don't even think about messing with this one")]
    [SerializeField] private PlayerInfo playerInfo;

    void Awake()
    {
        UpdateIngredientAmount();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    private void UpdateIngredientAmount()
    {
        for(int i = 0; i < textContainers.Length; i++)
        {
            if(i < differentIngredients)
            {
                textContainers[i].SetActive(true);
            }
            else
            {
                textContainers[i].SetActive(false);
            }
        }
        UpdateAmountText();
        UpdateRequiredText();
    }

    private void UpdateAmountText()
    {
        for(int i = 0; i < typeOfIngredients.Length; i++)
        {
            textOwnAmount[i].text = playerInfo.ingredientes[typeOfIngredients[i]].ToString();
        }
    }
    private void UpdateRequiredText()
    {
        for(int i = 0; i < typeOfIngredients.Length; i++)
        {
            textAmountRequired[i].text = "/ " + amountOfIngredients[typeOfIngredients[i]].ToString();
        }
    }
}
