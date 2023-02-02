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

    gun Gun;
    // Start is called before the first frame update
    void Start()
    {
        Gun = GameObject.FindGameObjectWithTag("gun").GetComponent<gun>();

        currentAmmo = CurrentAmmoObj.GetComponent<Text>();
        reserveAmmo = ReserveAmmoObj.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        curAmmo = Gun.getCurrAmmo();
        resAmmo = Gun.getStoredAmmo();
        currentAmmo.text = curAmmo.ToString();
        reserveAmmo.text = resAmmo.ToString();
    }
}
