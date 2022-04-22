using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorConsole : MonoBehaviour
{
    public Door cDoor;
    public DoorClose oDoor;
    public bool action;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TimeForAction()
    {
        action = true; 
        cDoor.openDoor = true;
        oDoor.closeDoor = true;
    }
}
