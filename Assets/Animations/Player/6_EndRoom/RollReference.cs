using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollReference : MonoBehaviour
{
    public RollAuxiliary rollAux;

    public void StopRollAnimation()
    {
        rollAux.DeactivateThisObject();
    }
}
