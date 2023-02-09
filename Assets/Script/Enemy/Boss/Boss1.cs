using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss1 : MonoBehaviour
{
    [SerializeField] GameObject bullet, shootingPos;
    public GameObject player, boss;
    NavMeshAgent agent;

    bool canHit = false;
    bool canShoot = true;
    float canShootTimer = 0f;
    float shootTimer = 0f;
    float bulletSpeed = 0f;

    GameObject fireballParticles;
    public float hitTime;
    float time = 0;

    public float health;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }
    void Shooting()
    {
        var BulletClone = Instantiate(bullet, shootingPos.transform.position, Quaternion.identity);
        var FireballClone = Instantiate(fireballParticles, BulletClone.transform.position, Quaternion.identity);
        FireballClone.transform.parent = BulletClone.transform;
        if (canShoot)
        {
            canShoot = false;
            BulletClone.GetComponent<Rigidbody>().AddForce((player.transform.position - shootingPos.transform.position).normalized * bulletSpeed);
        }
        canShootTimer += Time.deltaTime;
        if (canShootTimer >= shootTimer)
        {
            canShoot = true;
            canShootTimer = 0;
        }
    }
}
