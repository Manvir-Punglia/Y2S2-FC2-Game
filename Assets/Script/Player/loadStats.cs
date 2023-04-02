using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadStats : MonoBehaviour
{
    Scene scene;
    string sceneName;
    public PlayerManager player;
    public gun Auto;
    public gun Pistol;
    public bannerManager banner;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        sceneName = scene.name;
        //Debug.LogError(scene.name);
        Pistol.gameObject.SetActive(true);
        Auto.gameObject.SetActive(true);
        

        if (scene.name != "Hub")
        {
            //Debug.LogError("dungeon 1 loaded");
            //Debug.LogError(PlayerPrefs.GetInt("AutoloadedAmmo"));

            //int score = PlayerPrefs.GetInt("score");
            //float volume = PlayerPrefs.GetFloat("volume");
            //string player = PlayerPrefs.GetString("username");

            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
            banner = GameObject.FindGameObjectWithTag("Player").GetComponent<bannerManager>();
            

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

            Auto.hardSetCurrAmmo(PlayerPrefs.GetInt("AutoloadedAmmo"));
            Auto.hardSetUnloadedAmmo(PlayerPrefs.GetInt("AutostoredAmmo"));

            Pistol.hardSetCurrAmmo(PlayerPrefs.GetInt("PistolloadedAmmo"));
            Pistol.hardSetUnloadedAmmo(PlayerPrefs.GetInt("PistolstoredAmmo"));

        }
        else if (sceneName == "Hub" && PlayerPrefs.GetInt("hasRun") == 1)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
            banner = GameObject.FindGameObjectWithTag("Player").GetComponent<bannerManager>();
            //Auto = GameObject.FindGameObjectWithTag("auto").GetComponent<gun>();
            //Pistol = GameObject.FindGameObjectWithTag("pistol").GetComponent<gun>();

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

            Auto.hardSetCurrAmmo(PlayerPrefs.GetInt("AutoloadedAmmo"));
            Auto.hardSetUnloadedAmmo(PlayerPrefs.GetInt("AutostoredAmmo"));

            Pistol.hardSetCurrAmmo(PlayerPrefs.GetInt("PistolloadedAmmo"));
            Pistol.hardSetUnloadedAmmo(PlayerPrefs.GetInt("PistolstoredAmmo"));
        }

        Pistol.gameObject.SetActive(false);
        Auto.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
