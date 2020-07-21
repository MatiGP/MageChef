using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    [SerializeField] Transform[] sawPoints;
    [SerializeField] float sawTravelSpeed;
    [SerializeField] bool reverse = false;
    [SerializeField] bool stationary = false;

    int pointIndex = 0;

    bool returnSaw;

    private void Start()
    {
        if (stationary) return;

        if (!reverse)
        {
            transform.position = sawPoints[0].position;
        }
        else
        {
            transform.position = sawPoints[sawPoints.Length-1].position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stationary) return;

        if (transform.position != sawPoints[pointIndex].position)
        {
            Move();
        }
        else
        {
            GetNewPositionIndex();
        }
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, sawPoints[pointIndex].position, sawTravelSpeed * Time.deltaTime);
    }

    void GetNewPositionIndex()
    {
        pointIndex++;
        if (pointIndex == sawPoints.Length)
        {
            pointIndex = 0;
        }
    }

}
