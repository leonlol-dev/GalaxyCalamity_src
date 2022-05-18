using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKManager : MonoBehaviour
{
    //This is the inverse kinematic solution using the gradient descent method. However this was only meant for 2D implementation, it simulates robotic movement for arms.
    public Joint root;
    public Joint end;

    public GameObject target;

    public float threshold = 0.05f;
    public float rate = 5.0f;

    public int steps = 20;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < steps; i++)
        {
            if (getDistance(end.transform.position, target.transform.position) > threshold)
            {
                Joint current = root;
                while (current != null)
                {
                    float slope = calculateSlope(current);
                    current.rotate(-slope * rate);
                    current = current.getChild();
                }

            }
        }
        
    }

    float getDistance(Vector3 _point1, Vector3 _point2)
    {
        return Vector3.Distance(_point1, _point2);
    }

    //Gradient descent method
    float calculateSlope(Joint _joint)
    {
        float deltaTheta = 0.01f;
        float firstDistance = getDistance(end.transform.position, target.transform.position);

        _joint.rotate(deltaTheta);

        float secondDistance = getDistance(end.transform.position, target.transform.position);

        _joint.rotate(-deltaTheta);

        //Calculate Slope
        return (secondDistance - firstDistance) / deltaTheta;
    }
}
