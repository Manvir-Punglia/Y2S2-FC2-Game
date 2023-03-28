using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatShaderControl : MonoBehaviour
{
    public float ErodeRate = 0.03f;
    public float ErodeRefreshRate = 0.01f;
    public float ErodeDelay = 1.25f;
    //public SkinnedMeshRenderer[] ErodeMesh;
    //public Material[] Mats;
    public Material Mat;
    void Start()
    {
        //ErodeMesh = GetComponentsInChildren<SkinnedMeshRenderer>();
        //Mats = ErodeMesh.GetComponentsInChildren<Material>();
        StartCoroutine(ErodeObject());
    }

    IEnumerator ErodeObject()
    {
        yield return new WaitForSeconds(ErodeDelay);

        float t = 0;
        while (t < 1)
        {
            t += ErodeRate;
            Mat.SetFloat("_Erode", t);
            yield return new WaitForSeconds(ErodeRefreshRate);
        }


    }
}
