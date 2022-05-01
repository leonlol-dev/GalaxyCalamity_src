using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAmmoText : MonoBehaviour
{
    public TextMesh maxAmmoText;
    public TextMesh currentAmmoText;
    
    

    // Start is called before the first frame update
    void Start()
    {
        string newString = GetComponent<GunV2>().maxAmmo.ToString();
        maxAmmoText.text = newString;
    }

    // Update is called once per frame
    void Update()
    {
        string ammotext = GetComponent<GunV2>().currentAmmo.ToString();
        currentAmmoText.text = ammotext;
    }
}
