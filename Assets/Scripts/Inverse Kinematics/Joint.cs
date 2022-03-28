using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
