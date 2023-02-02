using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : gun
{
    private void Awake()
    {
        bulSpeed = 3000f;
        canAttack = true;
        attackSpeed = 0.5f;
        lastAttack = 0f;

        currentAmmo = 1;
        maxAmmo = 1;
        storedAmmo = 6;
        reloadTime = 3;

        auto = false;
    }
}
