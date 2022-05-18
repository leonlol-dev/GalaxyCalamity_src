using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderConstraint : MonoBehaviour
{
    //This controls the legs of the spider, these are the constraints of the legs.

    [SerializeField]
    private Vector3 originalPos;


    public GameObject moveCube;
    public float legMoveSpeed = 7f;
    public float targetDistance = 1f;
    public float moveDistance = 0.7f;
    public float moveStoppingDistance = 0.4f;
    public SpiderConstraint oppositeLeg;
    public float distanceToMoveCube;
    public bool isMoving = false;
    public bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    //Using four different cubes for each of the legs to display a support for centre of mass, all four cubes move with the body and each leg needs to be within a certain distance within the cube
    //If not they will move towards to cube using linear interpolation.
    // Update is called once per frame
    void FixedUpdate()
    {
        distanceToMoveCube = Vector3.Distance(transform.position, moveCube.transform.position);
        if ((distanceToMoveCube >= targetDistance && !oppositeLeg.isItMoving()) || moving)
        {
            moving = true;

            //Move the constraint
            transform.position = Vector3.Lerp(transform.position, moveCube.transform.position + new Vector3(0f, 0.3f, 0f), Time.deltaTime * legMoveSpeed);

            //Move original 
            originalPos = transform.position;

            isMoving = true;

            if(distanceToMoveCube < moveStoppingDistance)
            {
                moving = false;
            }
        }
        //If the leg is not moving, they will stay on the ground.
        else
        {
            transform.position = Vector3.Lerp(transform.position, originalPos + new Vector3(0f, -0.3f, 0f), Time.deltaTime * legMoveSpeed * 3);
            isMoving = false;
        }
    }

    public bool isItMoving()
    {
        return isMoving;
    }
}
