using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPickUp : MonoBehaviour
{
    [SerializeField] int points;
    [SerializeField] AudioSource source;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerPoints.instance.IncreasePoints(points);
            source.Play();
            Destroy(gameObject);
        }
    }
}
