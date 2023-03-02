using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadStats : MonoBehaviour
{
    Scene scene;
    string sceneName;
    public PlayerManager player;
    public gun Gun;
    public bannerManager banner;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        banner = GameObject.FindGameObjectWithTag("Player").GetComponent<bannerManager>();
        Gun = GameObject.FindGameObjectWithTag("gun").GetComponent<gun>();

        player.SetHealth(PlayerPrefs.GetInt("health"));
        player.SetMoney(PlayerPrefs.GetInt("money"));

        banner.hardSetKillCount("Fire", PlayerPrefs.GetInt("fireKills"));
        banner.hardSetKillCount("Water", PlayerPrefs.GetInt("waterKills"));
        banner.hardSetKillCount("Poison", PlayerPrefs.GetInt("poisonKills"));
        banner.hardSetKillCount("Lightning", PlayerPrefs.GetInt("lightningKills"));

        banner.setBannerAmount("Fire", PlayerPrefs.GetInt("fireBanner"));
        banner.setBannerAmount("Water", PlayerPrefs.GetInt("waterBanner"));
        banner.setBannerAmount("Poison", PlayerPrefs.GetInt("poisonBanner"));
        banner.setBannerAmount("Lightning", PlayerPrefs.GetInt("lightningBanner"));

        Gun.hardSetCurrAmmo(PlayerPrefs.GetInt("loadedAmmo"));
        Gun.hardSetUnloadedAmmo(PlayerPrefs.GetInt("storedAmmo"));

        if (sceneName == "Dungeon1")
        {
            //int score = PlayerPrefs.GetInt("score");
            //float volume = PlayerPrefs.GetFloat("volume");
            //string player = PlayerPrefs.GetString("username");

            player.SetHealth(PlayerPrefs.GetInt("health"));
            player.SetMoney(PlayerPrefs.GetInt("money"));

            banner.hardSetKillCount("Fire", PlayerPrefs.GetInt("fireKills"));
            banner.hardSetKillCount("Water", PlayerPrefs.GetInt("waterKills"));
            banner.hardSetKillCount("Poison", PlayerPrefs.GetInt("poisonKills"));
            banner.hardSetKillCount("Lightning", PlayerPrefs.GetInt("lightningKills"));

            banner.setBannerAmount("Fire", PlayerPrefs.GetInt("fireBanner"));
            banner.setBannerAmount("Water", PlayerPrefs.GetInt("waterBanner"));
            banner.setBannerAmount("Poison", PlayerPrefs.GetInt("poisonBanner"));
            banner.setBannerAmount("Lightning", PlayerPrefs.GetInt("lightningBanner"));

            Gun.hardSetCurrAmmo(PlayerPrefs.GetInt("loadedAmmo"));
            Gun.hardSetUnloadedAmmo(PlayerPrefs.GetInt("storedAmmo"));

        }
        else if (sceneName == "Hub" && PlayerPrefs.GetInt("hasRun") == 1)
        {

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
