using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererScript : MonoBehaviour
{
    public Transform[] points;
    public LineRenderer line;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        SetUpLine(points);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            line.SetPosition(i, points[i].position);
        }
    }
    
    public void SetUpLine(Transform[] points)
    {
        line.positionCount = points.Length;
        this.points = points;
    }
}
