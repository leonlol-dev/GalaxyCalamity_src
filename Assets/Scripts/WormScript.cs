using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormScript : MonoBehaviour
{
    public List<Transform> bodyParts = new List<Transform>();

    public float minDistance = 0.25f;


    public float speed = 1f;
    public float rotationSpeed = 50;

    private float distance;
    private Transform currentBodyPart;
    private Transform previousBodyPart;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        move();

    }

    public void move()
    {
        //Array 0 = Head

        float curSpeed = speed;

        if (Input.GetKey(KeyCode.W))
        {
            curSpeed *= 2;
        }

        bodyParts[0].Translate(bodyParts[0].forward * curSpeed * Time.smoothDeltaTime, Space.World);

        if (Input.GetAxis("Horizontal") != 0)
        {
            bodyParts[0].Translate(bodyParts[0].forward * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
        }

        //for (int i = 1; i < bodyParts.Count; i++)
        //{
        //    currentBodyPart = bodyParts[i];
        //    previousBodyPart = bodyParts[i - 1];

        //    distance = Vector3.Distance(previousBodyPart.position, currentBodyPart.position);

        //    Vector3 newPosition = previousBodyPart.position;

        //    newPosition.y = bodyParts[0].position.y;

        //    float T = Time.deltaTime * distance / minDistance * curSpeed;

        //    if (T > 0.5f)
        //    {
        //        T = 0.5f;
        //    }

        //    currentBodyPart.position = Vector3.Slerp(currentBodyPart.position, newPosition, T);
        //    currentBodyPart.rotation = Quaternion.Slerp(currentBodyPart.rotation, previousBodyPart.rotation, T);

        //}
    }

}
