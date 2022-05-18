using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{   
    //A very simple player health script.

    public int maxHealth = 100;
    public int currentHealth;
    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 0)
        {
            Debug.Log("Player Died");
            manager.GameOver();
           
        }    
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
    }
}
