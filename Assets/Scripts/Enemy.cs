using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 50f;
    [SerializeField]
    private float currentHealth;
    private int damage = 2;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }


    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("player hit");
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player hit");
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
