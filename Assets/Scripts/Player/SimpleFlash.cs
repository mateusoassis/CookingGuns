using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFlash : MonoBehaviour
{
        [Tooltip("Material to switch to during the flash.")]
        public Material flashMaterial;

        [Tooltip("Duration of the flash.")]
        public float flashDuration;
        public GameObject[] playerMeshParts;

        private SkinnedMeshRenderer meshRenderer;

        public Coroutine flashRoutine;

        public Material[] oldMaterials;

        private _PlayerStats playerStats;

        void Awake()
        {
            oldMaterials = new Material[7];
            for(int n = 0; n< playerMeshParts.Length; n++)
            {
                oldMaterials[n] = playerMeshParts[n].GetComponent<SkinnedMeshRenderer>().material;
            }
            playerStats = GameObject.Find("Player").GetComponent<_PlayerStats>();
            flashDuration = playerStats.immuneDuration;
        }

        void Start()
        {
            
        }

        /*

        public void Flash()
        {
            
            if (flashRoutine != null)
            {
                StopCoroutine(flashRoutine);
            }
            
            flashRoutine = StartCoroutine(FlashRoutine());

            Debug.Log("chama o flash");

            //for (float duration = 0; duration < 2; duration += Time.deltaTime)
            //{
            //    StartCoroutine(FlashRoutine());
            //} 
        }

        private IEnumerator FlashRoutine()
        {
            Debug.Log("fez o flash");
            foreach(GameObject k in playerMeshParts)
            {
                k.GetComponent<SkinnedMeshRenderer>().material = flashMaterial;
            }
            yield return new WaitForSeconds(flashDuration);
            for(int n = 0; n< playerMeshParts.Length; n++)
            {
                playerMeshParts[n].GetComponent<SkinnedMeshRenderer>().material = oldMaterials[n];
            }
            flashRoutine = null;
            Debug.Log("acaba o flash");
        }
        */
}
