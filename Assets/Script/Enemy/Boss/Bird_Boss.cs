using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Boss : MonoBehaviour
{
    [SerializeField] GameObject bullet, shootingPos;
    public GameObject player;
    public GameObject[] pos;
    Animator animator;
    Rigidbody rb;
    public Collider hitBox;
    public Collider chargeBox;

    bannerManager banner;


    float timer = 0;
    public float teleportT;

    public float health;


    bool canShoot;
    public float bulletSpeed;
    public float shootTimer;
    float canShootTimer;

    bool charge;
    public float chargeTimer;
    float chargeTime;
    public float waitTimer;
    float wait;
    public float speed;
    bool chargeAnimTriggered;
    // Start is called before the first frame update
    public void Awake()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
        if ((!animator.GetCurrentAnimatorStateInfo(0).IsName("Intro") || !animator.GetCurrentAnimatorStateInfo(0).IsName("Intro 2")) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            if (!charge)
            {
                hitBox.enabled = true;
                chargeBox.enabled = false;
                rb.velocity = Vector3.zero;
                timer += Time.deltaTime;
                canShootTimer += Time.deltaTime;
                if (timer >= teleportT)
                {
                    timer = 0;
                    this.transform.position = pos[Random.Range(0, pos.Length)].transform.position;
                }
                if (canShootTimer >= shootTimer && timer < teleportT)
                {
                    Shooting();
                    canShootTimer = 0;
                }
            }
            else if (charge)
            {
                if (chargeAnimTriggered)
                {
                    animator.SetTrigger("ATK_Melee");
                    chargeAnimTriggered = false;
                }
                    hitBox.enabled = false;
                chargeBox.enabled = true;
                chargeTime += Time.deltaTime;
                rb.velocity += transform.forward * speed;
                if (chargeTime >= chargeTimer)
                {
                    charge = false;
                    chargeTime = 0;
                }
            }
            if (!charge)
            {
                wait += Time.deltaTime;
            }
            if (wait >= waitTimer)
            {
                chargeAnimTriggered = true;
                charge = true;
                wait = 0;
            }
            Die();
        }
    }
    void Shooting()
    {
        var BulletClone = Instantiate(bullet, shootingPos.transform.position, Quaternion.identity);

        BulletClone.GetComponent<Rigidbody>().AddForce((player.transform.position - shootingPos.transform.position).normalized * bulletSpeed);
    }
    void Die()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
            Debug.Log("electric boss defeated");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Bullet"))
        {
            if ((!animator.GetCurrentAnimatorStateInfo(0).IsName("Intro") || !animator.GetCurrentAnimatorStateInfo(0).IsName("Intro 2")) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                health -= collision.gameObject.GetComponent<bullet>().getDamage();
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerManager>().TakeDamage();
        }
    }
}
