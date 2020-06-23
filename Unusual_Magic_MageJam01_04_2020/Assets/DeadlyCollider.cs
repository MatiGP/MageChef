using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<Health>().TakeDamage(4);
        }
        if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
          
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Climbable")
        {
            Destroy(collision.gameObject);
        }
    }
}
