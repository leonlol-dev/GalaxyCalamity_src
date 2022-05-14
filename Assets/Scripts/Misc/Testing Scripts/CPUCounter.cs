using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using UnityEngine;
using TMPro;
using System.IO;


public class CPUCounter : MonoBehaviour
{
    public TextMeshProUGUI text;

    [SerializeField]
    private float updateInterval; // How many intervals should CPU usage be updated?
    [SerializeField]
    private int processorCount; //The amount of physical cores.

    public float cpuUsage;

    private Thread cpuThread;
    private float lastCpuUsage;
    private int log;


    // Start is called before the first frame update
    void Start()
    {
        //Empties the .txt contents
        string path = Application.dataPath + "/cpuLog.txt";
        File.WriteAllText(path, string.Empty);

        Application.runInBackground = true;

        text.text = "{0:0}\nCPU\nUSAGE";

        //Set up the thread
        cpuThread = new Thread(UpdateCPUUsage)
        {
            IsBackground = true,
            Priority = System.Threading.ThreadPriority.BelowNormal

        };

        //Start the cpu usage thread
        cpuThread.Start();
    }

    private void OnValidate()
    {
        //This checks the amount of cpu cores your pc has, if you don't have virtual cores then you have to manually set this.
        processorCount = SystemInfo.processorCount / 2;
    }

    private void OnDestroy()
    {
        cpuThread?.Abort();
    }

    // Update is called once per frame
    void Update()
    {
        //Skip if nothing has changed.
        if (Mathf.Approximately(lastCpuUsage, cpuUsage)) return;

        //The first two values will be wrong, so we skip.
        if (cpuUsage < 0 || cpuUsage > 100) return;

        text.SetText("{0:0} %\nCPU\nUSAGE",
            cpuUsage);

        //text.text = cpuUsage.ToString("F1") + "% CPU";

        //Puts in into a file
        OnToFile(cpuUsage);

        //Update the value of last cpu usage
        lastCpuUsage = cpuUsage;

    }

    private void UpdateCPUUsage()
    {
        var lastCpuTime = new TimeSpan(0);

        // This is okay since this is executed in a background thread
        while (true)
        {
            var cpuTime = new TimeSpan(0);

            // Get a list of all running processes in this PC
            var AllProcesses = Process.GetProcesses();

            // Sum up the total processor time of all running processes
            cpuTime = AllProcesses.Aggregate(cpuTime, (current, process) => current + process.TotalProcessorTime);

            // get the difference between the total sum of processor times
            // and the last time we called this
            var newCPUTime = cpuTime - lastCpuTime;

            // update the value of _lastCpuTime
            lastCpuTime = cpuTime;

            // The value we look for is the difference, so the processor time all processes together used
            // since the last time we called this divided by the time we waited
            // Then since the performance was optionally spread equally over all physical CPUs
            // we also divide by the physical CPU count
            cpuUsage = 100f * (float)newCPUTime.TotalSeconds / updateInterval / processorCount;

            // Wait for UpdateInterval
            Thread.Sleep(Mathf.RoundToInt(updateInterval * 1000));
        }
    }

    private void OnToFile(float cpuUsage)
    {
        //Path
        string path = Application.dataPath + "/cpuLog.txt";

        //Create file
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "cpuLog \n\n");
        }
        //Contents
        string contents = (log + ". " + cpuUsage + "\n");
        log++;

        File.AppendAllText(path, contents);
    }
}
