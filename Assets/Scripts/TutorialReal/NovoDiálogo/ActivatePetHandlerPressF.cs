using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePetHandlerPressF : MonoBehaviour
{
    public PetHandler petHandler;

    void Awake()
    {
        petHandler = GameObject.Find("Player").GetComponent<PetHandler>();
    }

    void OnDestroy()
    {
        petHandler.pressFKey.SetActive(true);
    }
}
