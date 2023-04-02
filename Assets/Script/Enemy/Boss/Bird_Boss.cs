using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird_Boss : MonoBehaviour
{
    [SerializeField] GameObject bullet, shootingPos;
    GameObject target;
    public GameObject[] pos;
    Animator animator;
    Rigidbody rb;
    public Collider hitBox;
    public Collider chargeBox;

    PlayerManager player;
    bannerManager banner;
    gun Auto, Pistol;


    float timer = 0;
    public float teleportT;

    public float health;

    public int bounty;
    bool bountyObtain;

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

    public GameObject dissolve;
    // Start is called before the first frame update
    public void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        //banner = GameObject.FindGameObjectWithTag("Player").GetComponent<bannerManager>();
        //Auto = GameObject.FindGameObjectWithTag("auto").GetComponent<gun>();
        //Pistol = GameObject.FindGameObjectWithTag("pistol").GetComponent<gun>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Die();

        transform.LookAt(target.transform.position);
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
                //if (chargeAnimTriggered)
                //{
                //    animator.SetTrigger("ATK_Melee");
                //    chargeAnimTriggered = false;
                //}
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
                //chargeAnimTriggered = true;
                charge = true;
                wait = 0;
            }
            
        }
    }
    void Shooting()
    {
        var BulletClone = Instantiate(bullet, shootingPos.transform.position, Quaternion.identity);

        BulletClone.GetComponent<Rigidbody>().AddForce((target.transform.position - shootingPos.transform.position).normalized * bulletSpeed);
    }
    void Die()
    {
        if (health <= 0)
        {
            if (!bountyObtain)
            {
                //banner.increaseKillCount();
                //player.GetComponent<PlayerManager>().AddMoney(bounty);
                animator.SetBool("Death", true);
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

                PlayerPrefs.SetInt("BirdBossDone", 1);

                PlayerPrefs.Save();

                SceneManager.LoadScene("Hub");
            }
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
