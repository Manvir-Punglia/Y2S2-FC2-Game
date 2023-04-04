using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;
using UnityEngine.VFX;
using static Enemy_movement;
using static UnityEditor.PlayerSettings;

public class Enemy_movement : MonoBehaviour
{
    [SerializeField] GameObject bullet, shootingPos;
    GameObject target;
    NavMeshAgent agent;
    Animator animator;

    public PlayerManager player;
    public bannerManager banner;
    public gun Auto, Pistol;

    HitAnimation hitAnim;

    public string enemyName;

    //public ParticleSystem fireballParticles;

    public bool hit;
    public bool contacting;

    public float runDistance;
    public float shootingDistance;
    public float stopDistance;

    public float hitTime;

    public float MeleeDelay;
    public float RangeDelay;

    float time = 0;

    public GameObject melee;

    public float health;
    public float speed;

    public int bounty;
    bool bountyObtain;

    public bool canShoot;
    public float bulletSpeed;
    public float shootTimer;
    float canShootTimer;

    public GameObject dissolve;

    public enemy type = enemy.MELEE;
    public enum enemy
    {
        MELEE,
        RANGE,
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        //hitAnim = FindObjectOfType<HitAnimation>().GetComponent<HitAnimation>();

    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        transform.localEulerAngles = Vector3.zero;
        agent.speed = speed;
        transform.LookAt(new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z));
        float distance = Vector3.Distance(transform.position, target.transform.position);
        Die();
        switch (type)
        {
            case enemy.MELEE:
                {
                    animator.SetFloat("Movement", 0);
                    agent.stoppingDistance = stopDistance;
                    if (hit)
                    {

                        agent.SetDestination(target.transform.position);

                    }
                    if (!hit)
                    {
                        time += Time.deltaTime;

                        if (time >= hitTime)
                        {
                            hit = true;
                            time = 0;
                        }
                        
                        if (distance < runDistance)
                        {

                            agent.SetDestination(-target.transform.position);

                        }
                        
                    }
                    if (contacting)
                    {
                        agent.isStopped = true;
                    }
                    else
                    {
                        agent.isStopped = false;
                    }

                }
                break;
            case enemy.RANGE:
                {
                    agent.stoppingDistance = shootingDistance;
                    agent.SetDestination(target.transform.position);

                    if (distance > shootingDistance)
                    {
                        animator.SetFloat("Movement", 1);
                    }
                    else
                    {
                        animator.SetFloat("Movement", 0);
                    }

                    canShootTimer += Time.deltaTime;
                    if (canShoot)
                    {
                        animator.SetTrigger("ATK_Range");
                        StartCoroutine(RangeAttack(animator.GetCurrentAnimatorStateInfo(0).length * RangeDelay));

                        canShoot = false;

                    }
                    if (canShootTimer >= shootTimer)
                    {
                        canShoot = true;
                        canShootTimer = 0;
                    }

                }
                break;
        }
    }
    public void Die()
    {

        if (health <= 0)
        {
            agent.isStopped = true;
            if (!bountyObtain)
            {
                switch (enemyName)
                {
                    case "Bear":
                        //fire
                        player.GetComponent<bannerManager>().increaseKillCount("Fire");
                        break;

                    case "Frog":
                        //water
                        player.GetComponent<bannerManager>().increaseKillCount("Water");
                        break;

                    case "Bird":
                        //lightning
                        player.GetComponent<bannerManager>().increaseKillCount("Lightning");
                        break;

                    case "Rat":
                        //poison
                        player.GetComponent<bannerManager>().increaseKillCount("Poison");
                        break;
                }
                target.GetComponent<PlayerManager>().AddMoney(bounty);
                animator.SetTrigger("Death");
                dissolve.GetComponent<Dissolve>().StartAnim();

                bountyObtain = true;
            }
            Destroy(dissolve.gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }

    public void takeDamage(float dmg)
    {
        health -= dmg;
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == ("Player") && hit)
        {
            hit = false;
            Debug.LogWarning("contact");
            animator.SetTrigger("ATK_Melee");
            StartCoroutine(MeleeAttack(animator.GetCurrentAnimatorStateInfo(0).length * MeleeDelay));
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            contacting = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            contacting = false;
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

    IEnumerator MeleeAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (melee.GetComponent<ParticleSystem>() != null)
        {
            melee.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            melee.GetComponent<VisualEffect>().Play();
        }
        if (contacting)
        {
            target.GetComponent<PlayerManager>().TakeDamage();
        }
        Debug.LogError("test");
    }

    IEnumerator RangeAttack(float delay)
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(delay);
        if (bullet.GetComponent<VisualEffect>() != null)
        {
            Debug.LogWarning("shot");
            bullet.GetComponent<VisualEffect>().Play();
        }
        var BulletClone = Instantiate(bullet, shootingPos.transform.position, Quaternion.identity);
        BulletClone.GetComponent<Rigidbody>().AddForce((target.transform.position - shootingPos.transform.position).normalized * bulletSpeed);
        agent.isStopped = false;
    }
}