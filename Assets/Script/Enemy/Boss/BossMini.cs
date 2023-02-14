using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossMini : MonoBehaviour
{
    Enemy_movement enemy;
    void Start()
    {
        enemy = this.gameObject.GetComponent<Enemy_movement>();
    }
    void Update()
    {
        enemy.canSeeTarget = true;
        if (enemy.health > (enemy.maxHealth / 2))
        {
            enemy.type = Enemy_movement.enemy.RANGE;
        }
        else if (enemy.health <= (enemy.maxHealth / 2))
        {
            enemy.type = Enemy_movement.enemy.MELEE;
        } 
    }
}
