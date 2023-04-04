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

    public GameObject fireProgress;
    public GameObject waterProgress;
    public GameObject poisonProgress;
    public GameObject lightningProgress;
    public GameObject bannerUpgradeText;

    bool canShowProgress = true;
    bool willShowProgress = false;

    string justUpgraded;

    void Start()
    {

    }


    void Update()
    {

        if (canShowProgress && willShowProgress)
        {
            StartCoroutine(showUpgrade(justUpgraded));
            StopCoroutine(showUpgrade(justUpgraded));
        }

        fireBanner.bannerUpgrade(enemyCountF, "Fire");
        waterBanner.bannerUpgrade(enemyCountW, "Water");
        poisonBanner.bannerUpgrade(enemyCountP, "Poison");
        lightningBanner.bannerUpgrade(enemyCountL, "Lightning");

        if (Input.GetKeyDown(KeyCode.L))
        {
            enemyCountW += 10;
            //Debug.Log(enemyCountW);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            enemyCountF += 10;
            //Debug.Log(enemyCountW);
        }
    }

    public bool getHasAnyBanners()
    {
        if (fireBanner.getLevel() != 0 || waterBanner.getLevel() != 0 || poisonBanner.getLevel() != 0 || lightningBanner.getLevel() != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float getAmount(string bannerType)
    {
        switch (bannerType)
        {
            case "Fire":
                return fireBanner.getLevel();
            case "Water":
                return waterBanner.getLevel();
            case "Poison":
                return poisonBanner.getLevel();
            case "Lightning":
                return lightningBanner.getLevel();
        }

        return 0;
    }

    public void decrease(string type)
    {
        switch (type)
        {
            case "Fire":
                fireBanner.decreaseBannerAmount();
                break;
            case "Water":
                waterBanner.decreaseBannerAmount();
                break;
            case "Poison":
                poisonBanner.decreaseBannerAmount();
                break;
            case "Lightning":
                lightningBanner.decreaseBannerAmount();
                break;
        }

    }

    public void setBannerAmount(string type, float level)
    {
        switch (type)
        {
            case "Fire":
                fireBanner.setBannerAmount(level);
                break;
            case "Water":
                waterBanner.setBannerAmount(level);
                break;
            case "Poison":
                poisonBanner.setBannerAmount(level);
                break;
            case "Lightning":
                lightningBanner.setBannerAmount(level);
                break;
        }
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
    public void increaseKillCount(string type)
    {
        switch(type)
        {
            case "Fire":
                enemyCountF++;
                break;

            case "Water":
                enemyCountW++;
                break;

            case "Lightning":
                enemyCountL++;
                break;

            case "Poison":
                enemyCountP++;
                break;
        }
    }

    public int getKillCount(string enemyType)
    {
        switch (enemyType)
        {
            case "Fire":
                return enemyCountF;

            case "Water":
                return enemyCountW;

            case "Poison":
                return enemyCountP;

            case "Lightning":
                return enemyCountL;
        }
        return 0;
    }

    public void setKillCount(string enemyType)
    {
        switch (enemyType)
        {
            case "Fire":
                enemyCountF++;
                break;

            case "Water":
                enemyCountW++;
                break;
            case "Poison":
                enemyCountP++;
                break;
            case "Lightning":
                enemyCountL++;
                break;
        }
    }

    public void hardSetKillCount(string enemyType, int kills)
    {
        switch (enemyType)
        {
            case "Fire":
                enemyCountF = kills;
                break;

            case "Water":
                enemyCountW = kills;
                break;
            case "Poison":
                enemyCountP = kills;
                break;
            case "Lightning":
                enemyCountL = kills;
                break;
        }
    }

    public void resetKillCount(string enemyType)
    {
        switch (enemyType)
        {
            case "Fire":
                enemyCountF = 0;
                break;

            case "Water":
                enemyCountW = 0;
                break;
            case "Poison":
                enemyCountP = 0;
                break;
            case "Lightning":
                enemyCountL = 0;
                break;
        }
    }

    IEnumerator showUpgrade(string bannerType)
    {
        if (canShowProgress)
        {
            canShowProgress = false;

            switch (bannerType)
            {
                case "Fire":
                    fireProgress.SetActive(true);
                    break;

                case "Water":
                    waterProgress.SetActive(true);
                    break;

                case "Poison":
                    poisonProgress.SetActive(true);
                    break;

                case "Lightning":
                    lightningProgress.SetActive(true);
                    break;
            }
            bannerUpgradeText.SetActive(true);
            yield return new WaitForSeconds(5);

            fireProgress.SetActive(false);
            waterProgress.SetActive(false);
            poisonProgress.SetActive(false);
            lightningProgress.SetActive(false);
            bannerUpgradeText.SetActive(false);

            canShowProgress = true;
            willShowProgress = false;
        }
    }

    public void setString(string type)
    {
        justUpgraded = type;
        willShowProgress = true;
    }

}
