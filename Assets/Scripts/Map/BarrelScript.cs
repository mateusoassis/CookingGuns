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
            ActivateBarrel();
        }
    }
    
    private IEnumerator TimeToDestroy() 
    {
        yield return new WaitForSeconds(1.6f);

        Destroy(this.gameObject);
    }   

    public void ActivateBarrel()
    {
        barrelExplosionArea.SetActive(true);
        StartCoroutine("TimeToDestroy");
    }
}
