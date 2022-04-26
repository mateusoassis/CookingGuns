using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed;
    public int damageOnPlayer;

    private Transform player;
    [SerializeField] private Vector3 target;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        target = player.position;
        target.y = transform.position.y;
        Destroy(this.gameObject, 3f);
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
            if((other.gameObject.TryGetComponent(out _PlayerStats playerStats)))
            {
                playerStats.TakeHPDamage(damageOnPlayer);
            }
            Destroy(this.gameObject);
        }

        if(other.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }
}
