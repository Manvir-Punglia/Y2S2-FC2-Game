using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class banner : MonoBehaviour
{
    public string bannerType;

    int bannerAmount = 0;

    int damageIncrease = 0;
    int moveSpeedIncrease = 0;
    int healthIncrease = 0;
    float fireRateIncrease = 0;

    public bannerManager manager;

    PlayerManager player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }
    public float getLevel()
    {
        return bannerAmount;
    }

    public void bannerUpgrade(int enemyCount, string resetType)
    {
        switch (bannerAmount)
        {
            case 0:
                if(enemyCount >= 15)
                {
                    bannerAmount = 1;
                    manager.resetKillCount(resetType);
                    manager.setString(bannerType);
                    if(bannerType == "Water")
                    {
                        player.SetHealth(player.GetHealth() + 1);
                    }
                }
                break;
                

            case 1:
                if(enemyCount >= 15)
                {
                    bannerAmount = 2;
                    manager.resetKillCount(resetType);
                    manager.setString(bannerType);
                    if (bannerType == "Water")
                    {
                        player.SetHealth(player.GetHealth() + 1);
                    }
                }
                break;

            case 2:
                if(enemyCount >= 15)
                {
                    manager.resetKillCount(resetType);
                    manager.setString(bannerType);
                    if (bannerType == "Water" && player.GetHealth()<5)
                    {
                        player.SetHealth(player.GetHealth() + 1);
                    }
                }
                break;
        }

        switch (bannerType)
        {
            case "Fire":
                switch (bannerAmount)
                {
                    case 0:
                        statChange(0, 0, 0, 0);
                        break;
                    case 1:
                        statChange(0, 0, 0, 0.05f);
                        break;
                    case 2:
                        statChange(0, 0, 0, 0.15f);
                        break;
                    case 3:
                        statChange(0, 0, 0, 0.2f);
                        break;
                }
                break;

            case "Water":
                switch (bannerAmount)
                {
                    case 0:
                        statChange(0, 0, 0, 0);
                        break;
                    case 1:
                        statChange(0, 1, 0, 0.0f);
                        break;
                    case 2:
                        statChange(0, 2, 0, 0.0f);
                        break;
                    case 3:
                        statChange(0, 3, 6, 0.0f);
                        break;
                }
                break;

            case "Poison":
                switch (bannerAmount)
                {
                    case 0:
                        statChange(0, 0, 0, 0);
                        break;
                    case 1:
                        statChange(1, 0, 0, 0.0f);
                        break;
                    case 2:
                        statChange(2, 0, 0, 0.0f);
                        break;
                    case 3:
                        statChange(3, 0, 0, 0.0f);
                        break;
                }
                break;

            case "Lightning":
                switch (bannerAmount)
                {
                    case 0:
                        statChange(0, 0, 0, 0);
                        break;
                    case 1:
                        statChange(0, 0, 10, 0.0f);
                        break;
                    case 2:
                        statChange(0, 0, 20, 0.0f);
                        break;
                    case 3:
                        statChange(0, 0, 30, 0.0f);
                        break;
                }
                break;
        }
        
    }

    public void statChange(int dmg, int hp, int moveSpeed, float fireRate)
    {
        damageIncrease = dmg;
        healthIncrease = hp;
        moveSpeedIncrease = moveSpeed;
        fireRateIncrease = fireRate;
    }

    public float getStats(int stat)
    {
        if (stat <= 0)
        {
            return damageIncrease;
        }
        else if (stat == 1)
        {
            return healthIncrease;
        }
        else if (stat == 2)
        {
            return moveSpeedIncrease;
        }
        else if (stat >= 3)
        {
            return fireRateIncrease;
        }
        else
        {
            return 0;
        }
    }


    public void increaseBannerAmount()
    {
        bannerAmount++;
    }

    public void decreaseBannerAmount()
    {
        if (bannerAmount > 0)
        {
            bannerAmount--;
        }


    }

    public void setBannerAmount(int newAmount)
    {
        bannerAmount = newAmount;
    }
    private void Update()
    {

        //Debug.Log(bannerAmount);
    }
}
