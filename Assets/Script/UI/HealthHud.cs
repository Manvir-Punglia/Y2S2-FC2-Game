using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHud : MonoBehaviour
{
    public GameObject Health3of3;
    public GameObject Health2of3;
    public GameObject Health1of3;
    public GameObject Health0of3;
    public GameObject armour1;
    public GameObject armour2;

    public PlayerManager player;

    public int playerHealth;

    private void Start()
    {
       //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealth = player.GetHealth();

        if (playerHealth == 3)
        {
            Health3of3.SetActive(true);
            Health2of3.SetActive(false);
            Health1of3.SetActive(false);
            Health0of3.SetActive(false);
            armour1.SetActive(false);
            armour2.SetActive(false);

        }
        else if (playerHealth == 2)
        {
            Health3of3.SetActive(false);
            Health2of3.SetActive(true);
            Health1of3.SetActive(false);
            Health0of3.SetActive(false);
            armour1.SetActive(false);
            armour2.SetActive(false);

        }
        else if (playerHealth == 1)
        {
            Health3of3.SetActive(false);
            Health2of3.SetActive(false);
            Health1of3.SetActive(true);
            Health0of3.SetActive(false);
            armour1.SetActive(false);
            armour2.SetActive(false);
        }
        else if(playerHealth == 4)
        {
            Health3of3.SetActive(true);
            Health2of3.SetActive(false);
            Health1of3.SetActive(false);
            Health0of3.SetActive(false);
            armour1.SetActive(true);
            armour2.SetActive(false);
        }
        else if(playerHealth == 5)
        {
            Health3of3.SetActive(true);
            Health2of3.SetActive(false);
            Health1of3.SetActive(false);
            Health0of3.SetActive(false);
            armour1.SetActive(false);
            armour2.SetActive(true);
        }
        else
        {
            Health3of3.SetActive(false);
            Health2of3.SetActive(false);
            Health1of3.SetActive(false);
            Health0of3.SetActive(true);
            armour1.SetActive(false);
            armour2.SetActive(false);
        }

    }
}
