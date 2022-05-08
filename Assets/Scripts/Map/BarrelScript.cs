using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    [SerializeField] private GameObject barrelExplosionArea;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBullet"|| other.gameObject.tag == "EnemyBullet"|| other.gameObject.tag == "PlayerGranade"|| other.gameObject.tag == "EnemyGranade")
        {
            transform.rotation = Quaternion.identity;
            barrelExplosionArea.SetActive(true);
            StartCoroutine("TimeToDestroy");
        }
    }
    
    private IEnumerator TimeToDestroy() 
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(this.gameObject);
    }   
}
