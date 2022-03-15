using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    public int damageDone;
    [SerializeField] private GameObject hitParticle;
    
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy" && playerController.isAttacking /*&& !isHit*/)
        {
            Debug.Log("acertou inimigo de nome " + other.name + " e deu " + damageDone + " de dano");
            //isHit = true;
            //Instantiate(hitParticle, new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z), other.transform.rotation);
        }
    }
}
