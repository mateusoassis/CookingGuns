using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpike : MonoBehaviour
{
    [SerializeField] private int trapDamagePlayer;
    [SerializeField] private int trapDamageEnemy;
    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.TryGetComponent(out _PlayerStats playerStats)))
        {
            playerStats.TakeHPDamage(trapDamagePlayer);
        }

        if ((other.gameObject.TryGetComponent(out EnemyStats enemyStats)))
        {
            enemyStats.TakeDamage(trapDamageEnemy);
        }
    }
}
