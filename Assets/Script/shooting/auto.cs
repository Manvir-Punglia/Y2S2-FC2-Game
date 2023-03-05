using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class auto : gun
{
    private void Awake()
    {
        bulSpeed = 3500f;
        canAttack = true;
        attackSpeed = 0f;
        lastAttack = 0f;

        currentAmmo = 25;
        maxAmmo = 25;
        storedAmmo = 150;
        maxStoredAmmo = 150;
        reloadTime = 3;

        auto = true;
    }
}
