using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] bgm;
    public AudioClip bossMusic;
    public AudioSource aSource;
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        aSource = GetComponent<AudioSource>();
        StartAudio();

    }

    // Update is called once per frame
    void Update()
    {
       if(!aSource.isPlaying)
        {
            StartAudio();
        }
        
    }

    void StartAudio()
    {
        aSource.clip = bgm[i];
        aSource.Play();

        i++;
    }

    public void BossTrigger()
    {
        aSource.Stop();
        aSource.clip = bossMusic;
        aSource.Play();
    }
}
