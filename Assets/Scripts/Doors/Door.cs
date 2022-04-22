using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    private Vector3 defaultPos;
    public float openSpeed = 50f;
    public float gap = 250f;
    public bool openDoor;
    public bool closeDoor;
    public Vector3 openedDoor;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.position;
        openedDoor = new Vector3(defaultPos.x, defaultPos.y + gap, defaultPos.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (openDoor)
        {
            Open();
        }


    
    }

    void Open()
    {
        
        transform.position = Vector3.Slerp(transform.position, openedDoor, Time.deltaTime);
    }
}
