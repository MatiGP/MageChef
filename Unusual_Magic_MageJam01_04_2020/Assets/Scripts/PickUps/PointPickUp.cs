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
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(Disable());
        }
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
