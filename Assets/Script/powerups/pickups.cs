using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickups : MonoBehaviour
{
    gun Gun;
    PlayerManager playerManager;
    public pickUpType type = pickUpType.HEALTH;

    public enum pickUpType
    {
        HEALTH,
        AMMO,
    }

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        Gun = GameObject.FindGameObjectWithTag("gun").GetComponent<gun>();
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.LogError("collided with player");
            switch (type)
            {
                case pickUpType.HEALTH:
                    {
                        if(playerManager.GetHealth() < 3)
                        {
                            playerManager.SetHealth(playerManager.GetHealth() + 1);
                        }
                        
                        break;
                    }

                case pickUpType.AMMO:
                    {
                        Gun.setAmmo(10);
                        break;
                    }
            }

            Destroy(gameObject);
        }
    }
}
