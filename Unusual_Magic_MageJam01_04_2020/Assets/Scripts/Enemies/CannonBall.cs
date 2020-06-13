using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    Vector2 direction;
    float ballXSpeed;
    float ballYSpeed;
    float ballLifeTime;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("DestroyBall", ballLifeTime);
    }

    public void SetUpCannon(Vector2 dir, float XSpeed, float YSpeed, float lifeTime)
    {
        direction    = dir;
        ballXSpeed   = XSpeed;
        ballYSpeed   = YSpeed;
        ballLifeTime = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(direction.x * ballXSpeed, direction.y * ballYSpeed);
    }

    void DestroyBall()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage();
            DestroyBall();
        }
        if(collision.tag == "Colliders")
        {
            DestroyBall();
        }
    }
}
