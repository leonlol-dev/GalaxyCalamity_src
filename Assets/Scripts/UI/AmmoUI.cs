using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public GameObject weaponManager;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //spaghetti i know lol
        if (weaponManager.GetComponent<WeaponManager>().selectedWeapon == 0 || weaponManager.GetComponent<WeaponManager>().selectedWeapon == 1)
        {
            text.text = weaponManager.GetComponent<WeaponManager>().currentWeapon.GetComponent<GunV2>().currentAmmo.ToString();
        }

        if (weaponManager.GetComponent<WeaponManager>().selectedWeapon == 2)
        {
            text.text = weaponManager.GetComponent<WeaponManager>().currentWeapon.GetComponent<LaserGun>().currentAmmo.ToString();
        }
        //switch(weaponManager.GetComponent<WeaponManager>().selectedWeapon)
        //{
        //    case 0:
        //        text.text = weaponManager.GetComponent<WeaponManager>().currentWeapon.GetComponent<GunV2>().currentAmmo.ToString();
        //        break;
        //    case 1:
        //        text.text = weaponManager.GetComponent<WeaponManager>().currentWeapon.GetComponent<GunV2>().currentAmmo.ToString()
        //}

    }
}
