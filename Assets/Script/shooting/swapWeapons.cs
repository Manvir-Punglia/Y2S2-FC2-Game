using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapWeapons : MonoBehaviour
{
    public List<GameObject> weaponList = new List<GameObject>();
    int index = 0;

    bool hasPistol = true;
    bool hasWaffle = false;
    bool hasAuto = false;
    bool hasGrenade = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            
            if (weaponList.Count > index+1 && weaponList[index + 1] != null && index < 3)
            {
                weaponList[index].gameObject.SetActive(false);
                weaponList[index+1].gameObject.SetActive(true);
                index = index + 1;
            }

            else if(weaponList[0] != null && index == 3)
            {
                weaponList[index].gameObject.SetActive(false);
                weaponList[0].gameObject.SetActive(true);
                index = 0;
            }
            Debug.LogError(index);
        }
    }

    public void setPistol(bool hasIt)
    {
        hasPistol = hasIt;
    }

    public void setWaffle(bool hasIt)
    {
        hasWaffle = hasIt;
    }

    public void setAuto(bool hasIt)
    {
        hasAuto = hasIt;
    }

    public void setGrenade(bool hasIt)
    {
        hasGrenade = hasIt;
    }
}
