using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    public SimpleFlash[] simpleFlash;
    public _WeaponHandler weaponHandler;

    public void Flash()
        {
            for(int n = 0; n < simpleFlash.Length; n++)
            {
                if (simpleFlash[n].flashRoutine != null)
                {
                    StopCoroutine(simpleFlash[n].flashRoutine);
                }
                
                simpleFlash[n].flashRoutine = StartCoroutine(FlashRoutine());

                Debug.Log("chama o flash");

                //for (float duration = 0; duration < 2; duration += Time.deltaTime)
                //{
                //    StartCoroutine(FlashRoutine());
                //} 
            }
            
        }

        private IEnumerator FlashRoutine()
        {
            for(int i = 0; i < simpleFlash.Length; i++)
            {
                Debug.Log("fez o flash");
                foreach(GameObject k in simpleFlash[i].playerMeshParts)
                {
                    Debug.Log("trocou material");
                    k.GetComponent<SkinnedMeshRenderer>().material = simpleFlash[i].flashMaterial;
                }

                /*
                for(int n = 0; n < simpleFlash[i].playerMeshParts.Length; n++)
                {
                    simpleFlash[i].playerMeshParts[n].GetComponent<SkinnedMeshRenderer>().material = simpleFlash[i].oldMaterials[n];
                }
                simpleFlash[i].flashRoutine = null;
                Debug.Log("acaba o flash");
                */

            }
            yield return new WaitForSeconds(simpleFlash[0].flashDuration);
            for(int i = 0; i < simpleFlash.Length; i++)
            {
                for(int n = 0; n < simpleFlash[i].playerMeshParts.Length; n++)
                {
                    simpleFlash[i].playerMeshParts[n].GetComponent<SkinnedMeshRenderer>().material = simpleFlash[i].oldMaterials[n];
                }
                simpleFlash[i].flashRoutine = null;
                Debug.Log("acaba o flash");
            }
        }
}
