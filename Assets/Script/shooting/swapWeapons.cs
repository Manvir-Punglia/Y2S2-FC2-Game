using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapWeapons : MonoBehaviour
{
    public List<GameObject> weaponList = new List<GameObject>();
    int index = 0;

    public GameObject auto;
    public GameObject pistol;

    bool hasPistol = false;
    bool hasWaffle = false;
    bool hasAuto = true;
    bool hasGrenade = false;

    private void Start()
    {
        weaponList[0] = auto;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            for(int i = 0; i < weaponList.Count; i++)
            {
                weaponList[index].SetActive(false);
            }

            if(index == weaponList.Count)
            {
                index = 0;
                weaponList[index].SetActive(true);
            }
            else if(index != weaponList.Count)
            {
                index++;
                weaponList[index].SetActive(true);
            }

        }

    }

    public void setPistol(bool hasIt)
    {
        hasPistol = hasIt;
        weaponList.Add(pistol);

    }

    
    public void setAuto(bool hasIt)
    {
        hasAuto = hasIt;
        weaponList.Add(auto);
    }

    public void removePistol()
    {
        hasPistol = false;
        weaponList.Remove(pistol);
    }

    public void removeAuto()
    {
        hasAuto = false;
        weaponList.Remove(auto);
    }

}
