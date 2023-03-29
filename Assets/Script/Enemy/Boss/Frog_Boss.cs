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

public class Frog_Boss : MonoBehaviour
{
    [SerializeField] GameObject bullet, shootingPos;
    public GameObject player;
    Animator animator;
    Rigidbody rb;

    bannerManager banner;

    HitAnimation hitAnim;

    //public ParticleSystem fireballParticles;

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
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        ground = true;
    }

    // Update is called once per frame
    void Update()
    {
        jTimer += Time.deltaTime;
        canShootTimer += Time.deltaTime;
        if (jTimer >= jumpTime && ground)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            jTimer = 0;
        }
        if (!ground)
        {
            transform.LookAt(player.transform);
            rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);
        }

        if (canShootTimer >= shootTimer)
        {
            canShootTimer = 0;
            var BulletClone = Instantiate(bullet, shootingPos.transform.position, Quaternion.identity);
            BulletClone.GetComponent<Rigidbody>().AddForce((player.transform.position - shootingPos.transform.position).normalized * bulletSpeed);
            animator.SetTrigger("ATK_Range");
        }
        Die();
    }
    public void Die()
    {
        if (health <= 0)
        {
            if (!bountyObtain)
            {
                //banner.increaseKillCount();
                //player.GetComponent<PlayerManager>().AddMoney(bounty);
                animator.SetTrigger("Death");
                bountyObtain = true;
            }
            Destroy(this.gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
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
            //StartCoroutine(hitAnim.HitAnim());
            health -= collision.gameObject.GetComponent<bullet>().getDamage();

        }
        if (collision.gameObject.tag == ("Player"))
        {
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage();
        }
    }
}
