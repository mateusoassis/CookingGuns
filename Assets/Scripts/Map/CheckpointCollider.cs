using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCollider : MonoBehaviour
{
    public Transform checkpoint;
    public int damage;

    public GameObject outOfBoundsFadeObject;
    
    public float outOfBoundsDuration;
    public float outOfBoundsTimer;
    private _PlayerStats playerStatsReal;
    public GameManager gameManager;

    void Awake()
    {
        outOfBoundsFadeObject = GameObject.Find("OutOfBoundsFade");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        outOfBoundsFadeObject.SetActive(false);
        outOfBoundsTimer = outOfBoundsDuration;
    }

    void Update()
    {
        if(gameManager.outOfBoundsCollider)
        {
            outOfBoundsTimer -= Time.deltaTime;
            if(outOfBoundsTimer < 0)
            {
                playerStatsReal.gameObject.transform.position = checkpoint.position;
                outOfBoundsTimer = outOfBoundsDuration;
                gameManager.outOfBoundsCollider = false;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if((other.gameObject.TryGetComponent(out _PlayerStats playerStats)))
            {
                playerStatsReal = playerStats;
                playerStatsReal.TakeHPDamage(damage);
                gameManager.outOfBoundsCollider = true;
                FindObjectOfType<SoundManager>().PlayOneShot("WaterFall");
                outOfBoundsFadeObject.SetActive(true);
                playerStatsReal.gameObject.GetComponent<_AnimationHandler>().anim[playerStatsReal.gameObject.GetComponent<_AnimationHandler>().weapon].SetBool("Walking", false);
            }
        }
    }
}
