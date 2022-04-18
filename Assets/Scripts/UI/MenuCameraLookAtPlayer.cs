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
    public int lastAnimation;

    [Header("Entre animações")]
    [SerializeField] private float delayTimer;
    public float delayBetweenAnimations;

    [Header("Entre modelos")]
    [SerializeField] private float durationTimer;
    public float durationOfModels;
    
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
        durationTimer = durationOfModels;

        transform.Rotate((rotationVector * rotationMultiplier * Random.Range(-5, 6)) + (Random.Range(-rotationMultiplier, rotationMultiplier) * rotationVector));
    }

    void Update()
    {
        transform.Rotate(rotationVector * Time.deltaTime * rotationMultiplier);

        UseAnimationWhenTimerZero();
        SwapModelWhenTimerZero();
    }

    public void UseAnimationWhenTimerZero()
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
            delayTimer = delayBetweenAnimations + Random.Range(0, 6);
            lastAnimation = 1;
        }
        else
        {
            delayTimer -= Time.deltaTime;
        }

        if(delayTimer < 0 && lastAnimation == 1)
        {
            anim[modelIndex].SetBool("Walking", true);
            delayTimer = delayBetweenAnimations + Random.Range(0, 6);
            lastAnimation = 0;
        }
        else
        {
            delayTimer -= Time.deltaTime;
        }
    }

    public void SwapModelWhenTimerZero()
    {
        if(durationTimer < 0)
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
            durationTimer = durationOfModels;
            delayTimer = 6f;
        }
        else
        {
            durationTimer -= Time.deltaTime;
        }
    }
}
