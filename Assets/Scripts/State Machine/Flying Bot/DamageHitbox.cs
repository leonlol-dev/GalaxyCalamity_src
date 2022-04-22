using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHitbox : MonoBehaviour
{
    FlyingBotStateMachine stateMachine;

    public bool playerEntered;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = this.GetComponentInParent<FlyingBotStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onCollisionEnter(Collision other)
    {
        Debug.Log("Something entered");
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().currentHealth -= stateMachine.damage;
            playerEntered = true;
            

        }
    }

    private void onTriggerEnter(Collider other)
    {
        Debug.Log("Something entered");
        if (other.gameObject.tag == "Player")
        {
            //other.GetComponent<PlayerHealth>().currentHealth -= stateMachine.damage;
            playerEntered = true;

        }

    }
    private void onCollisionExit(Collision collision)
    {
        playerEntered = false;
    }
}
