using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using Unity.VisualScripting;
using static Enemy_movement;
using UnityEngine.SceneManagement;

public class Spawn : MonoBehaviour
{
    [SerializeField] List<GameObject> pos;
    [SerializeField] TMP_Text enemyRemaining;
    [SerializeField] GameObject enemyPrefab;
    public List<GameObject> enemyList;
    public bool canSpawn = false;
    public bool triggered = false;
    public int wave;
    public int waveCount = 0;
    public bool key;
    PlayerManager player;
    bannerManager banner;
    gun Auto, Pistol;
    public bool playing;
    public string checkVariable;
    //public GameObject keyPrefab;
    //public GameObject keyPos;

    // Update is called once per frame
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        banner = GameObject.FindGameObjectWithTag("Player").GetComponent<bannerManager>();
        Auto = GameObject.FindGameObjectWithTag("auto").GetComponent<gun>();
        Pistol = GameObject.FindGameObjectWithTag("pistol").GetComponent<gun>();
    }
    void Update()
    {
        if (canSpawn)
        {
            for (int i = 0; i < pos.Count; i++)
            {
                if (pos[i].GetComponent<SpawnType>().type == SpawnType.spawn.MELEE)
                {
                    enemyPrefab.GetComponentInChildren<Enemy_movement>().type = enemy.MELEE;
                }
                else if(pos[i].GetComponent<SpawnType>().type == SpawnType.spawn.RANGE)
                {
                    enemyPrefab.GetComponentInChildren<Enemy_movement>().type = enemy.RANGE;
                }
                GameObject enemies = Instantiate(enemyPrefab, pos[i].transform.position, Random.rotation);
                enemyList.Add(enemies);
                enemies.GetComponentInChildren<Enemy_movement>().player = player;
                enemies.GetComponentInChildren<Enemy_movement>().banner = banner;
                enemies.GetComponentInChildren<Enemy_movement>().Auto = Auto;
                enemies.GetComponentInChildren<Enemy_movement>().Pistol = Pistol;
            }
            canSpawn = false;
            triggered = true;
        }
        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i] == null)
            {
                enemyList.Remove(enemyList[i]);
            }
        }
        if (triggered)
        {
            if (enemyList.Count <= 0 && waveCount < wave)
            {
                canSpawn = true;
                waveCount++;
            }
        }
        if (enemyList.Count > 0)
        {
            playing = true;
        }
        else
        {
            playing = false;
        }
        if (key && triggered && enemyList.Count == 0 && waveCount == wave)
        {
            PlayerPrefs.SetInt("PistolloadedAmmo", 1);
            PlayerPrefs.SetInt("PistolstoredAmmo", 60);

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

            PlayerPrefs.SetInt(checkVariable, 1);

            PlayerPrefs.Save();

            SceneManager.LoadScene("Hub");
        }
        //enemyRemaining.text = string.Format("Enemy Count: {00}", enemyList.Count);
    }
    public void SetCanSpawn(bool spawn)
    {
        canSpawn = spawn;
    }
    public bool GetSpawn()
    {
        return canSpawn;
    }
    public bool GetTrigger()
    {
        return triggered;
    }

    public int getPosCount()
    {
        return pos.Count;
    }
}
