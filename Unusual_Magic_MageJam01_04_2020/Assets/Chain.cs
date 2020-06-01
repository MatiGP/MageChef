using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    [SerializeField] float swingForce;
    [SerializeField] Rigidbody2D rb2d;

    bool isPlayerAttached;
    bool swingBack = true;
    PlayerController pc;

    private void Update()
    {
        if (!isPlayerAttached) return;

        if (Input.GetKeyDown(KeyCode.A) && swingBack)
        {
            swingBack = false;
            rb2d.AddForce(new Vector2(-swingForce, swingForce));
        }

        if (Input.GetKeyDown(KeyCode.D) && !swingBack)
        {
            swingBack = true;
            rb2d.AddForce(new Vector2(swingForce, swingForce));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pc.StopSwinging();
            pc = null;
            isPlayerAttached = false;
            StartCoroutine(DisableHandle());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.parent = transform;
            isPlayerAttached = true;
            pc = collision.GetComponent<PlayerController>();
        }
    }

    IEnumerator DisableHandle()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponent<CircleCollider2D>().enabled = true;
    }

}
