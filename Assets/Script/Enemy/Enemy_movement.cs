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

    public bool hit;

    public float runDistance;
    public float shootingDistance;

    public float hitTime;
    float time = 0;

    public float health;
    public float speed;

    public int bounty;
    bool bountyObtain;

    public bool canShoot;
    public float bulletSpeed;
    public float shootTimer;
    float canShootTimer;

    public enemy type = enemy.MELEE;
    public enum enemy
    {
        MELEE,
        RANGE,
    }
    private void Start()
    {
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
        Vector3 lookTo = player.transform.position;
        transform.LookAt(lookTo);
        float distance = Vector3.Distance(transform.position, player.transform.position);
        animator.SetFloat("Movement", 1);
        Die();
        switch (type)
        {
            case enemy.MELEE:
                {
                    if (hit)
                    {
                        agent.SetDestination(lookTo);
                        animator.SetBool("ATK_Melee", true);
                    }
                    if (!hit)
                    {
                        if (time >= hitTime)
                        {
                            hit = true;
                            time = 0;
                        }
                        if (time < hitTime)
                        {
                            time += Time.deltaTime;
                            if (distance < runDistance)
                            {
                                agent.SetDestination(-player.transform.position);
                            }
                        }
                    }

                }
                break;
            case enemy.RANGE:
                {
                    agent.stoppingDistance = shootingDistance;
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
                break;
        }
    }
    public void Die()
    {
        if (health <= 0)
        {
            if (!bountyObtain)
            {
                banner.increaseKillCount();
                player.GetComponent<PlayerManager>().AddMoney(bounty);
                animator.SetBool("Death", true);
                bountyObtain = true;
            }
            Destroy(this.gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    public void takeDamage(float dmg)
    {
        health -= dmg;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Bullet"))
        {
            //StartCoroutine(hitAnim.HitAnim());
            health -= collision.gameObject.GetComponent<bullet>().getDamage();

        }
        if (collision.gameObject.tag == ("Player"))
        {
            hit = false;
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage();
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
    public void SetHit(bool isHit)
    {
        hit = isHit;
    }
}