using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class auto : gun
{
    private void Awake()
    {
        bulSpeed = 3500f;
        canAttack = true;
        attackSpeed = 0.1f;
        lastAttack = 0f;

        currentAmmo = 45;
        maxAmmo = 45;
        storedAmmo = 250;
        reloadTime = 3;

        auto = true;
    }
}
