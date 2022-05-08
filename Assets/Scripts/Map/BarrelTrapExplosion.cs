using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelTrapExplosion : MonoBehaviour
{
    public float barrelExplosionTimer;
    public GameObject particleEffect;
    public int damageDoneInPlayer;
    public int damageDoneInEnemy;
    public CapsuleCollider capsuleCollider;

    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.enabled = false;
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        transform.Rotate(0, 0.2f, 0 * Time.deltaTime);
    }

    void Start()
    {
        StartCoroutine("BombTimer");
    }

    public IEnumerator BombTimer()
    {
        yield return new WaitForSeconds(barrelExplosionTimer);
        PlayExplosionParticle();
        capsuleCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }

    void PlayExplosionParticle()
    {
        Instantiate(particleEffect, transform.position, Quaternion.identity);
    }
}
