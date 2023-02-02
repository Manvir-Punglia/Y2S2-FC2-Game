using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fieldOfView : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public float melee_Radius;
    public float melee_Angle;

    public float range_Radius;
    public float range_Angle;

    public GameObject player;
    public LayerMask targetMask;
    public LayerMask obstructionMask;
    Enemy_movement enemy;
    float time;
    public float chase_Time;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<Enemy_movement>();
        if (this.gameObject.GetComponent<Enemy_movement>().type == Enemy_movement.enemy.MELEE)
        {
            radius = melee_Radius;
            angle = melee_Angle;
        }
        if (this.gameObject.GetComponent<Enemy_movement>().type == Enemy_movement.enemy.RANGE)
        {
            radius = range_Radius;
            angle = range_Angle;
        }
    }
    private void Update()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstructionMask))
                {
                    enemy.SetCanSee(true);
                    time = 0;
                }
                else
                {
                    CantSee();
                }
            }
            else
            {
                CantSee();
            }
        }
        else if (enemy.GetCanSee())
        {
            CantSee();
        }
    }
    void CantSee()
    {
        if (enemy.GetCanSee())
        {
            time += Time.deltaTime;
        }
        if (time >= chase_Time)
        {
            enemy.SetCanSee(false);
            time = 0;
        }
    }
}
