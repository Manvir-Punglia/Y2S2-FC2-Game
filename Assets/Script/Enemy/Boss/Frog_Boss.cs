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
using UnityEngine.SceneManagement;

public class Frog_Boss : MonoBehaviour
{
    [SerializeField] GameObject bullet, shootingPos;
    GameObject target;
    Animator animator;
    Rigidbody _rb;

    PlayerManager player;
    bannerManager banner;
    gun Auto, Pistol;

    HitAnimation hitAnim;

    public float jumpTime;
    float jTimer = 0;
    public float jumpHeight;
    bool ground;

    public float speed;

    public float health;

    public int bounty;
    bool bountyObtain;

    public float bulletSpeed;
    public float shootTimer;
    float canShootTimer;

    public GameObject dissolve;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        banner = GameObject.FindGameObjectWithTag("Player").GetComponent<bannerManager>();
        //Auto = GameObject.FindGameObjectWithTag("auto").GetComponent<gun>();
        //Pistol = GameObject.FindGameObjectWithTag("pistol").GetComponent<gun>();
        animator = GetComponent<Animator>();
        
        ground = true;
        
    }

    void Update()
    {
        //if ((!animator.GetCurrentAnimatorStateInfo(0).IsName("Intro") || !animator.GetCurrentAnimatorStateInfo(0).IsName("Intro 2")) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            jTimer += Time.deltaTime;
            canShootTimer += Time.deltaTime;
            if (jTimer >= jumpTime && ground)
            {
                _rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                jTimer = 0;
            }
            if (!ground)
            {
                animator.SetTrigger("ATK_AOE");
                transform.LookAt(new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z));
                _rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);
            }

            if (canShootTimer >= shootTimer)
            {
                canShootTimer = 0;
                var BulletClone = Instantiate(bullet, shootingPos.transform.position, Quaternion.identity);
                BulletClone.GetComponent<Rigidbody>().AddForce((target.transform.position - shootingPos.transform.position).normalized * bulletSpeed);
                animator.SetTrigger("ATK_Range");
            }
            Die();
        }
    }
    public void Die()
    {
        if (health <= 0)
        {
            
            if (!bountyObtain)
            {
                //banner.increaseKillCount();
                //target.GetComponent<PlayerManager>().AddMoney(bounty);
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

                PlayerPrefs.SetInt("FrogBossDone", 1);

                PlayerPrefs.Save();

                SceneManager.LoadScene("Hub");
            }
        }
    }
    public void takeDamage(float dmg)
    {
        health -= dmg;
    }
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "floor")
        {
            ground = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "floor")
        {
            ground = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Bullet"))
        {
            health -= collision.gameObject.GetComponent<bullet>().getDamage();

        }
        if (collision.gameObject.tag == ("Player"))
        {
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage();
        }
    }
}
