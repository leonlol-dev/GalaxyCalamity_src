using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    //This script controls the ui of the player interact. When the player hovers over an interactable object, text will appear.
    public GameObject player;
    public Camera cam;

    public GameObject text;

    public float range;
    public float textRange = 10;



    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        text.SetActive(false);

        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        RaycastHit interact;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out interact, textRange))
        {
            if (interact.collider.gameObject.tag == "Interactable" && !interact.collider.gameObject.GetComponent<UpgradeMachine>().opened || interact.collider.gameObject.tag == "Door"/* && !interact.collider.gameObject.GetComponent<DoorConsole>().action*/)
            {
                text.SetActive(true);
            }

            else
            {
                text.SetActive(false);
            }
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

            DoorConsole doorConsole = hit.transform.GetComponent<DoorConsole>();
            if(doorConsole != null)
            {
                doorConsole.TimeForAction();
            }

            Room4Console bossConsole = hit.transform.GetComponent<Room4Console>();
            if(bossConsole != null)
            {
                bossConsole.BossCutscene();
            }


        }
        

    }



}
