using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    //This is the dash script, it gets the direction of the camera's forward and will teleport the player.
    public PlayerMovement movementScript;
    public Camera cam;
    public float dashSpeed;
    public float dashTime;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }


    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            //movementScript.controller.Move(movementScript.move * dashSpeed * Time.deltaTime);
            Vector3 dir = Camera.main.transform.forward;
            movementScript.controller.Move(dir * Time.deltaTime * Time.deltaTime);
            yield return null;
        }
    }
}
