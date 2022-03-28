using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int selectedWeapon;
    [SerializeField]
    private float switchDelay = 1f;
    private bool isSwitching = false;
    public GameObject currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetKeyDown(KeyCode.Alpha1) && !isSwitching)
        {
            selectedWeapon = 0;
        
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2 && !isSwitching)
        {
            selectedWeapon = 1;

        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3 && !isSwitching)
        {
            selectedWeapon = 2;

        }

        if (previousSelectedWeapon != selectedWeapon)
        {
            //Invoke("SelectWeapon", switchDelay);
            SelectWeapon();
            //play out animation?
            
        }
    }

    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if(i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                currentWeapon = weapon.gameObject;
            }

            else
            {
                weapon.GetComponent<Animator>().CrossFade("New State", 0f);
                weapon.GetComponent<Animator>().Update(0f);
                weapon.GetComponent<Animator>().Update(0f);
                weapon.GetComponent<Animator>().Update(0f);
                weapon.gameObject.SetActive(false);
            }

            i++;
        }
    }

    private IEnumerator SwitchAfterDelay()
    {
        isSwitching = true;

        currentWeapon.GetComponent<Animator>().SetBool("Reloading", false);
        yield return new WaitForSeconds(switchDelay);
        isSwitching = false;

    }


}
