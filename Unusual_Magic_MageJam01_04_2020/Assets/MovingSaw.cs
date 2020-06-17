using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSaw : MonoBehaviour
{
    [SerializeField] Transform[] sawPoints;
    [SerializeField] float sawSpeed;
    

    int pointIndex = 1;
    bool returnSaw;

    private void Start()
    {
        transform.position = sawPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position != sawPoints[pointIndex].position)
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
        transform.position = Vector2.MoveTowards(transform.position, sawPoints[pointIndex].position, sawSpeed * Time.deltaTime);
    }

    void GetNewPositionIndex()
    {
        if (!returnSaw)
        {
            pointIndex++;
            if (pointIndex + 1 > sawPoints.Length - 1)
            {
                returnSaw = true;
            }
        }
        else
        {
            pointIndex--;
            if(pointIndex - 1 < 0)
            {
                returnSaw = false;
            }
        }
    }

}
