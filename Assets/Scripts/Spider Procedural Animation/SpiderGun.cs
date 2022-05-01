using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderGun : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;

    public GameObject bullet;
    public GameObject bulletOrigin;
    public AudioSource shootSound;
    public Transform gun;
    public LineRenderer line;
    public SpiderStateMachine stateMachine;

    public float rotationSpeed = 1f;
    public float attackSpeed = 2f;
    public float defaultAttackSpeed = 2f;
    public float angryAttackSpeed = 6f;
    public float range = 55f;
    public int damage;
    public bool activateTurret;

    private bool alreadyAttacked;
    private float nextTimeToFire = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(bulletOrigin.transform.position, bulletOrigin.transform.forward * range, Color.yellow);
        if (Physics.Raycast(bulletOrigin.transform.position, bulletOrigin.transform.forward, out hit, range))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("player in sight.");
                line.SetWidth(0.5f, 0.5f);

                if(Time.time >= nextTimeToFire && activateTurret)
                {
                    nextTimeToFire = Time.time + 1f / attackSpeed;
                    Attack();
                }
            }
            else
            {
                line.SetWidth(0, 0);
            }
        }

        if(stateMachine.angry)
        {
            attackSpeed = angryAttackSpeed;
        }

        else
        {
            attackSpeed = defaultAttackSpeed;
        }
    
    }

    private void FixedUpdate()
    {
        Quaternion rotation = Quaternion.LookRotation(bulletOrigin.transform.position - player.transform.position);
        gun.rotation = Quaternion.RotateTowards(gun.transform.rotation, rotation, Time.deltaTime * rotationSpeed);

       
        
    }

    void Attack()
    {
        shootSound.Play();

        Rigidbody rb = GameObject.Instantiate(bullet, bulletOrigin.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(gun.transform.forward * -34f, ForceMode.Impulse);
        rb.GetComponent<TurretBullet>().damage = damage;

        GameObject.Destroy(rb, 2f);
    }

    private void OnDrawGizmos()
    {

    }
}
