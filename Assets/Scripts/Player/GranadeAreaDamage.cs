using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeAreaDamage : MonoBehaviour
{
    [SerializeField]private float bombTimer;
    public GranadeInnerArea granadeInnerAreaScript;
    public GameObject particleEffect;
    public GameObject particleEffect2;
    public GameObject granade;
    //public GameObject innerArea;
    public int damageDone;
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
        transform.Rotate(0, 1f, 0 * Time.deltaTime);
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
        FindObjectOfType<SoundManager>().PlayOneShot("ExplosionSound");
        capsuleCollider.enabled = true;
        granadeInnerAreaScript.isMaxSize = true;
        granadeInnerAreaScript.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
        Destroy(granade);
    }
}
