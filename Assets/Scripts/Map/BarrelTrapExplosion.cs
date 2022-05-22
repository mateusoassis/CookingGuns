using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelTrapExplosion : MonoBehaviour
{
    private GranadeInnerArea granadeInnerAreaScript;

    public float barrelExplosionTimer;
    public GameObject particleEffect;
    public int damageDoneInPlayer;
    public int damageDoneInEnemy;
    public CapsuleCollider capsuleCollider;

    void Awake()
    {
        granadeInnerAreaScript = GetComponentInChildren<GranadeInnerArea>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        capsuleCollider.enabled = false;
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        transform.Rotate(0, 2f, 0 * Time.deltaTime);
    }

    void Start()
    {
        StartCoroutine("BombTimer");
    }

    public IEnumerator BombTimer()
    {
        yield return new WaitForSeconds(barrelExplosionTimer);
        PlayExplosionParticle();
        granadeInnerAreaScript.isMaxSize = true;
        granadeInnerAreaScript.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        FindObjectOfType<SoundManager>().PlayOneShot("ExplosionSound");
        capsuleCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }

    void PlayExplosionParticle()
    {
        Instantiate(particleEffect, transform.position, Quaternion.identity);
    }
}
