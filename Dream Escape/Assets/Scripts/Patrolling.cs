using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    public Transform[] points;

    private int currentPosition;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        currentPosition = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != points[currentPosition].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[currentPosition].position,
                speed * Time.deltaTime);
        }
        else
        {
            currentPosition = (currentPosition + 1) % points.Length;
        }
    }
}
