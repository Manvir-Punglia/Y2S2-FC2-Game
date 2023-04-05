using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public float damage = 1;
    public float maxdamage = 1;

    float destroyTimer = 0f;
    public float destroyTime = 3f;
    public GameObject hit_prefab;
    public bannerManager banner;

    PlayerManager playerManager;
 
    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        banner = GameObject.FindGameObjectWithTag("Player").GetComponent<bannerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.tag == "Bullet" && banner != null)
        {
            damage = maxdamage + banner.getStats(0);
        }

        destroyTimer += Time.deltaTime;
        if (destroyTimer >= destroyTime)
        {
            DestroyImmediate(this.gameObject, true);
            //Destroy(bullet_prefab);
            //Destroy(hit_prefab);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(this.tag != "Bullet")
            {
                playerManager.TakeDamage();
            }
            

        }
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
        }

        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;

        if (hit_prefab != null)
        {
            var hit = Instantiate(hit_prefab, pos, rotation);

            Destroy(hit, 3f);
        }

        if(collision.gameObject.tag == "enemy")
        {
            collision.gameObject.GetComponent<Enemy_movement>().takeDamage(damage);
        }

        Destroy(this.gameObject);
    }

    public float getDamage()
    {
        return damage;
    }
}
