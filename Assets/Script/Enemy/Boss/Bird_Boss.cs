using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Boss : MonoBehaviour
{
    [SerializeField] GameObject bullet, shootingPos;
    public GameObject player;
    public GameObject[] pos;
    Animator animator;

    bannerManager banner;

    HitAnimation hitAnim;

    float timer = 0;
    public float teleportT;

    public float health;


    bool canShoot;
    public float bulletSpeed;
    public float shootTimer;
    float canShootTimer;
    // Start is called before the first frame update
    public void Awake()
    {
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookTo = player.transform.position;
        transform.LookAt(lookTo);
        if ((!animator.GetCurrentAnimatorStateInfo(0).IsName("Intro") || !animator.GetCurrentAnimatorStateInfo(0).IsName("Intro 2")) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
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
                StartCoroutine(hitAnim.HitAnim());
                health -= collision.gameObject.GetComponent<bullet>().getDamage();
            }

        }
    }
}
