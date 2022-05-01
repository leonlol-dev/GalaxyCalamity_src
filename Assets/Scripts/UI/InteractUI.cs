using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class InteractUI : MonoBehaviour
{
    public GameObject player;
    public GameObject text;


    // Start is called before the first frame update
    void Start()
    { 
        text.SetActive(false);
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
