using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Gun variables
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    [SerializeField]
    int bulletsLeft, bulletsShot;

    //bools
    private bool shooting, readyToShoot, reloading;

    //References
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit ray;
    public LayerMask enemy;

    //Graphics
    public GameObject muzzleFlash, bulletHole;

    // Start is called before the first frame update
    void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        input();
    }

    private void input()
    {
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }

        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            //shoot
            Shoot();
        }
    }

    private void Shoot()
    {
        //Spread (for shotguns and stuff
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);
        
        //**************************************************************************//
        readyToShoot = false;
        bulletsLeft--;

        if (Physics.Raycast(fpsCam.transform.position, direction, out ray, range, enemy))
        {
            Debug.Log(ray.transform.name);

            if(ray.transform.CompareTag("Enemy"))
            {
                //damage enemy 
                ray.transform.GetComponent<Enemy>().takeDamage(damage);
            }
        }
        //Graphics
        Instantiate(bulletHole, ray.point, Quaternion.Euler(0, 100, 0));
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot < 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);

    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
   
}
