using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.VFX;
using UnityEngine.Animations.Rigging;
using static UnityEditor.PlayerSettings;
using UnityEngine.SceneManagement;

public class Bear_Boss : MonoBehaviour
{
    [SerializeField] GameObject bullet, shootingPos;
    GameObject target;
    NavMeshAgent agent;
    Animator animator;

    PlayerManager player;
    bannerManager banner;
    gun Auto, Pistol;

    public ParticleSystem fireballParticles;

    public bool hit;

    public float shootDistance;

    public float hitTime;
    float time = 0;

    public float stompDistance;
    public float stompTime;
    float stompTimer = 0;

    public float maxHealth;
    public float health;

    public int bounty;
    bool bountyObtain;

    public float bulletSpeed;
    public float shootTimer;
    float canShootTimer;

    public GameObject dissolve;
    public GameObject crack;
    public Transform crackPos;

    public float meleeDelay;
    public float rangeDelay;
    public float stompDelay;

    float distance;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        health = maxHealth;
        hit = false;
        animator = GetComponent<Animator>();
        animator.SetTrigger("Intro");
        target = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        banner = GameObject.FindGameObjectWithTag("Player").GetComponent<bannerManager>();
        Auto = GameObject.FindGameObjectWithTag("auto").GetComponent<gun>();
        Pistol = GameObject.FindGameObjectWithTag("pistol").GetComponent<gun>();
    }
    void Update()
    {
        Die();
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Intro1") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Intro2") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Before Intro State"))
        {
            transform.LookAt(new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z));
            distance = Vector3.Distance(transform.position, target.transform.position);
            animator.SetFloat("Movement", 1);
            stompTimer += Time.deltaTime;
            canShootTimer += Time.deltaTime;

            if (stompTimer >= stompTime && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 2)
            {
                agent.isStopped = true;
                animator.SetTrigger("ATK_AOE");
                StartCoroutine(StompAttack(animator.GetCurrentAnimatorStateInfo(0).length * stompDelay));
                stompTimer = 0;

            }
            else
            {
                agent.isStopped = false;
            }
            if (health > (maxHealth / 2))
            {
                agent.stoppingDistance = shootDistance;
                if (distance > shootDistance)
                {
                    animator.SetFloat("Movement", 1);
                }
                else
                {
                    animator.SetFloat("Movement", 0);
                }
                if (canShootTimer >= shootTimer && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    animator.SetTrigger("ATK_Range");
                    StartCoroutine(RangeAttack(animator.GetCurrentAnimatorStateInfo(0).length * rangeDelay));
                    canShootTimer = 0;

                }
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("ATK_Range"))
                {
                    agent.isStopped = false;
                }
            }
            else if (health <= (maxHealth / 2) && health > 0)
            {
                agent.stoppingDistance = 0;
                if (hit)
                {
                    agent.SetDestination(target.transform.position);
                    animator.SetTrigger("ATK_Melee");
                }
                else
                {
                    time += Time.deltaTime;
                    agent.SetDestination(-target.transform.position);
                    if (time >= hitTime)
                    {
                        hit = true;
                        time = 0;
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
                banner.increaseKillCount("Fire");
                player.GetComponent<PlayerManager>().AddMoney(bounty);
                animator.SetTrigger("Death");
                dissolve.GetComponent<Dissolve>().StartAnim();

                bountyObtain = true;
            }
            if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Destroy(dissolve.gameObject);
                PlayerPrefs.SetInt("PistolloadedAmmo", 1);
                PlayerPrefs.SetInt("PistolstoredAmmo", 60);

                PlayerPrefs.SetInt("health", player.GetHealth());
                PlayerPrefs.SetInt("money", player.GetMoney());

                PlayerPrefs.SetFloat("fireBanner", banner.getAmount("Fire"));
                PlayerPrefs.SetFloat("waterBanner", banner.getAmount("Water"));
                PlayerPrefs.SetFloat("poisonBanner", banner.getAmount("Poison"));
                PlayerPrefs.SetFloat("lightningBanner", banner.getAmount("Lightning"));

                PlayerPrefs.SetFloat("fireKills", banner.getKillCount("Fire"));
                PlayerPrefs.SetFloat("waterKills", banner.getKillCount("Water"));
                PlayerPrefs.SetFloat("poisonKills", banner.getKillCount("Poison"));
                PlayerPrefs.SetFloat("lightningKills", banner.getKillCount("Lightning"));

                PlayerPrefs.SetInt("AutoloadedAmmo", Auto.getCurrAmmo());
                PlayerPrefs.SetInt("AutostoredAmmo", Auto.getStoredAmmo());
                PlayerPrefs.SetInt("PistolloadedAmmo", Pistol.getCurrAmmo());
                PlayerPrefs.SetInt("PistolstoredAmmo", Pistol.getStoredAmmo());

                PlayerPrefs.SetInt("BearBossDone", 1);

                PlayerPrefs.Save();

                SceneManager.LoadScene("Hub");
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Bullet"))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Intro1") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Intro2"))
            {
                health -= collision.gameObject.GetComponent<bullet>().getDamage();
            }

        }
        if (collision.gameObject == target)
        {
            hit = false;
            target.gameObject.GetComponent<PlayerManager>().TakeDamage();
            time = 0;
        }
    }
    public void takeDamage(float dmg)
    {
        health -= dmg;
    }
    IEnumerator StompAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (crack != null)
        {
            Instantiate(crack, crackPos.position, Quaternion.identity);
        }
        if (distance <= stompDistance)
        {
            //target.GetComponent<PlayerManager>().TakeDamage();
            //Debug.Log("stomp");
        }
    }
    IEnumerator RangeAttack(float delay)
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(delay);
        if (bullet.GetComponent<VisualEffect>() != null)
        {
            bullet.GetComponent<VisualEffect>().Play();
        }
        var BulletClone = Instantiate(bullet, shootingPos.transform.position, Quaternion.identity);
        BulletClone.GetComponent<Rigidbody>().AddForce((target.transform.position - shootingPos.transform.position).normalized * bulletSpeed);
    }
}

