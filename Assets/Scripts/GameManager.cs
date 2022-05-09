using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    public GameObject weaponCam;
    public GameObject weaponManager;
    public GameOver gameOver;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver()
    {
        gameOver.GameOverStartUp();
        weaponCam.gameObject.SetActive(false);
        weaponManager.gameObject.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerTeleport>().enabled = false;
        cam.GetComponent<MouseLook>().enabled = false;
        player.GetComponent<BoxCollider>().enabled = false;
        //player.transform.position = new Vector3(0f, 0f);
        Cursor.lockState = CursorLockMode.None;
    }
}
