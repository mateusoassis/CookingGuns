using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWaveCounter : MonoBehaviour
{
    [SerializeField] private Transform positionLeft;
    [SerializeField] private Transform positionMiddle;
    [SerializeField] private Transform positionRight;

    //[SerializeField] private float textMoveSpeed;
    [SerializeField] private float moveDuration;

    private float elapsedTime;

    private void Awake()
    {
        positionLeft = GameObject.Find("WaveCounterLeft").GetComponent<Transform>();
        positionMiddle = GameObject.Find("WaveCounterMiddle").GetComponent<Transform>();
        positionRight = GameObject.Find("WaveCounterRight").GetComponent<Transform>();
        transform.position = positionLeft.position;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
    }

    public void TriggerMovement() 
    {
        StartCoroutine("ShowWaveNumber");
    }
    
    public IEnumerator ShowWaveNumber() 
    {
        float percentageComplete = elapsedTime / moveDuration;
        transform.position = Vector3.Lerp(transform.position, positionMiddle.position, percentageComplete);
        yield return new WaitForSeconds(2.0f);
        transform.position = Vector3.Lerp(transform.position, positionRight.position, percentageComplete);
        yield return new WaitForSeconds(0.5f);
        transform.position = positionLeft.position;
    }
}
