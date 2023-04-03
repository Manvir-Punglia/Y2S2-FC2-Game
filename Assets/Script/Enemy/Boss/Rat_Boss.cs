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
using static Enemy_movement;
using UnityEngine.SceneManagement;

public class Rat_Boss : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    GameObject target;
    public List<GameObject> summonPos;
    public List<GameObject> enemyList;
    Animator animator;

    PlayerManager player;
    bannerManager banner;
    gun Auto, Pistol;

    HitAnimation hitAnim;

    public float health;

    public int bounty;
    bool bountyObtain;

    public bool canSummon;
    public float summonTimer;
    float timer;

    public GameObject dissolve;

    public void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        //banner = GameObject.FindGameObjectWithTag("Player").GetComponent<bannerManager>();
        //Auto = GameObject.FindGameObjectWithTag("auto").GetComponent<gun>();
        //Pistol = GameObject.FindGameObjectWithTag("pistol").GetComponent<gun>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if ((!animator.GetCurrentAnimatorStateInfo(0).IsName("Intro") || !animator.GetCurrentAnimatorStateInfo(0).IsName("Intro 2")) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            transform.LookAt(new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z));
            Die();
            timer += Time.deltaTime;
            if (timer >= summonTimer)
            {
                canSummon = true;
                timer = 0;
            }
            if (canSummon)
            {
                animator.SetTrigger("ATK_AOE");
                for (int i = 0; i < summonPos.Count; i++)
                {
                    if (summonPos[i].GetComponent<SpawnType>().type == SpawnType.spawn.MELEE)
                    {
                        enemyPrefab.GetComponent<Enemy_movement>().type = enemy.MELEE;
                    }
                    else if (summonPos[i].GetComponent<SpawnType>().type == SpawnType.spawn.RANGE)
                    {
                        enemyPrefab.GetComponent<Enemy_movement>().type = enemy.RANGE;
                    }
                    GameObject enemies = Instantiate(enemyPrefab, summonPos[i].transform.position, Random.rotation);
                    enemyList.Add(enemies);
                }
                canSummon = false;
            }
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] == null)
                {
                    enemyList.Remove(enemyList[i]);
                }
            }
        }
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

                PlayerPrefs.SetInt("RatBossDone", 1);

                PlayerPrefs.Save();

                SceneManager.LoadScene("Hub");
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Bullet"))
        {
            if (enemyList.Count > 0)
            {
                StartCoroutine(hitAnim.HitAnim());
                health -= collision.gameObject.GetComponent<bullet>().getDamage();
            }
            else
            {
                Debug.Log("you need to take down the minions first!");
            }
        }
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerManager>().TakeDamage();
            animator.SetTrigger("ATK_Melee");
        }
    }
}
