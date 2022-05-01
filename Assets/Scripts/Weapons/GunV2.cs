using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunV2 : MonoBehaviour
{
    [Header("Weapon Sway Settings")]
    public float swayMultiplier;
    public float smoothness;

    [Header("Gun Settings")]
    public int damage = 10;
    public float range = 100f;
    public float spread = 0.2f;
    public float fireRate = 15f;
    public int maxAmmo = 10;
    public float reloadTime = 1f;
    public bool automatic = false;
    public float force = 30f;

    [Header("Sound Settings")]
    public AudioClip[] sounds;
    public AudioSource audiosource;
    public AudioClip reloadSound;

    [Header("Objects to set")]
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Animator animator;

    //Private
    [SerializeField]
    private float nextTimeToFire = 0f;
    private bool isReloading = false;

    [HideInInspector]
    public int currentAmmo;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = this.GetComponent<AudioSource>();
        currentAmmo = maxAmmo;
       
        
    }

    private void OnEnable()
    {

        isReloading = true;

        animator.enabled = true;

        animator.SetBool("Reloading", false);
 
        animator.Play("Out", 0, 6f);

        Invoke("turnOffAnimator", 1f);

        isReloading = false;


    }

    

    private void OnDisable()
    {
        //transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSway();
        Debug.DrawRay(transform.position, fpsCam.transform.forward, Color.green);


        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (currentAmmo < maxAmmo && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }    

        // if statement for automatic
        if (automatic)
        {
            if (Input.GetButton("Fire1") & Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") & Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }

      
    }
    IEnumerator Reload()
    {
        isReloading = true;

        animator.enabled = true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;

        animator.SetBool("Reloading", false);
        Invoke("turnOffAnimator", 0.66f);
        isReloading = false;
    }
    void Shoot()
    {
        currentAmmo--;

        muzzleFlash.Play();
        GunSound();


        //Spread (for shotguns and stuff
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //Debug.DrawRay(fpsCam.transform.position, direction, Color.green);

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range))
        {
            //Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.takeDamage(damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * force);
            }
            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);
        }
    }

    void WeaponSway()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

        //Calculate target rotation
        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion targetRotation = rotationX * rotationY;

        //Rotate weapon
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smoothness * Time.deltaTime);
    }

    void GunSound()
    {
        //Chooses a random sound
        int soundChoice = Random.Range(0, sounds.Length);
        audiosource.clip = sounds[soundChoice];
        audiosource.Play();
    }

    public void ReloadSound()
    {
        audiosource.clip = reloadSound;
        audiosource.Play();
    }





    void turnOffAnimator()
    {
        animator.enabled = false;
    }

    private void OnDrawGizmos()
    {
        
    }
}
