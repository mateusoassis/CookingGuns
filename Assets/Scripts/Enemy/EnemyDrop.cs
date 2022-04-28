using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector;
    [SerializeField] private float rotationMultiplier;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float pullDistance;
    [SerializeField] private float timeToPull;
    public float distanceFromPlayer;
    public bool canGoToPlayer;
    public Rigidbody rb;

    [Header("Nome do Item")]
    public string ItemName;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        canGoToPlayer = true;
        //rb.AddForce();
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
            if(SceneManager.GetActiveScene().buildIndex != 1) // checando se o jogo está ou não no TUTORIAL
            {
                other.gameObject.GetComponent<Inventory>().AddItem(ItemName);
            }
            Destroy(this.gameObject);
        }
    }
}
