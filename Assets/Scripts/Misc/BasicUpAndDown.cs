using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUpAndDown : MonoBehaviour
{
    private Vector3 pos;
    public float vertSpeed = 5f;
    public float height = 0.5f;
    private void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newY = Mathf.Sin(Time.time * vertSpeed) * height + pos.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
