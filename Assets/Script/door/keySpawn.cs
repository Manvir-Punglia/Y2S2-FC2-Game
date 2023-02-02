using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keySpawn : MonoBehaviour
{
    bannerManager _banner;
    public Spawn _spawn;
    public GameObject keySpawner;
    public GameObject key;
    public GameObject keyText;

    bool canSpawn = true;
    void Start()
    {
        _banner = GameObject.FindGameObjectWithTag("Player").GetComponent<bannerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.LogError(_banner.getKillCount());
        if (canSpawn)
        {
            if (_banner.getKillCount() == _spawn.getPosCount())
            {
                Instantiate(key, keySpawner.transform.position, keySpawner.transform.rotation);
                keyText.SetActive(true);
                canSpawn = false;
            }
        }
        
    }
}
