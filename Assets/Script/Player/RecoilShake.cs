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
            if (PlayerPrefs.GetInt("CurrentGun") == 1)
            {
                GetComponent<CinemachineImpulseSource>().GenerateImpulse(Camera.main.transform.forward);

                addRecoil = false;
            }
            if (PlayerPrefs.GetInt("CurrentGun") == 0)
            {
                GetComponent<CinemachineImpulseSource>().GenerateImpulseWithVelocity(new Vector3 (0.0f, 0.2f, 0.0f));
                addRecoil = false;
            }
        }
        
    }
}
