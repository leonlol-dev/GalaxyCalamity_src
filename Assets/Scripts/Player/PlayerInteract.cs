using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    public GameObject player;
    public Camera cam;

    public GameObject text;

    public float range;



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
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out interact, range))
        {
            if (interact.collider.gameObject.tag == "Interactable" && !interact.collider.gameObject.GetComponent<UpgradeMachine>().opened)
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
        }
        

    }

}
