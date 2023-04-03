using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorTransportation : MonoBehaviour
{
    public PlayerManager player;
    public gun Auto;
    public gun Pistol;
    public bannerManager banner;
    public string Scenename;
    public string BossSceneName;
    public string checkVariable;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        banner = GameObject.FindGameObjectWithTag("Player").GetComponent<bannerManager>();
        Auto = GameObject.FindGameObjectWithTag("auto").GetComponent<gun>();
        //Pistol = GameObject.FindGameObjectWithTag("pistol").GetComponent<gun>();
    }
        

    // Update is called once per frame
    void Update()
    {
        //Pistol = GameObject.FindGameObjectWithTag("pistol").GetComponent<gun>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //PlayerPrefs.SetInt("PistolloadedAmmo", 1);
           // PlayerPrefs.SetInt("PistolstoredAmmo", 60);
            //PlayerPrefs.SetInt("Ammo", 5);
            //PlayerPrefs.SetFloat("Kills/banner ", 0.6f);
            //PlayerPrefs.SetString("username", "John Doe");
            //PlayerPrefs.Save();
            PlayerPrefs.SetInt("health", player.GetHealth());
            PlayerPrefs.SetInt("money", player.GetMoney());

            PlayerPrefs.SetFloat("fireBanner", banner.getAmount("Fire"));
            PlayerPrefs.SetFloat("waterBanner", banner.getAmount("Water"));
            PlayerPrefs.SetFloat("poisonBanner", banner.getAmount("Poison"));
            PlayerPrefs.SetFloat("lightningBanner", banner.getAmount("Lightning"));

            PlayerPrefs.SetFloat("fireKills", banner.getKillCount("Fire"));
            PlayerPrefs.SetFloat("waterKills", banner.getKillCount("Water"));
            PlayerPrefs.SetFloat("poisonKills", banner.getKillCount("Poison"));
            PlayerPrefs.SetFloat("lightningKills", banner.getKillCount("Lightning"));

            PlayerPrefs.SetInt("AutoloadedAmmo", Auto.getCurrAmmo());
            PlayerPrefs.SetInt("AutostoredAmmo", Auto.getStoredAmmo());
            PlayerPrefs.SetInt("PistolloadedAmmo", Pistol.getCurrAmmo());
            PlayerPrefs.SetInt("PistolstoredAmmo", Pistol.getStoredAmmo());

            PlayerPrefs.SetInt("hasRun", 1);

            PlayerPrefs.Save();

            if (PlayerPrefs.GetInt(checkVariable) == 1)
            {
                SceneManager.LoadScene(BossSceneName);
            }
            else
            {
                SceneManager.LoadScene(Scenename);
            }
        }
    }

}