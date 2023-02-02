using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class HitAnimation : MonoBehaviour
{
    Rig rig;
    float targWeight;
    float timer = 1f;
    float noWeight = 0f;

    private void Awake()
    {
        rig = this.GetComponent<Rig>();
        rig.weight = 0f;
        targWeight = 0f;

    }

    private void Update()
    {
        targWeight = rig.weight;
    }

    public IEnumerator HitAnim()
    {
        targWeight = .75f;
        Debug.Log("Hit");
        yield return new WaitForSeconds(timer);
        targWeight = Mathf.Lerp(targWeight, noWeight, timer);
    }
}
