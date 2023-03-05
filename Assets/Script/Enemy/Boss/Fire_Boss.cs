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

public class Fire_Boss : MonoBehaviour
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
        Vector3 runTo = transform.position + ((transform.position - player.transform.position));
        float distance = Vector3.Distance(transform.position, player.transform.position);
        transform.LookAt(player.transform.position);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Intro1") || animator.GetCurrentAnimatorStateInfo(0).IsName("Intro2"))
        {
            agent.destination = this.gameObject.transform.position;
        }
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Intro1") || !animator.GetCurrentAnimatorStateInfo(0).IsName("Intro2"))
        {
            stompTimer += Time.deltaTime;
            if (stompTimer >= stompTime)
            {
                agent.destination = this.gameObject.transform.position;
                animator.SetBool("ATK_AOE", true);
                if (distance <= stompDistance)
                {
                    player.GetComponent<PlayerManager>().TakeDamage();
                }
                stompTimer = 0;
            }
            if (health > (maxHealth / 2))
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
            else if (health <= (maxHealth / 2))
            {
                time += Time.deltaTime;
                agent.destination = player.transform.position;
                if (stompTimer < stompTime)
                {
                    if (time >= hitTime)
                    {
                        animator.SetBool("ATK_Melee", true);
                        agent.destination = player.transform.position;
                    }
                    if (time < hitTime)
                    {
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
        if (canShootTimer >= shootTimer)
        {
            canShoot = true;
            canShootTimer = 0;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ATK_Range"))
        {
            agent.destination = this.gameObject.transform.position;
        }
        animator.SetBool("ATK_Range", false);
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

