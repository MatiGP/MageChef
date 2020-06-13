using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckBossLaserProjectile : MonoBehaviour
{
    Transform destination;
    [SerializeField] float laserBeamSpeed;
    [SerializeField] float lifeTime;
    // Update is called once per frame
    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        if (destination == null) return;

        transform.position = Vector2.MoveTowards(transform.position, destination.position, laserBeamSpeed * Time.deltaTime);
    }

    public void SetTarget(Health target)
    {
        destination = target.transform;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage();
            DestroyProjectile();
        }
        else
        {
            DestroyProjectile();
        }
    
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
