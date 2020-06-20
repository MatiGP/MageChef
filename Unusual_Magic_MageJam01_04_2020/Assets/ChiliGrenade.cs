using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChiliGrenade : MonoBehaviour
{
    [SerializeField] float cookieTravelSpeed;
    [SerializeField] float cookieLifeTime;

    Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("DestroyCookie", cookieLifeTime);
    }

    private void FixedUpdate()
    {
        if (transform.localScale.x < 0)
        {
            rb2d.velocity = new Vector2(-cookieTravelSpeed, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(cookieTravelSpeed, rb2d.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyArmor")
        {
            collision.gameObject.SetActive(false);
            collision.GetComponentInParent<Health>().SetHealth(4);
            DestroyCookie();

        }
        else if (collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().TakeDamage();
            DestroyCookie();
        }
        else if(collision.tag == "Colliders")
        {
            DestroyCookie();
        }       
    }

    void DestroyCookie()
    {
        Destroy(gameObject);
    }
}
