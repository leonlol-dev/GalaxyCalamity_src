using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpiderBodyUp : MonoBehaviour
{
    //THIS IS NOT USED WITHIN THE GAME.
    [SerializeField]
    private int layerMask;
    public GameObject raycastOrigin;
    public GameObject spider;
    public GameObject spiderBody;
    public GameObject spiderModel;

    public Vector3 groundParallel;
    public Vector3 slopeParallel;


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
        if (Physics.Raycast(raycastOrigin.transform.position, -transform.up, out hit, Mathf.Infinity, layerMask))
        {
            transform.position = hit.point + new Vector3(0f, 0.33f, 0f);
            spider.transform.position = transform.position;

            //A lil bit of code to make the spider go up the hill with it's body, tho its a little bit buggy.
            //spiderModel.transform.rotation = Quaternion.Lerp(spiderModel.transform.rotation, (Quaternion.LookRotation(hit.normal)), Time.deltaTime * 5.0f);


        }

    }

}
