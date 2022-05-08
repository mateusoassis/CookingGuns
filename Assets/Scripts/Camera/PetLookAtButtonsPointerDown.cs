using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PetLookAtButtonsPointerDown : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler 
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log("entrando em " + gameObject.name);
    }
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Debug.Log("saindo de " + gameObject.name);
    }

    public void OnMouseOver()
    {
        Debug.Log("mouseover " + gameObject.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("mouseover " + gameObject.name);
    }
}
