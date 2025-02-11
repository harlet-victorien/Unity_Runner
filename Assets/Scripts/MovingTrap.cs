using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 3f;

    private bool movingToB = true;

    void Update()
    {
        if (movingToB)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB, speed * Time.deltaTime);
            if (transform.position == pointB)
                movingToB = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA, speed * Time.deltaTime);
            if (transform.position == pointA)
                movingToB = true;
        }
    }
}

