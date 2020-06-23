using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBallRoll : MonoBehaviour
{
    [SerializeField] SurfaceEffector2D surfaceEffector;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            surfaceEffector.speed = -5.6f;
        }
    }
}
