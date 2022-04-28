using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCollider : MonoBehaviour
{
    public Transform checkpoint;
    public int damage;
    public GameObject fadeoutFadeinFadeoutWindow;
    public bool isPlayerInside;
    public float playerInsideDuration;
    public float playerInsideTimer;
    private _PlayerStats playerStatsReal;

    void Awake()
    {
        fadeoutFadeinFadeoutWindow.SetActive(false);
    }

    void Start()
    {
        playerInsideTimer = playerInsideDuration;
    }

    void Update()
    {
        if(isPlayerInside)
        {
            playerInsideTimer -= Time.deltaTime;
            if(playerInsideTimer < 0)
            {
                playerStatsReal.gameObject.transform.position = checkpoint.position;
                playerInsideTimer = playerInsideDuration;
                isPlayerInside = false;
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
                isPlayerInside = true;
                FindObjectOfType<SoundManager>().PlayOneShot("WaterFall");
                fadeoutFadeinFadeoutWindow.SetActive(true);
                playerStatsReal.gameObject.GetComponent<_AnimationHandler>().anim[playerStatsReal.gameObject.GetComponent<_AnimationHandler>().weapon].SetBool("Walking", false);
            }
        }
    }
}
