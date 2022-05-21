using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDoorToOpenOnKill : MonoBehaviour
{
    public OpenDoor openDoor;

    void Start()
    {
        openDoor.blockedOpen = true;
    }

    void OnDestroy()
    {
        openDoor.blockedOpen = false;
    }
}
