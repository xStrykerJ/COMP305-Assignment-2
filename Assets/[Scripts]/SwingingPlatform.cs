using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingPlatform : MonoBehaviour
{
    Rigidbody2D rb;

    public float movementSpeed;
    public float leftAngle;
    public float rightAngle;

    bool movingClockwise;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void ChangeMoveDirection() 
    {
        if (transform.rotation.z > rightAngle)
        {
            movingClockwise = false;
        }
        if (transform.rotation.z < leftAngle)
        {
            movingClockwise = true;
        }
    }

    public void Move()
    {
        ChangeMoveDirection();

        if (movingClockwise)
        {
            rb.angularVelocity = movementSpeed;
        }
        if (!movingClockwise)
        {
            rb.angularVelocity = -movementSpeed;
        }
    }
}
