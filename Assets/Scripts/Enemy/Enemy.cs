using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple Enemy script that hold the enemy healthbar and hits the player if they get too close.

public class Enemy : MonoBehaviour
{
    public int maxHealth = 50;
    [SerializeField]
    public int currentHealth;
    private int damage = 2;
    public HealthBarScript healthbar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }


    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);

    }

    private void Die()
    {
        Destroy(gameObject);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log("player hit");
    //        collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        Debug.Log("player hit");
    //        other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
    //    }
    //}
}
