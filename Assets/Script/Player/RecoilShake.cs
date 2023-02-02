using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RecoilShake : MonoBehaviour
{
    public static RecoilShake Instance { get; private set; }
    private bool addRecoil = false;

    public void triggerRecoil(bool yesRecoil)
    {
        addRecoil = yesRecoil;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (addRecoil)
        {
            GetComponent<CinemachineImpulseSource>().GenerateImpulse(Camera.main.transform.forward);

            addRecoil = false;
        }
    }
}
