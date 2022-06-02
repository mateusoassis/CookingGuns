using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWaveCounter : MonoBehaviour
{
    [SerializeField] private RectTransform positionLeft;
    [SerializeField] private RectTransform positionMiddle;
    [SerializeField] private RectTransform positionRight;

    //[SerializeField] private float textMoveSpeed;
    [SerializeField] private float moveDuration;

    [SerializeField] private float elapsedTime;
    [SerializeField] private float percentageComplete;
    [SerializeField] private bool next;
    [SerializeField] private int index;

    private void Awake()
    {
        positionLeft = GameObject.Find("WaveCounterLeft").GetComponent<RectTransform>();
        positionMiddle = GameObject.Find("WaveCounterMiddle").GetComponent<RectTransform>();
        positionRight = GameObject.Find("WaveCounterRight").GetComponent<RectTransform>();
        transform.position = positionLeft.position;
    }

    private void Update()
    {
        if(next)
        {
            elapsedTime += Time.deltaTime;
            percentageComplete = elapsedTime / moveDuration;
            if(index == 0)
            {
                GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, positionMiddle.anchoredPosition, percentageComplete);
            }
            else if(index == 1)
            {
                GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(GetComponent<RectTransform>().anchoredPosition, positionRight.anchoredPosition, percentageComplete);
            }

            if(GetComponent<RectTransform>().anchoredPosition == positionMiddle.anchoredPosition)
            {
                index = 1;
                next = false;
                StartCoroutine(ShowWaveNumber());
            }
            /*
            else if(GetComponent<RectTransform>().anchoredPosition == positionRight.anchoredPosition)
            {
                BackToStartPos();
            }
            */
        }  
    }

    public void BackToStartPos()
    {
        GetComponent<RectTransform>().anchoredPosition = positionLeft.anchoredPosition;
        index = 0;
        next = false;
    }

    public void TriggerMovement() 
    {
        elapsedTime = 0f;
        next = true;
        //StartCoroutine("ShowWaveNumber");
    }
    
    public IEnumerator ShowWaveNumber() 
    {
        //GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, positionMiddle.anchoredPosition, percentageComplete);
        yield return new WaitForSeconds(2.0f);
        TriggerMovement();
        yield return new WaitForSeconds(2.0f);
        BackToStartPos();
        //GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(GetComponent<RectTransform>().anchoredPosition, positionRight.anchoredPosition, percentageComplete);
        //yield return new WaitForSeconds(0.5f);
        //transform.position = positionLeft.position;
    }
}
