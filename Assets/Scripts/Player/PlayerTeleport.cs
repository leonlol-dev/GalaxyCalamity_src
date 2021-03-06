using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    //This is the teleport script.

    private RaycastHit lastRaycastHit;
    public bool cantTeleport;

    [Header("Variables")]
    public float range = 1000;
    public float cooldown;

    [Space(10)]

    [Header("Things to set")]
    public CharacterController player;
    public GameObject teleportIndicator;
    public Camera cam;
    public AudioSource teleportSFX;
    public GameObject particles;


    private void Start()
    {
        teleportIndicator.SetActive(false);
        particles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Raycast for teleport indicator
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit camRay, range))
        {
            if (camRay.distance <= range)
            {
                teleportIndicator.transform.position = camRay.point + camRay.normal;
            }

        }

        else
        {
            //This sets the indicator to a place to where the player can't see, disabling the game object will break the code.
            teleportIndicator.transform.position = transform.TransformPoint(2, 0, 0);
        }

        if (Input.GetButtonDown("Fire2") && !cantTeleport)
        {
            teleportIndicator.SetActive(true);
        }

        if (Input.GetButtonUp("Fire2") && !cantTeleport && teleportIndicator.active)
        {
            //Turn off teleport indicator
            teleportIndicator.SetActive(false);

            if (getObject() != null)
            {
                //Start cooldown
                StartCoroutine(teleportCooldown(cooldown));

                //Particles
                StartCoroutine(teleportParticles());

                //Teleport
                teleportToLookAt();

                //Teleport bool to false
                cantTeleport = true;
                
            }
        }    



        
    }

    //This checks if the player is looking at a object within range, this returns a game object. 
    private GameObject getObject()
    {
        Vector3 origin = transform.position;
        Vector3 direction = Camera.main.transform.forward;

        if(Physics.Raycast(origin, direction, out lastRaycastHit, range))
        {
            return lastRaycastHit.collider.gameObject;
        }

        else
        {
            return null;
        }
        
    }

    //This teleports the player to whatever they are looking at.
    private void teleportToLookAt()
    {
        player.enabled = false;
        transform.position = lastRaycastHit.point + lastRaycastHit.normal * 1.5f;
        player.enabled = true;
        //play audio
        teleportSFX.Play();
    }

    //Teleport cooldown 
    IEnumerator teleportCooldown(float coolDownDuration)
    {
        yield return new WaitForSeconds(coolDownDuration);
        Debug.Log("teleport can now be used again.");
        cantTeleport = false;

    }

    //To show particle effects after a certain amount of time.
    IEnumerator teleportParticles()
    {
        //set active
        particles.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        //set active off
        particles.SetActive(false);
    }

}
