using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainPlayerPos : MonoBehaviour
{   
    private Transform player;
    [SerializeField] private Vector3 offsetFromPlayer;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        offsetFromPlayer = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position + offsetFromPlayer;
    }
}
