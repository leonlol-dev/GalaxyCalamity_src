using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject player;
    public Camera cam;

    public float range;

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void Interact()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            UpgradeMachine machine = hit.transform.GetComponent<UpgradeMachine>();
            if (machine != null)
            {
                machine.ChooseUpgrade();
            }
        }
        

    }
}
