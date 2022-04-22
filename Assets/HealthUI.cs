using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI text;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        text.text = player.GetComponent<PlayerHealth>().currentHealth.ToString();
    }
}
