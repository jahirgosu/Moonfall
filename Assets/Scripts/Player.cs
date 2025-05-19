using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2f;
    public float maxAcceleration = 3f;
    public float accelerationRate = 0.025f;
    public float decelerationRate = 0.05f;

    private float acceleration = 1f;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            acceleration += accelerationRate;
            if (acceleration >= maxAcceleration)
            {
                acceleration = maxAcceleration;
            }
        }
        else {
            acceleration -= decelerationRate;
            if (acceleration < 1f)
            {
                acceleration = 1f;
            }
        }

        float finalSpeed = speed * acceleration;

        Vector3 currentPosition = transform.position;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(
                currentPosition.x + (finalSpeed * Time.deltaTime),
                currentPosition.y,
                currentPosition.z
            );  
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(
                currentPosition.x - (finalSpeed * Time.deltaTime),
                currentPosition.y,
                currentPosition.z
            );
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(
                currentPosition.x,
                currentPosition.y + (finalSpeed * Time.deltaTime),
                currentPosition.z
            );
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(
                currentPosition.x,
                currentPosition.y - (finalSpeed * Time.deltaTime),
                currentPosition.z
            );
        }
    }
}