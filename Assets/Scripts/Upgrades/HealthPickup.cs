using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float vertSpeed = 5f;
    public float height = 0.5f;
    public int healthGain = 25;
    public AudioSource sound;
    public GameObject model;
    private Vector3 pos;
    private bool entered;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        pos = transform.position;
    }

    private void Update()
    {
        transform.Rotate(transform.rotation.x, rotationSpeed * Time.deltaTime, transform.rotation.z);

        float newY = Mathf.Sin(Time.time * vertSpeed) * height + pos.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !entered)
        {
            entered = true;
            other.GetComponent<PlayerHealth>().currentHealth += healthGain;
            sound.Play();
            model.SetActive(false);
            Destroy(this.gameObject, 0.55f);
        }
    }
}
