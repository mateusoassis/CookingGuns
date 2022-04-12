using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector;
    [SerializeField] private float rotationMultiplier;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float pullDistance;
    [SerializeField] private float timeToPull;
    public float distanceFromPlayer;
    public bool canGoToPlayer;

    [Header("Nome do Item")]
    public string ItemName;
    
    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        canGoToPlayer = true;
    }

    void Update()
    {
        transform.Rotate(rotationVector * Time.deltaTime * rotationMultiplier);

        distanceFromPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if(distanceFromPlayer < pullDistance && canGoToPlayer)
        {
            Debug.Log("if");
            StartCoroutine(MoveTowardsThePlayer());
        }
    }

    public IEnumerator MoveTowardsThePlayer()
    {
        Debug.Log("coroutine");
        canGoToPlayer = false;
        var currentPos = transform.position;
        var t = 0f;
        while(t < 1f)
        {
            t += Time.deltaTime / timeToPull;
            transform.position = Vector3.Lerp(currentPos, playerTransform.position, t);
            yield return null;    
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("ganhou material tal");
            other.gameObject.GetComponent<Inventory>().AddItem(ItemName);
            Destroy(this.gameObject);
        }
    }
}
