using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMachine : MonoBehaviour
{
    [Header("Objects To Set")]
    [Space(3)]
    public GunV2 gun;
    public GunV2 rifle;
    public LaserGun laser;
    public WeaponManager weaponManager;
    public AudioSource aSource;
    [Space(10)]

    [Header("Allow Mega Upgrade on this?")]
    [Space(3)]
    public bool megaUpgrade;  //Mega upgrade is upgrades to all 3 weapons.
    [Space(10)]

    [Header("Colours")]
    [Space(3)]
    [ColorUsage(true, true)]
    public Color mainOrb;
    [ColorUsage(true, true)]
    public Color fresnelOrb;
    [Space(10)]

    [Header("Renderers")]
    [Space(3)]
    public Renderer[] Rend;
    [Space(10)]



    //[Header("Materials to Set for Shader Properties Changes")]
    //[Space(3)]
    private Material mat;
    //[Space(10)]

    private bool opened;
   
    // Start is called before the first frame update
    void Start()
    {
        // Set Material and Shader 
        mat = new Material(Shader.Find("Shader Graphs/Hologram 2"));

        for (int i = 0; i < Rend.Length; i++)
        {
            Rend[i].material = mat;
            
            Rend[i].material.SetColor("Main_Colour", mainOrb);
            Rend[i].material.SetColor("Fresnel_Colour", fresnelOrb);
            Rend[i].material.SetFloat("Scroll_Speed", 1f);
        }

        //Grab audio source
        aSource = this.gameObject.GetComponent<AudioSource>();

        //Get gun scripts
        gun = weaponManager.transform.GetChild(0).GetComponent<GunV2>();
        rifle = weaponManager.transform.GetChild(1).GetComponent<GunV2>();
        laser = weaponManager.transform.GetChild(2).GetComponent<LaserGun>();
    }


     public void ChooseUpgrade()
    {
        if (!opened)
        {
            Debug.Log("upgrade!");

            //Set Bool to true;
            opened = true;

            //Play Sound
            aSource.Play();

            //Set Material Colour to red.
            for (int i = 0; i < Rend.Length; i++)
            {
                Rend[i].material.SetColor("Main_Colour", Color.red);
                Rend[i].material.SetColor("Fresnel_Colour", Color.red);

            }

            if (megaUpgrade)
            {
                PistolUpgrade();
                RifleUpgrade();
                LaserUpgrade();
            }

            else
            {
                //Randomly select a number for upgrade
                int select = Random.Range(0, 2);
                switch(select)
                {
                    default:
                        Debug.Log("Upgrade failed, weapon choice went out of bounds!");
                        break;

                    case 0:
                        PistolUpgrade();
                        break;
                    case 1:
                        RifleUpgrade();
                        break;
                    case 2:
                        LaserUpgrade();
                        break;
                }
            }
  
        }

    }

    private void PistolUpgrade()
    {
        //Select upgrade int and value int
        int select = Random.Range(0, 4);
        

        switch(select)
        {
            default:
                Debug.Log("Pistol Upgrade fail, went out of bounds");
                break;
            case 0:
                int damageValue = Random.Range(0, 12);
                gun.damage += damageValue;
                string d = gun.damage.ToString();
                Debug.Log("Pistol Damage upgraded! Damage: " + d);
                break;

            case 1:
                int fireRateValue = Random.Range(0, 100);
                gun.fireRate += fireRateValue;
                string f = gun.fireRate.ToString();
                Debug.Log("Pistol Fire Rate upgraded! Fire Rate: " + f);
                break;

            case 2:
                int ammoValue = Random.Range(0, 10);
                gun.maxAmmo += ammoValue;
                string a = gun.maxAmmo.ToString();
                Debug.Log("Pistol Ammo increased! Ammo: " + a);
                break;

            case 3:
                float reloadValue = Random.Range(0, 1);
                gun.reloadTime -= reloadValue;
                string r = gun.reloadTime.ToString();
                Debug.Log("Pistol Reload Time decreased! Reload Time: " + r);
                break;

            case 4:
                gun.automatic = true;
                Debug.Log("Pistol is now automatic!");
                break;

        }
    }

    private void RifleUpgrade()
    {
        int select = Random.Range(0, 2);
        switch (select)
        {
            default:
                Debug.Log("Rifle Upgrade fail, went out of bounds");
                break;

            case 0:
                int damageValue = Random.Range(0, 12);
                rifle.damage += damageValue;
                string d = rifle.damage.ToString();
                Debug.Log("Rifle Damage upgraded! Damage: " + d);
                break;

            case 1:
                int ammoValue = Random.Range(0, 10);
                rifle.maxAmmo += ammoValue;
                string a = rifle.maxAmmo.ToString();
                Debug.Log("Rifle Ammo increased! Ammo: " + a);
                break;

            case 2:
                float reloadValue = Random.Range(0, 1);
                rifle.reloadTime -= reloadValue;
                string r = rifle.reloadTime.ToString();
                Debug.Log("Rifle Reload Time decreased! Reload Time: " + r);
                break;

        }
    }

    private void LaserUpgrade()
    {
        int select = Random.Range(0, 3);
        switch(select)
        {
            default:
                Debug.Log("Laser Gun upgrade fail, went out of bounds");
                break;

            case 0:
                int damageValue = Random.Range(0, 20);
                laser.damage += damageValue;
                string d = laser.damage.ToString();
                Debug.Log("Laser Gun Damage upgraded! Damage: " + d);
                break;

            case 1:
                laser.automatic = true;
                Debug.Log("Laser Gun is now automatic!");
                break;

            case 2:
                int ammoValue = Random.Range(0, 10);
                laser.maxAmmo += ammoValue;
                string a = laser.maxAmmo.ToString();
                Debug.Log("Laser Gun Ammo increased! Ammo: " + a);
                break;

            case 3:
                float reloadValue = Random.Range(0, 1);
                laser.reloadTime -= reloadValue;
                string r = laser.reloadTime.ToString();
                Debug.Log("Laser Gun Reload Time decreased! Reload Time: " + r);
                break;


        }
    }
}
