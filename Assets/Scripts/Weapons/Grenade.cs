using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 2f;

    private float countdown;
    bool hasExploded = false;

    public GameObject explosionEffect;
    public AudioSource audiosource;
    public AudioClip[] explosionSound;
    public int explosionSoundChoice;

    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
        audiosource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if(countdown <= 0f && !hasExploded)
        {
            hasExploded = true;
            explode();
        }
    }

    void explode()
    {
        //Instantiate the particle effect.
        GameObject particles = (GameObject)Instantiate(explosionEffect, transform.position, transform.rotation);

        //Chooses a random explosion sound and plays it.
        randomSoundSelector();

        //Destroy Grenade
        Destroy(gameObject, 1f);

        //Destroy particle system
        Destroy(particles, 2.25f);
    }

    void randomSoundSelector()
    {
        //Chooses a random explosion sound
        explosionSoundChoice = Random.Range(0, explosionSound.Length);
        audiosource.clip = explosionSound[explosionSoundChoice];
        audiosource.Play();
    }
}
