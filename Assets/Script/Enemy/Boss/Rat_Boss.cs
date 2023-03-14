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

public class Rat_Boss : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    public GameObject player;
    public List<GameObject> summonPos;
    public List<GameObject> enemyList;
    Animator animator;

    HitAnimation hitAnim;

    public float health;

    public int bounty;
    bool bountyObtain;

    public bool canSummon;
    public float summonTimer;
    float timer;

    public void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        Die();
        timer += Time.deltaTime;
        if (timer >= summonTimer)
        {
            canSummon = true;
            timer = 0;
        }
        if (canSummon)
        {
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
    void Die()
    {
        if (health <= 0)
        {
            //if (!bountyObtain)
            //{
            //    //banner.increaseKillCount();
            //    //player.GetComponent<PlayerManager>().AddMoney(bounty);
            //    animator.SetBool("Death", true);
            //    bountyObtain = true;
            //}
            //Destroy(this.gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
            Destroy(this.gameObject);
            Debug.Log("rat boss defeated");
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
            //melee animation;
        }
    }
}
