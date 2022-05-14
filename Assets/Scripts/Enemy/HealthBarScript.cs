using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script that controls the UI health bars on the enemies.
public class HealthBarScript : MonoBehaviour
{
    public Slider slider;
    public Text text; 

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        text.text = "HP: " + health;
    }

    public void SetHealth(int health)
    {
        slider.value = health ;
        text.text = "HP: " + health;
    }

}
