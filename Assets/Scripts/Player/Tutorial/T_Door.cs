using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Door : MonoBehaviour
{
    public Transform doorTarget;
    public Transform door;
    public bool enemyKilled;
    public bool lootDrops;
    public bool doorInPlace;
    public float doorTime;
    public float currentLerpTime;
    public float perc;

    public void Start()
    {
        doorInPlace = false;
        enemyKilled = false;
    }

    public void Update()
    {
        if(doorInPlace)
        {
            return;
        }
        else if(!doorInPlace && enemyKilled)
        {
            currentLerpTime += Time.deltaTime;
            perc = currentLerpTime / doorTime;

            door.position = Vector3.Lerp(door.position, doorTarget.position, perc);
        }  
        if(door.position == doorTarget.position)
        {
            doorInPlace = true;
        }
    }
}
