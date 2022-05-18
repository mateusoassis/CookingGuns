using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTimeRoomManager : MonoBehaviour
{
    private TimeSurvivalRoom timeSurvivalRoomScript;
    private void Awake()
    {
        timeSurvivalRoomScript = GameObject.Find("WaveManagerTimer").GetComponent<TimeSurvivalRoom>();
    }

    private void Start()
    {
        StartCoroutine("CheckSpawn");
    }
    private IEnumerator CheckSpawn() 
    {
        yield return new WaitForSeconds(timeSurvivalRoomScript.timeBetweenWaves);
        timeSurvivalRoomScript.StartCoroutine("ControlSpawn");
    }
}
