using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTimeRoomManager : MonoBehaviour
{
    private TimeSurvivalRoom timeSurvivalRoomScript;
    private float timeToStartRoom;
    private void Awake()
    {
        timeSurvivalRoomScript = GameObject.Find("WaveManagerTimer").GetComponent<TimeSurvivalRoom>();
        timeToStartRoom = 4.0f;
    }

    private IEnumerator Start()
    {
        if (timeSurvivalRoomScript.elapsedTime >= 60.0f) 
        { 
            yield return new WaitForSeconds(timeToStartRoom);
        }
        StartCoroutine("CheckSpawn");
    }
    private IEnumerator CheckSpawn() 
    {
        yield return new WaitForSeconds(timeSurvivalRoomScript.timeBetweenWaves);
        timeSurvivalRoomScript.StartCoroutine("ControlSpawn");
    }
}
