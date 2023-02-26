using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric_Boss : MonoBehaviour
{
    [SerializeField] GameObject bullet, shootingPos;
    public GameObject player;
    public GameObject[] pos;
    Animator animator;

    bannerManager banner;

    HitAnimation hitAnim;

    //public ParticleSystem ;
    float timer = 0;
    public float teleportT;

    public float health;

    public int bounty;
    bool bountyObtain;

    bool canShoot;
    public float bulletSpeed;
    public float shootTimer;
    float canShootTimer;
    // Start is called before the first frame update
    public void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
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
    void Shooting()
    {
        var BulletClone = Instantiate(bullet, shootingPos.transform.position, Quaternion.identity);
        //var FireballClone = Instantiate(fireballParticles, BulletClone.transform.position, Quaternion.identity);
        //FireballClone.transform.parent = BulletClone.transform;

        BulletClone.GetComponent<Rigidbody>().AddForce((player.transform.position - shootingPos.transform.position).normalized * bulletSpeed);
        //fireballParticles.Play();
        //animator.SetBool("ATK_Range", true);
        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("ATK_Range"))
        //{
        //    agent.isStopped = true;
        //}
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
            Debug.Log("electric boss defeated");
        }
    }
}
