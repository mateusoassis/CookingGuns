using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpike : MonoBehaviour
{
    [SerializeField] private int trapDamage;
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.TryGetComponent(out _PlayerStats playerStats)))
        {
            playerStats.TakeHPDamage(trapDamage);

        }
    }
}
