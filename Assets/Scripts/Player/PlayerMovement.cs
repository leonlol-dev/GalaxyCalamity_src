using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //This script controls the player's movements using the character controller component. The player movement also uses the freefall equation.

    public float speed = 12f;
    public float maxSpeed = 20f;
    public float upgradeSpeed = 25f;
    public float defaultSpeed = 12f;
    public float gravity = -18.81f;
    public float jumpHeight = 3f;
    public float x;
    public float z;
    public Vector3 velocity;
    public Vector3 move;
    public CharacterController controller;
    public bool windUpgrade;


    // Update is called once per frame
    void Update()
    {
        
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKey(KeyCode.Space) && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            speed++;

            if(speed >= maxSpeed)
            {
                speed = maxSpeed;
            }
        }

        else if(Input.GetKeyUp(KeyCode.Space))
        {
            speed = defaultSpeed;

        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
    
}
