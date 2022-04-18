using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeAreaDamage : MonoBehaviour
{
    public float bombTimer;
    public GameObject particleEffect;
    public GameObject granade;
    public int damageDone;
    public MeshCollider meshCollider;

    void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
        meshCollider.enabled = false;
        transform.rotation = Quaternion.identity;
    }

    void Start()
    {
        StartCoroutine("BombTimer");
    }

    public IEnumerator BombTimer()
    {
        yield return new WaitForSeconds(bombTimer);
        particleEffect.SetActive(true);
        meshCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
        Destroy(granade);
    }
}
