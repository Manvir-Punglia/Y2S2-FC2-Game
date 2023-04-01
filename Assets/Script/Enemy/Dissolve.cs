using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Dissolve : MonoBehaviour
{
    public VisualEffect vfxDissolve;

    public SkinnedMeshRenderer skinnedMesh;

    private Material[] skinnedMaterials;

    public float dissolveRate = 0.0125f;
    public float refreshRate = 0.025f;

    
    void Start()
    {

        if (skinnedMesh != null)
        {
            skinnedMaterials = skinnedMesh.materials;
            
        }
        
    }

    IEnumerator DissolveAnim(float delay)
    {
        if (vfxDissolve != null)
        {
            vfxDissolve.Play();
        }

        if (skinnedMaterials.Length > 0)
        {
            float counter = 0;

            yield return new WaitForSeconds(delay);

            while (skinnedMaterials[0].GetFloat("_DissolveAmount") < 1)
            {

                counter += dissolveRate;

                for (int i = 0; i < skinnedMaterials.Length; i++)
                {

                    skinnedMaterials[i].SetFloat("_DissolveAmount", counter);
                }

                yield return new WaitForSeconds(refreshRate); 
            }
        }
        

    }

    public void StartAnim()
    {
        StartCoroutine(DissolveAnim(vfxDissolve.GetFloat("Delay")));
    }
}
