using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraDampenTowardsMouse : MonoBehaviour
{
    private Transform player;
    [SerializeField] private Transform mouseTransform;
    [SerializeField] private Vector3 mousePos;
    private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;

    

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            mousePos = hit.point;
        }
        mousePos.y = 0f;
        mouseTransform.position = mousePos;

        //offSet = (new Vector3(player.position.x, 0f, player.position.z) - new Vector3(mousePos.x, 0f, mousePos.z)).normalized;
    }

    void LateUpdate()
    {
        
    }
}
