using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoHudCount : MonoBehaviour
{
    public GameObject CurrentAmmoObj;
    public GameObject ReserveAmmoObj;

    public Text currentAmmo;
    public Text reserveAmmo;

    public int curAmmo;
    public int resAmmo;

    //public gun Gun;
    public swapWeapons player;
    // Start is called before the first frame update
    void Start()
    {
        //Gun = GameObject.FindGameObjectWithTag("gun").GetComponent<gun>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<swapWeapons>();

        currentAmmo = CurrentAmmoObj.GetComponent<Text>();
        reserveAmmo = ReserveAmmoObj.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        curAmmo = player.getCurrentAmmo("loaded");
        resAmmo = player.getCurrentAmmo("unloaded");
        currentAmmo.text = curAmmo.ToString();
        reserveAmmo.text = resAmmo.ToString();
        //Debug.LogError(resAmmo);
    }
}
