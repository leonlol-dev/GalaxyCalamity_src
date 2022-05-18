using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLegGroundAimer : MonoBehaviour
{
    //This script shoots raycasts to check to see whether the spider needs to go up or down terrain.

    [SerializeField]
    private int layerMask;
    public GameObject raycastOrigin;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground");
        raycastOrigin = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; 
        if(Physics.Raycast(raycastOrigin.transform.position, -transform.up, out hit, Mathf.Infinity, layerMask))
        {
            transform.position = hit.point + new Vector3(0f, 0.3f, 0f);
        }
    }
}
