using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    // This script allows the health bar canvas to rotate according to the camera so the player can constantly see the health bar. - Leon
    
    public Transform cam;
    void Start()
    {
       
        cam = GameObject.Find("Main Camera").transform;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);

    }
}
