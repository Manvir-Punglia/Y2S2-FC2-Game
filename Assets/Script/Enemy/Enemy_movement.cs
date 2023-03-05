using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;
using static Enemy_movement;
using static UnityEditor.PlayerSettings;

public class Enemy_movement : MonoBehaviour
{
    [SerializeField] GameObject bullet, shootingPos;
    public GameObject player;
    NavMeshAgent agent;
    Animator animator;

    bannerManager banner;

    HitAnimation hitAnim;

    //public ParticleSystem fireballParticles;

    public bool canSeeTarget;
    public bool hit;

    public float runDistance;
    public float shootingDistance;

    public float hitTime;
    float time = 0;

    public float health;

    public int bounty;
    bool bountyObtain;

    public float wanderRadius;
    float timer = 0;
    public float wanderingTimer;
    public float speed;

    public bool canShoot;
    public float bulletSpeed;
    public float shootTimer;
    float canShootTimer;

    public enemy type = enemy.MELEE;
    public enum enemy
    {
        MELEE,
        RANGE,
        MONEY,
    }
    private void Start()
    {
        hit = false;
        player = GameObject.FindWithTag("Player");
        banner = GameObject.FindGameObjectWithTag("Player").GetComponent<bannerManager>();
        animator = GetComponent<Animator>();
        //hitAnim = FindObjectOfType<HitAnimation>().GetComponent<HitAnimation>();

    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        agent.speed = speed;
        Vector3 runTo = transform.position + ((transform.position - player.transform.position));
        float distance = Vector3.Distance(transform.position, player.transform.position);
        animator.SetFloat("Movement", 0);
        Die();
        if (canSeeTarget)
        {
            transform.LookAt(player.transform.position);
            animator.SetFloat("Movement", 1);
            switch (type)
            {
                case enemy.MELEE:
                    {
                        if (!hit)
                        {
                            agent.destination = player.transform.position;
                            animator.SetBool("ATK_Melee", true);
                        }
                        else
                        {
                            if (time < hitTime)
                            {
                                animator.SetBool("ATK_Melee", false);
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
                    break;
                case enemy.RANGE:
                    {
                        if (distance < shootingDistance)
                        {
                            agent.SetDestination(runTo);
                        }
                        else
                        {
                            agent.destination = player.transform.position;
                        }
                        Shooting();
                    }
                    break;
                case enemy.MONEY:
                    {
                        if (distance < runDistance)
                        {
                            agent.SetDestination(runTo);
                        }
                    }
                    break;
            }
        }
        else
        {
            Wandering();
        }
    }
    public void Die()
    {
        if (health <= 0)
        {
            canSeeTarget = false;
            hit = false;
            if (!bountyObtain)
            {
                //banner.increaseKillCount();
                //player.GetComponent<PlayerManager>().AddMoney(bounty);
                animator.SetBool("Death", true);
                bountyObtain = true;
            }
            Destroy(this.gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
            //Destroy(gameObject);
        }
    }

    public void takeDamage(float dmg)
    {
        health -= dmg;
    }

    void Shooting()
    {
        canShootTimer += Time.deltaTime;
        if (canShoot)
        {
            var BulletClone = Instantiate(bullet, shootingPos.transform.position, Quaternion.identity);
            //var FireballClone = Instantiate(fireballParticles, BulletClone.transform.position, Quaternion.identity);
            //FireballClone.transform.parent = BulletClone.transform;

            BulletClone.GetComponent<Rigidbody>().AddForce((player.transform.position - shootingPos.transform.position).normalized * bulletSpeed);
            //fireballParticles.Play();

            canShoot = false;
            animator.SetBool("ATK_Range", true);

        }
        if (canShootTimer >= shootTimer && (transform.position - player.transform.position).magnitude >= shootingDistance)
        {
            canShoot = true;
            canShootTimer = 0;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ATK_Range"))
        {
            agent.isStopped = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Bullet"))
        {
            StartCoroutine(hitAnim.HitAnim());
            canSeeTarget = true;
            health -= collision.gameObject.GetComponent<bullet>().getDamage();

        }
        if (collision.collider.CompareTag("Player"))
        {
            hit = true;
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage();
        }
    }
    void Wandering()
    {
        timer += Time.deltaTime;

        if (timer >= wanderingTimer)
        {
            Vector3 pos;
            float ang = Random.value * 360;
            pos.x = transform.position.x + (wanderRadius * Mathf.Sin(ang * Mathf.Deg2Rad));
            pos.y = transform.position.y;
            pos.z = transform.position.z + (wanderRadius * Mathf.Cos(ang * Mathf.Deg2Rad));
            agent.SetDestination(pos);
            timer = 0;
        }
        if ((transform.position - agent.destination).magnitude > 0)
        {
            animator.SetFloat("Movement", 1);
        }
    }
    //public void ShootFireBall()
    //{

    //    var BulletClone = Instantiate(bullet, shootingPos.transform.position, Quaternion.identity);
    //    var FireballClone = Instantiate(fireballParticles, BulletClone.transform.position, Quaternion.identity);
    //    FireballClone.transform.parent = BulletClone.transform;

    //    BulletClone.GetComponent<Rigidbody>().AddForce((player.transform.position - shootingPos.transform.position).normalized * bulletSpeed);
    //    fireballParticles.Play();

    //}

    public bool GetCanSee()
    {
        return canSeeTarget;
    }
    public void SetCanSee(bool canSee)
    {
        canSeeTarget = canSee;
    }
    public void SetHit(bool isHit)
    {
        hit = isHit;
    }
}