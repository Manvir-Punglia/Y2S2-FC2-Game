using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class pistol : gun
{
    public Animator PistolAnimator;
    private void Awake()
    {
     bulSpeed = 3000f;
     canAttack = true;
     attackSpeed = 0.5f;
     lastAttack = 0f;

     currentAmmo = 10;
     maxAmmo = 10;
     storedAmmo = 60;
     reloadTime = 3;

     auto = false;

     animator = PistolAnimator;
    }

    


}
