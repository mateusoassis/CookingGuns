using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeAreaDamage : MonoBehaviour
{
    public float bombTimer;
    public GameObject particleEffect;
    public GameObject particleEffect2;
    public GameObject granade;
    public int damageDone;
    public CapsuleCollider capsuleCollider;

    void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.enabled = false;
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        transform.Rotate(0, 20, 0 * Time.deltaTime);
    }

    void Start()
    {
        StartCoroutine("BombTimer");
    }

    public IEnumerator BombTimer()
    {
        yield return new WaitForSeconds(bombTimer);
        Instantiate(particleEffect, transform.position, Quaternion.identity);
        Instantiate(particleEffect2, transform.position, Quaternion.identity);
        capsuleCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
        Destroy(granade);
    }
}
