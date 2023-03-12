using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class doorTransportation : MonoBehaviour
{
    public PlayerManager player;
    public gun Gun;
    public bannerManager banner;
    public string Scenename;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        banner = GameObject.FindGameObjectWithTag("Player").GetComponent<bannerManager>();
        Gun = GameObject.FindGameObjectWithTag("gun").GetComponent<gun>();
    }
        

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(Scenename);
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

            PlayerPrefs.SetInt("loadedAmmo", Gun.getCurrAmmo());
            PlayerPrefs.SetInt("storedAmmo", Gun.getStoredAmmo());
                     
            PlayerPrefs.SetInt("hasRun", 1);

            PlayerPrefs.Save();
        }
    }

}