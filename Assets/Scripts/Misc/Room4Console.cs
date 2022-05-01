using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room4Console : MonoBehaviour
{
    public GameObject player;
    public GameObject cutsceneCam;
    public Transform teleportLoc;

    public float time = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BossCutscene()
    {
        Debug.Log("PRessed");
        cutsceneCam.SetActive(true);
        player.SetActive(false);
        StartCoroutine(CutsceneAndWait(time));
        player.transform.position = teleportLoc.position;
    }    

    private IEnumerator CutsceneAndWait(float time)
    {
       
        yield return new WaitForSeconds(time);
        cutsceneCam.SetActive(false); ;
        player.SetActive(true);
    }
}
