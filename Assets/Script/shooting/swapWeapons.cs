using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swapWeapons : MonoBehaviour
{
    public List<GameObject> weaponList = new List<GameObject>();
    int index = 0;

    public GameObject auto;
    public GameObject autoModel;
    public GameObject pistol;
    public GameObject pistolModel;
    public GameObject rocketLauncher;
    public GameObject rocketLauncherModel;
    public GameObject shotgun;
    public GameObject shotgunModel;

    GameObject currentGun;

    bool hasPistol = true;
    bool hasShot = true;
    bool hasAuto = true;
    bool hasGrenade = true;

    private void Start()
    {
        currentGun = auto;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (hasAuto && currentGun.GetComponent<gun>().getCanSwap())
            {
                currentGun = auto;
                auto.SetActive(true);
                autoModel.SetActive(true);

                pistol.SetActive(false);
                pistolModel.SetActive(false);

                rocketLauncher.SetActive(false);
                rocketLauncherModel.SetActive(false);

                shotgun.SetActive(false);
                shotgunModel.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (hasPistol && currentGun.GetComponent<gun>().getCanSwap())
            {
                currentGun = pistol;
                auto.SetActive(false);
                autoModel.SetActive(false);

                pistol.SetActive(true);
                pistolModel.SetActive(true);

                rocketLauncher.SetActive(false);
                rocketLauncherModel.SetActive(false);

                shotgun.SetActive(false);
                shotgunModel.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (hasShot && currentGun.GetComponent<gun>().getCanSwap())
            {
                currentGun = shotgun;
                auto.SetActive(false);
                autoModel.SetActive(false);

                pistol.SetActive(false);
                pistolModel.SetActive(false);

                rocketLauncher.SetActive(false);
                rocketLauncherModel.SetActive(false);

                shotgun.SetActive(true);
                shotgunModel.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (hasGrenade && currentGun.GetComponent<gun>().getCanSwap())
            {
                currentGun = rocketLauncher;
                auto.SetActive(false);
                autoModel.SetActive(false);

                pistol.SetActive(false);
                pistolModel.SetActive(false);

                rocketLauncher.SetActive(true);
                rocketLauncherModel.SetActive(true);

                shotgun.SetActive(false);
                shotgunModel.SetActive(false);
            }
        }

    }

    public void addGun(string gun)
    {
        switch (gun)
        {
            case "Pistol":
                hasPistol = true;
                break;

            case "Shotgun":
                hasShot = true;
                break;

            case "Rocket Launcher":
                hasGrenade = true;
                break;

            case "Auto":
                hasAuto = true;
                break;
        }
        
    }

    
    public void removeGun(string gun)
    {
        switch (gun)
        {
            case "Pistol":
                hasPistol = false;
                break;

            case "Shotgun":
                hasShot = false;
                break;

            case "Rocket Launcher":
                hasGrenade = false;
                break;

            case "Auto":
                hasAuto = false;
                break;
        }
    }


}
