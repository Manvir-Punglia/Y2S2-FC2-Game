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
    public List<GameObject> wall;
    public bool canSpawn = false;
    public bool triggered = false;
    public int wave;
    public int waveCount = 0;
    public bool key;
    //public GameObject keyPrefab;
    //public GameObject keyPos;

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            for (int i = 0; i < pos.Count; i++)
            {
                if (pos[i].GetComponent<SpawnType>().type == SpawnType.spawn.MELEE)
                {
                    enemyPrefab.GetComponent<Enemy_movement>().type = enemy.MELEE;
                }
                else if(pos[i].GetComponent<SpawnType>().type == SpawnType.spawn.RANGE)
                {
                    enemyPrefab.GetComponent<Enemy_movement>().type = enemy.RANGE;
                }
                GameObject enemies = Instantiate(enemyPrefab, pos[i].transform.position, Random.rotation);
                enemyList.Add(enemies);
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
            for (int i = 0; i < wall.Count; i++)
            {
                wall[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < wall.Count; i++)
            {
                wall[i].SetActive(false);
            }
        }
        if (key && triggered && enemyList.Count == 0 && waveCount == wave)
        {
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
