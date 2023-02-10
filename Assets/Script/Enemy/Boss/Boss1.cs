using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss1 : MonoBehaviour
{
    [SerializeField] GameObject bullet, shootingPos;
    public GameObject player, boss;
    NavMeshAgent agent;

    bool canHit = false;
    bool canShoot = true;
    float shootTimer = 0f;
    float bulletSpeed = 0f;

    GameObject fireballParticles;
    public float hitTime;

    public float maxHealth;
    float health;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health > (maxHealth / 2))
        {
            phase1();
        }
        else if (health <= (maxHealth / 2))
        {
            phase2();
        }
    }
    void Shooting()
    {
        var BulletClone = Instantiate(bullet, shootingPos.transform.position, Quaternion.identity);
        var FireballClone = Instantiate(fireballParticles, BulletClone.transform.position, Quaternion.identity);
        FireballClone.transform.parent = BulletClone.transform;
        if (canShoot)
        {
            canShoot = false;
            BulletClone.GetComponent<Rigidbody>().AddForce((player.transform.position - shootingPos.transform.position).normalized * bulletSpeed);
        }
        shootTimer += Time.deltaTime;
        if (canShootTimer >= shootTimer)
        {
            canShoot = true;
            canShootTimer = 0;
        }
    }
    void phase1()
    {
        Shooting();
    }
    void phase2()
    {
        float time = 0;
        if (!hit)
        {
            agent.destination = player.transform.position;
            //animator.SetBool("ATK_Melee", true);
        }
        else
        {
            if (time < hitTime)
            {
                //animator.SetBool("ATK_Melee", false);
                Vector3 runTo = transform.position + ((transform.position - player.transform.position));
                float distance = Vector3.Distance(transform.position, player.transform.position);
                transform.LookAt(player.transform.position);
                if (distance < runDistance)
                {
                    agent.SetDestination(runTo);
                }

                else
                {
                    agent.destination = player.transform.position;
                }
                time += Time.deltaTime;
            }
            else
            {
                hit = false;
                time = 0;
            }
        }
           
    }
}
