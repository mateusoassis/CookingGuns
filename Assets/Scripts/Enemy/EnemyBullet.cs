using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed;

    private Transform player;
    private Vector3 target;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        target = player.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, bulletSpeed * Time.fixedDeltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y && transform.position.z == target.z)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("player tomou dano do tiro");
            Destroy(this.gameObject);
        }
    }
}
