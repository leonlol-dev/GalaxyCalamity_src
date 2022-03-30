using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    public float projectileSpeed = 5f;
    public Vector3 target;
    public GameObject collisionExplosion;
    public AudioSource explosionSound;
    public AudioClip explosionClip;

    public float radius = 2f;
    public float force = 555f;
    private void Start()
    {
        explosionSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += transform.forward * Time.deltaTime * projectileSpeed; 
        float step = projectileSpeed * Time.deltaTime;

        if(target != null)
        {
            if (transform.position == target)
            {

                explode();
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
    }

    public void setTarget(Vector3 _target)
    {
        target = _target;
    }

    void explode()
    {
        if(collisionExplosion != null)
        {
            GameObject explosion = (GameObject)Instantiate(collisionExplosion, transform.position, transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider objectsNearby in colliders)
            {
                Rigidbody rb = objectsNearby.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }

                Enemy enemy = objectsNearby.GetComponent<Enemy>();
                if(enemy != null)
                {
                    enemy.takeDamage(33f);
                }
            }
            Destroy(gameObject);
            Destroy(explosion, 1f);
            
        }
    }


}
