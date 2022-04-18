using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStartKeys : MonoBehaviour
{
    public BoxCollider box;

    void OnDisable()
    {
        box.enabled = true;
    }
}
