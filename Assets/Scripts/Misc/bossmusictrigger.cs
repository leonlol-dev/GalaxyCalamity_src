using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossmusictrigger : MonoBehaviour
{
    public MusicManager musicManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            musicManager.BossTrigger();
            Destroy(this, 0.5f);
        }
    }
}
