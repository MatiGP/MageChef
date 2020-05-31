using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPickUp : MonoBehaviour
{
    [SerializeField] int points;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerPoints.instance.AddPoints(points);
            Destroy(gameObject);
        }
    }

}
