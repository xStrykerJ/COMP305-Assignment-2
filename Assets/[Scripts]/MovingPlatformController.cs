using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [Header("Movement")]
    public PlatformDirection direction;
    [Range(1,10)]
    public float distance;
    [Range(1,10)]
    public float speed;

    private Vector2 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        Vector2 newPosition = new Vector2();

        switch (direction)
        {
            case PlatformDirection.HORIZONTAL:
                newPosition = new Vector2(startingPosition.x + Mathf.PingPong(Time.time * speed, distance), transform.position.y);
                break;
            case PlatformDirection.VERTICAL:
                newPosition = new Vector2(transform.position.x, startingPosition.y + Mathf.PingPong(Time.time * speed, distance));
                break;
        }

        transform.position = newPosition;
    }
}
