using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    // This is the game manager, this mostly handles when the player dies and when the game ends.
    public GameObject player;
    public GameObject cam;
    public GameObject weaponCam;
    public GameObject weaponManager;
    public GameOver gameOver;
    public TextMeshProUGUI gameOverText;


    public bool wormBossKilled;
    public bool spiderBossKilled;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wormBossKilled && spiderBossKilled)
        {
            GameWin();
        }    
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

    public void GameWin()
    {
        gameOverText.text = "You Win!";
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
