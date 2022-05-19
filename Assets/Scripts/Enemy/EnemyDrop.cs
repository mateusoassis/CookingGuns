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
    private float x;
    private float z;
    public float range;
    public float throwForce;

    [Header("Nome do Item")]
    public string ItemName;

    public int itemType;
    // 0 = biscoito
    // 1 = caramelo
    // 2 = chocolate
    // 3 = donut
    // 4 = ice cream
    // 5 = marshmallow
    // 6 = maça
    // 7 = sugar cane
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //Physics.IgnoreLayerCollision(16, 16, true);
    }

    void Start()
    {
        playerTransform = GameObject.Find("LootTransform").GetComponent<Transform>();
        canGoToPlayer = true;
        x = Random.Range(-2f, 2f);
        z = Random.Range(-2f, 2f);
        rb.AddForce(new Vector3(x, 3f, z) * throwForce, ForceMode.Impulse);
    }

    void Update()
    {
        transform.Rotate(rotationVector * Time.deltaTime * rotationMultiplier);

        distanceFromPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if(distanceFromPlayer < pullDistance && canGoToPlayer)
        {
            StartCoroutine(MoveTowardsThePlayer());
        }
    }

    public IEnumerator MoveTowardsThePlayer()
    {
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

    /*
    void OnCollisionEnter(Collision other)
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

        
        //if(other.gameObject.tag == "Ground")
        //{
        //    Debug.Log("ativa colisão entre drops");
        //}
        
    }
    */
}
