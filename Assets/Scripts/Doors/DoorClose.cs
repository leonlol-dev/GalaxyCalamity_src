using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorClose : MonoBehaviour
{
    private Vector3 defaultPos;
    public float openSpeed = 0.01f;
    public float gap = 10f;
    public bool closeDoor;
    public Vector3 closedDoor;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.position;
        closedDoor = new Vector3(defaultPos.x, defaultPos.y - gap, defaultPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (closeDoor)
        {
            Close();
        }



    }

    void Close()
    {

        transform.position = Vector3.Slerp(transform.position, closedDoor, Time.deltaTime);
    }
}
