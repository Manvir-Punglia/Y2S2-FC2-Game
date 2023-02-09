using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bannerManager : MonoBehaviour
{

    int enemyCountF = 0;
    int enemyCountW = 0;
    int enemyCountL = 0;
    int enemyCountP = 0;

    public banner fireBanner;
    public banner waterBanner;
    public banner poisonBanner;
    public banner lightningBanner;
    

    void Start()
    {

    }


    void Update()
    {
        fireBanner.bannerUpgrade(enemyCountF);
        waterBanner.bannerUpgrade(enemyCountW);
        poisonBanner.bannerUpgrade(enemyCountP);
        lightningBanner.bannerUpgrade(enemyCountL);
    }

    public float getStats(int stat)
    {
        if (stat <= 0)
        {
            float damageIncrease = fireBanner.getStats(0) + waterBanner.getStats(0) + poisonBanner.getStats(0) + lightningBanner.getStats(0);
            return damageIncrease;
        }
        else if (stat == 1)
        {
            float healthIncrease = fireBanner.getStats(1) + waterBanner.getStats(1) + poisonBanner.getStats(1) + lightningBanner.getStats(1);
            return healthIncrease;
        }
        else if (stat == 2)
        {
            float moveSpeedIncrease = fireBanner.getStats(2) + waterBanner.getStats(2) + poisonBanner.getStats(2) + lightningBanner.getStats(2);
            return moveSpeedIncrease;
        }
        else if (stat >= 3)
        {
            float fireRateIncrease = fireBanner.getStats(3) + waterBanner.getStats(3) + poisonBanner.getStats(3) + lightningBanner.getStats(3);
            return fireRateIncrease;
        }
        else
        {
            return 0;
        }
    }
    public void increaseKillCount()
    {
        enemyCountF++;
    }

    public int getKillCount()
    {
        return enemyCountF;
    }

}
