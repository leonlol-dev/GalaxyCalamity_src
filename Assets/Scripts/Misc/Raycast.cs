using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    public int range;
    public LayerMask layerMaskInteract;
    public Camera fpsCam;
    public Image crosshair;


    private GameObject raycastObject;
    

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = fpsCam.transform.forward + new Vector3(0, 0, 0);

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, direction, out hit, range, layerMaskInteract.value))
        {
            if (hit.collider.CompareTag("Interact"))
            {
                raycastObject = hit.collider.gameObject;
            }

            if(Input.GetKeyDown("e"))
            {
                Debug.Log("interact object");
                raycastObject.SetActive(false);
            }
        }

        else
        {
            //crosshair normal
        }

    }

    void CrosshairActive()
    {

    }


}
