using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportHUD : MonoBehaviour
{
    public Image image;
    public PlayerTeleport teleportScript;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        float cooldown = teleportScript.cooldown;

        if(teleportScript.cantTeleport)
        {

            image.fillAmount += 1 / cooldown * Time.deltaTime;

            if (image.fillAmount >= 1)
            {
                image.fillAmount = 0;
            }
        }

        if (!teleportScript.cantTeleport && image.fillAmount >= 0)
        {
            image.fillAmount = 1;
        }


    }
}
