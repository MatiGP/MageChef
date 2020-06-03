using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    [SerializeField] float swingForce;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] Transform attachPos;
    [SerializeField] Rigidbody2D chainRB;

    bool isPlayerAttached;
    bool swingBack = true;

    float horizontal;

    PlayerController pc;

    private void Update()
    {
        if (!isPlayerAttached) return;

        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pc.StopSwinging();
            pc = null;
            isPlayerAttached = false;
            StartCoroutine(DisableHandle());
        }
    }

    private void FixedUpdate()
    {
        if (horizontal == 0) return;

        rb2d.AddForce(new Vector2(swingForce * horizontal, swingForce));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StopCoroutine(Reset());
            SetAngularDrag(0.05f);
            collision.transform.parent = transform;
            isPlayerAttached = true;
            pc = collision.GetComponent<PlayerController>();
            collision.transform.position = attachPos.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(DisableHandle());
            StartCoroutine(Reset());
        }
    }


    IEnumerator DisableHandle()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponent<CircleCollider2D>().enabled = true;
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(4f);
        SetAngularDrag(1500f);
    }

    public void SetAngularDrag(float ang)
    {
        chainRB.angularDrag = ang;       
    }

}
