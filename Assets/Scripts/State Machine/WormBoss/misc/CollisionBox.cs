using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBox : MonoBehaviour
{
    public GameObject worm;
    public ParticleSystem pSystem;

    private GameObject player;
    private bool playerInside;
    private float nextTimeToDamage = 0f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        worm = transform.parent.gameObject;
        pSystem = GetComponent<ParticleSystem>();

    }
    // Update is called once per frame
    void Update()
    {
        //When Player enters the particle system if they ever get too close.
        while (playerInside)
        {
            if (Time.time >= nextTimeToDamage)
            {
                nextTimeToDamage = Time.time + 1f / worm.GetComponent<WormStateMachine>().damageTick;
                player.GetComponent<PlayerHealth>().currentHealth -= worm.GetComponent<WormStateMachine>().damage;
                
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player)
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerInside = false;
    }

    //When the player touches the particles (lightning) they take damage.
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player hit by worm laser");
            player.GetComponent<PlayerHealth>().currentHealth -= worm.GetComponent<WormStateMachine>().damage;
        }
    }
}
