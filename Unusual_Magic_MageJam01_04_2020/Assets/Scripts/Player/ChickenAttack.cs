using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAttack : MonoBehaviour
{
    [SerializeField] int chickenDamage = 2;
    [SerializeField] float chickenSpeed;
    [SerializeField] float chickenDetonationTime;
    [SerializeField] AudioSource source;

    Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Invoke("Detonate", chickenDetonationTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.rotation.y > 0)
        {
            rb2d.velocity = new Vector2(-chickenSpeed, rb2d.velocity.y);
        }
        else
        {
            rb2d.velocity = new Vector2(chickenSpeed, rb2d.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.collider.tag == "Enemy")
       {
            collision.collider.GetComponent<Health>().TakeDamage();
            Detonate();
       }
    }

    void Detonate()
    {
        StartCoroutine(Detonation());
    }

    IEnumerator Detonation()
    {
        source.Play();
        GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
