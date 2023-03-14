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
    NavMeshAgent agent;
    Animator animator;

    bannerManager banner;

    HitAnimation hitAnim;

    //public ParticleSystem fireballParticles;

    public bool hit;

    public float runDistance;
    public float shootingDistance;

    public float hitTime;
    float time = 0;

    public float stompDistance;
    public float stompTime;
    float stompTimer = 0;

    public float health;

    public int bounty;
    bool bountyObtain;

    public bool canShoot;
    public float bulletSpeed;
    public float shootTimer;
    float canShootTimer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Die()
    {
        if (health <= 0)
        {
            hit = false;
            if (!bountyObtain)
            {
                //banner.increaseKillCount();
                //player.GetComponent<PlayerManager>().AddMoney(bounty);
                animator.SetBool("Death", true);
                bountyObtain = true;
            }
            Destroy(this.gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
            //Destroy(gameObject);
        }
    }
}
