using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public GameObject player;
    public Transform teleport;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("entered");
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = teleport.position;
            player.GetComponent<CharacterController>().enabled = true;

        }
    }
}
