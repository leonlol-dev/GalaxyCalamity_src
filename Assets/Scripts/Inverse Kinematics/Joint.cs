using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class joint for the inverse kinematics manager.
public class Joint : MonoBehaviour
{
    public Joint child;

    public Joint getChild()
    {
        return child;
    }

    public void rotate(float _angle)
    {
        transform.Rotate(Vector3.up * _angle);
    }
}
