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


    public float jumpTime;
    public float jumpHeight;
    bool ground;

    public float speed;

    public float health;

    public int bounty;
    bool bountyObtain;

    public float bulletSpeed;
    public float shootTimer;
    float canShootTimer;
    bool canShoot;
    public float rangeDelay;

    public GameObject dissolve;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        banner = GameObject.FindGameObjectWithTag("Player").GetComponent<bannerManager>();
        Auto = GameObject.FindGameObjectWithTag("auto").GetComponent<gun>();
        Pistol = GameObject.FindGameObjectWithTag("pistol").GetComponent<gun>();
        animator = GetComponent<Animator>();
        
        ground = true;
        
    }

    void Update()
    {
        if ((!animator.GetCurrentAnimatorStateInfo(0).IsName("Intro")))
        {
            
            StartCoroutine(Jump(jumpTime));

            canShootTimer += Time.deltaTime;
            if (canShoot)
            {
                animator.SetTrigger("ATK_Range");
                StartCoroutine(Range(animator.GetCurrentAnimatorStateInfo(0).length * rangeDelay));

                canShoot = false;

            }
            if (canShootTimer >= shootTimer)
            {
                canShoot = true;
                canShootTimer = 0;
            }
            Die();
            if (!ground)
            {
                animator.SetTrigger("ATK_AOE");
                transform.LookAt(new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z));
                _rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);
            }
        }
    }
    public void Die()
    {
        if (health <= 0)
        {
            _rb.velocity = Vector3.zero;
            if (!bountyObtain)
            {
                animator.SetTrigger("Death");
                banner.increaseKillCount("Water");
                target.GetComponent<PlayerManager>().AddMoney(bounty);
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
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            ground = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            ground = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage();
        }
        if (collision.gameObject.tag == ("Bullet"))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Intro"))
            {
                health -= collision.gameObject.GetComponent<bullet>().damage;
                _rb.velocity = Vector3.zero;
            }
        }
    }
    IEnumerator Jump(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (ground)
        {
            _rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
    IEnumerator Range(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger("ATK_Range");
        if (bullet.GetComponent<VisualEffect>() != null)
        {
            Debug.LogWarning("shot");
            bullet.GetComponent<VisualEffect>().Play();
        }
        var BulletClone = Instantiate(bullet, shootingPos.transform.position, Quaternion.identity);
        BulletClone.GetComponent<Rigidbody>().AddForce((target.transform.position - shootingPos.transform.position).normalized * bulletSpeed);
    }
}
