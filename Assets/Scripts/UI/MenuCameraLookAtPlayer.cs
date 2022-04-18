using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraLookAtPlayer : MonoBehaviour
{
    public Animator[] anim;
    public GameObject[] characterModels;
    public int modelIndex;
    public Camera characterCamera;
    
    public Vector3 rotationVector;
    public float rotationMultiplier;

    [SerializeField] private float delayTimer;
    private float durationTimer;
    public float delayBetweenAnimations;
    public float durationOfAnimations;
    public int lastAnimation;
    // 0 = walking
    // 1 = shooting

    void Start()
    {
        modelIndex = Random.Range(0, characterModels.Length);

        for(int n = 0; n < characterModels.Length; n++)
        {
            if(n == modelIndex)
            {
                characterModels[n].SetActive(true);
            }
            else
            {
                characterModels[n].SetActive(false);
            }
        }

        delayTimer = delayBetweenAnimations;
    }

    void Update()
    {
        transform.Rotate(rotationVector * Time.deltaTime * rotationMultiplier);

        UseAnimationWhenTimeZero();
    }

    public void UseAnimationWhenTimeZero()
    {
        if(delayTimer < 0 && lastAnimation == 0)
        {
            anim[modelIndex].SetBool("Walking", false);
            if(modelIndex == 2)
            {
                anim[modelIndex].Play("ShootShotgun");
            }
            else
            {
                anim[modelIndex].SetTrigger("Shoot");
            }
            delayTimer = delayBetweenAnimations;
            lastAnimation = 1;
        }
        else
        {
            delayTimer -= Time.deltaTime;
        }

        if(delayTimer < 0 && lastAnimation == 1)
        {
            anim[modelIndex].SetBool("Walking", true);
            delayTimer = delayBetweenAnimations;
            lastAnimation = 0;
        }
        else
        {
            delayTimer -= Time.deltaTime;
        }

    }
}
