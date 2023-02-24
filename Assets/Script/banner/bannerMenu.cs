using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bannerMenu : MonoBehaviour
{
    public bannerManager player;
    public GameObject fireBanner0;
    public GameObject fireBanner1;
    public GameObject waterBanner0;
    public GameObject waterBanner1;
    public GameObject poisonBanner0;
    public GameObject poisonBanner1;
    public GameObject lightningBanner0;
    public GameObject lightningBanner1;

    public Text fireEnemyLeft;
    public Text waterEnemyLeft;
    public Text poisonEnemyLeft;
    public Text lightningEnemyLeft;

    // Update is called once per frame
    void Update()
    {
        fireEnemyLeft.text = (15 - player.getKillCount("Fire")).ToString();
        waterEnemyLeft.text = (15 - player.getKillCount("Water")).ToString();
        poisonEnemyLeft.text = (15 - player.getKillCount("Poison")).ToString();
        lightningEnemyLeft.text = (15 - player.getKillCount("Lightning")).ToString();

        if(player.getAmount("Fire") == 0)
        {
            fireBanner0.SetActive(true);
            fireBanner1.SetActive(false);
        }
        if(player.getAmount("Fire") > 0)
        {
            fireBanner0.SetActive(false);
            fireBanner1.SetActive(true);
        }

        if (player.getAmount("Water") == 0)
        {
            waterBanner0.SetActive(true);
            waterBanner1.SetActive(false);
        }
        if (player.getAmount("Water") > 0)
        {
            waterBanner0.SetActive(false);
            waterBanner1.SetActive(true);
        }

        if (player.getAmount("Poison") == 0)
        {
            poisonBanner0.SetActive(true);
            poisonBanner1.SetActive(false);
        }
        if (player.getAmount("Poison") > 0)
        {
            poisonBanner0.SetActive(false);
            poisonBanner1.SetActive(true);
        }

        if (player.getAmount("Lightning") == 0)
        {
            lightningBanner0.SetActive(true);
            lightningBanner1.SetActive(false);
        }
        if (player.getAmount("Lightning") > 0)
        {
            lightningBanner0.SetActive(false);
            lightningBanner1.SetActive(true);
        }


    }
}
