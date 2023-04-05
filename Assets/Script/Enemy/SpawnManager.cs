using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> spawner;
    public List<GameObject> wall;
    float count;
    void Update()
    {
        count = 0;
        for (int i = 0; i < spawner.Count; i++)
        {
            if (spawner[i].GetComponent<Spawn>().playing)
            {
                count++;
            }
        }
        if (count > 0)
        {
            updateWall(true);
        }
        else
        {
            updateWall(false);
        }
    }
    void updateWall(bool update)
    {
        for (int i = 0; i < wall.Count; i++)
        {
            wall[i].SetActive(update);
        }
    }
}
