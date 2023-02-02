using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waffle : gun
{
    
    
    private void Awake()
    {
        //gunSound.Play();

        bulSpeed = 3500f;
        canAttack = true;
        attackSpeed = 1.5f;
        lastAttack = 0f;

        currentAmmo = 5;
        maxAmmo = 5;
        storedAmmo = 50;
        maxStoredAmmo = 50;
        reloadTime = 3;

        auto = false;

        //gunSound.Play();
    }

    

}
