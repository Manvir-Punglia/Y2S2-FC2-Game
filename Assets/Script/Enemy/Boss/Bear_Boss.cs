using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;
using static UnityEditor.PlayerSettings;

public class Bear_Boss : MonoBehaviour
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

    public float stompDistance;
    public float stompTime;
    float stompTimer = 0;

    public float maxHealth;
    public float health;

    public int bounty;
    bool bountyObtain;

    public bool canShoot;
    public float bulletSpeed;
    public float shootTimer;
    float canShootTimer;

    void Start()
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
        health = maxHealth;
    }
    void Update()
    {
        //if ((!animator.GetCurrentAnimatorStateInfo(0).IsName("Intro") || !animator.GetCurrentAnimatorStateInfo(0).IsName("Intro 2")) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            transform.LookAt(new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z));
            Vector3 runTo = transform.position + ((transform.position - player.transform.position));
            runTo.y = this.transform.position.y;
            float distance = Vector3.Distance(transform.position, player.transform.position);

            stompTimer += Time.deltaTime;
            if (stompTimer >= stompTime)
            {
                agent.destination = this.gameObject.transform.position;
                animator.SetTrigger("ATK_AOE");
                if (distance <= stompDistance)
                {
                    player.GetComponent<PlayerManager>().TakeDamage();
                }
                stompTimer = 0;
            }
            if (health > (maxHealth / 2))
            {
                agent.stoppingDistance = shootingDistance;
                canShootTimer += Time.deltaTime;
                if (canShootTimer >= shootTimer)
                {
                    var BulletClone = Instantiate(bullet, shootingPos.transform.position, Quaternion.identity);
                    BulletClone.GetComponent<Rigidbody>().AddForce((player.transform.position - shootingPos.transform.position).normalized * bulletSpeed);
                    animator.SetTrigger("ATK_Range");
                    canShootTimer = 0;
                }
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("ATK_Range"))
                {
                    agent.isStopped = true;
                }
            }
            else if (health <= (maxHealth / 2))
            {
                agent.stoppingDistance = 0;
                if (hit)
                {
                    agent.SetDestination(-runTo);
                    animator.SetTrigger("ATK_Melee");
                }
                else
                {
                    if (time >= hitTime)
                    {
                        hit = false;
                        time = 0;
                    }
                    if (time < hitTime)
                    {
                        time += Time.deltaTime;
                        if (distance < runDistance)
                        {
                            agent.SetDestination(runTo);
                        }
                    }
                }
            }
        }
    }
    public void Die()
    {
        if (health <= 0)
        {
            hit = false;
            if (!bountyObtain)
            {
                //banner.increaseKillCount();
                //player.GetComponent<PlayerManager>().AddMoney(bounty);
                animator.SetTrigger("Death");
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Bullet"))
        {
            StartCoroutine(hitAnim.HitAnim());
            health -= collision.gameObject.GetComponent<bullet>().getDamage();

        }
        if (collision.collider.CompareTag("Player"))
        {
            hit = true;
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage();
            time = 0;
        }
    }
    public void SetHit(bool isHit)
    {
        hit = isHit;
    }
}

