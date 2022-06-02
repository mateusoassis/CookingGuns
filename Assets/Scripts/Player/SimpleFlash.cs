using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFlash : MonoBehaviour
{
        [Tooltip("Material to switch to during the flash.")]
        [SerializeField] private Material flashMaterial;

        [Tooltip("Duration of the flash.")]
        [SerializeField] private float flashDuration;

        [SerializeField] private GameObject[] playerMeshParts;

        private SkinnedMeshRenderer meshRenderer;

        private Coroutine flashRoutine;

        public Material[] oldMaterials;

        void Awake()
        {
            oldMaterials = new Material[7];
        }

        void Start()
        {
            for(int n = 0; n< playerMeshParts.Length; n++)
            {
                oldMaterials[n] = playerMeshParts[n].GetComponent<SkinnedMeshRenderer>().material;
            }
        }

        public void Flash()
        {
            if (flashRoutine != null)
            {
                StopCoroutine(flashRoutine);
            }
            flashRoutine = StartCoroutine(FlashRoutine());
            Debug.Log("fez o flash");
        }

        private IEnumerator FlashRoutine()
        {
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
        }

        private IEnumerator InvulnerableFlashing() 
        {

        }
}
