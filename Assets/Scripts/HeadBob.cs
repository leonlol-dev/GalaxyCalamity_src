using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public bool enable;

    public float amplitude = 0.015f;
    public float frequency = 10.0f;

    public Transform mainCamera;
    public Transform cameraHolder;

    public float toggleSpeed = 3.0f;
    private Vector3 startPos;
    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        startPos = mainCamera.localPosition;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (!enable) return;

        CheckMotion();
        
        mainCamera.LookAt(FocusTarget());
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitude;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitude * 2;
        return pos;
    }

    private void CheckMotion()
    {
        float speed = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).magnitude;
      
        ResetPosition();

        if (speed < toggleSpeed) return;
        if (!controller.isGrounded) return;

        PlayMotion(FootStepMotion());
    }

    private void ResetPosition()
    {
        if (mainCamera.localPosition == startPos) return;
        mainCamera.localPosition = Vector3.Lerp(mainCamera.localPosition, startPos, 1 * Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraHolder.localPosition.y, transform.position.z);
        pos += cameraHolder.forward * 15f;
        return pos;
    }

    private void PlayMotion(Vector3 motion)
    {
        mainCamera.localPosition += motion;
    }
}
