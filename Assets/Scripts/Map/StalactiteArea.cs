using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalactiteArea : MonoBehaviour
{
    [SerializeField]private float bombTimer;
    public GranadeInnerArea granadeInnerAreaScript;
    //public CapsuleCollider capsuleCollider;
    public GameObject stalactiteObject;

    private bool gotObject;

    void Awake()
    {
        transform.rotation = Quaternion.identity;
        stalactiteObject = GameObject.Find("Stalactite(Clone)");
    }

    void Start()
    {
        if(stalactiteObject != null)
        {
            gotObject = true;
            Debug.Log("adeus");
        }
        StartCoroutine("BombTimer");
    }

    void Update()
    {
        transform.Rotate(0, 1f, 0 * Time.deltaTime);
        if(stalactiteObject == null && gotObject)
        {
            Debug.Log("mano");
            Destroy(this.gameObject);
        }
    } 

    public IEnumerator BombTimer()
    {
        yield return new WaitForSeconds(bombTimer);
        //FindObjectOfType<SoundManager>().PlayOneShot("ExplosionSound");
        granadeInnerAreaScript.isMaxSize = true;
        granadeInnerAreaScript.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        yield return new WaitForSeconds(0.2f);
    }
}
