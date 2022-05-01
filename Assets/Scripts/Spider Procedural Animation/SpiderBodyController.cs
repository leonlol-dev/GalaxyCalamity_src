using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBodyController : MonoBehaviour
{
    
    public GameObject[] legTargets; //The tips of the legs
    public Vector3 lastBodyUp;
    public float smoothness = 8f; //Smoothness of the movement of the body 

    // Start is called before the first frame update
    void Start()
    {
        //Setting last body up, which is the up side of the body.
        lastBodyUp = transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v1 = legTargets[0].transform.position - legTargets[1].transform.position; //Distance from leg 1 and 2
        Vector3 v2 = legTargets[2].transform.position - legTargets[3].transform.position; //Distance from leg 3 and 4
        Vector3 normal = Vector3.Cross(v1, v2).normalized; //Middle of the two distances 
        Vector3 up = Vector3.Lerp(lastBodyUp, normal, 1f / (float)(smoothness + 1)); //Up
        transform.up = up;
        transform.rotation = Quaternion.LookRotation(transform.parent.forward, up);
        lastBodyUp = transform.up; //Update last body up
    }
}
