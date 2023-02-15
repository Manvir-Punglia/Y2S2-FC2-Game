using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class banner : MonoBehaviour
{
    int bannerAmount = 0;

    int damageIncrease = 0;
    int moveSpeedIncrease = 0;
    int healthIncrease = 0;
    float fireRateIncrease = 0;

    public bannerManager manager;

    public float getLevel()
    {
        return bannerAmount;
    }

    public void bannerUpgrade(int enemyCount, string resetType)
    {
        switch (bannerAmount)
        {
            case 0:
                if(enemyCount == 15)
                {
                    bannerAmount = 1;
                    manager.resetKillCount(resetType);
                }
                break;
                

            case 1:
                if(enemyCount == 15)
                {
                    bannerAmount = 2;
                    manager.resetKillCount(resetType);
                }
                break;

            case 2:
                if(enemyCount == 15)
                {
                    manager.resetKillCount(resetType);
                }
                break;

            

        }

        switch (bannerAmount)
        {
            case 0:
                statChange(0, 0, 0, 0);
                break;
            case 1:
                statChange(1, 0, 100, 0.05f);
                break;
            case 2:
                statChange(2, 0, 4, 0.15f);
                break;
            case 3:
                statChange(3, 0, 6, 0.2f);
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

    private void Update()
    {

        //Debug.Log(bannerAmount);
    }
}
