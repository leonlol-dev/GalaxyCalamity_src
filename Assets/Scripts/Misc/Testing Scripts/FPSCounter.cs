using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class FPSCounter : MonoBehaviour
{
    public TextMeshProUGUI text;
    [Range(0.1f, 2f)]
    public float sampleDuration;
    int frames;
    public int log = 0;
    float duration, bestDuration = float.MaxValue, worstDuration;

    // Start is called before the first frame update
    void Start()
    {
        //Empties the .txt contents
        string path = Application.dataPath + "/log.txt";
        File.WriteAllText(path, string.Empty);
    }

    // Update is called once per frame
    void Update()
    {
        float frameDuration = Time.unscaledDeltaTime;

        frames += 1;
        duration += frameDuration;
        
        if(frameDuration < bestDuration)
        {
            bestDuration = frameDuration;
        }
        if (frameDuration > worstDuration)
        {
            worstDuration = frameDuration;
        }

        if (duration >= sampleDuration)
        {
            text.SetText("FPS\n{0:0}\n{1:0}\n{2:0}", 
                1f / bestDuration,
                frames / duration,
                1f / worstDuration);

            OnToFile(frames / duration, 1f/ bestDuration, 1f/ worstDuration);

            frames = 0;
            duration = 0f;
            bestDuration = float.MaxValue;
            worstDuration = 0f;
        }

        

    }

    void OnToFile(float avg, float best, float worst)
    {
        //Path
        string path = Application.dataPath + "/log.txt";

        //Create file
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Log \n\n");
        }

        //Contents
        string contents = (log + ". AVG: " +  avg + " BEST: " + best + " WORST: " + worst + "\n");
        log++;

        //Add content to file
        File.AppendAllText(path, contents);
    }
}
