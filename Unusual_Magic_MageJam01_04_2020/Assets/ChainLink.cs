using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLink : MonoBehaviour
{
    [SerializeField] float swingForce;
    [SerializeField] float playerAttachedStartSwingForce = 500;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] Collider2D[] collider2Ds;

    PlayerController playerAttachedToChainLink;
    float horizontal;
    bool isPlayerAttached;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerAttached) return;

        horizontal = Input.GetAxis("Horizontal");

        if(horizontal < 0)
        {
            playerAttachedToChainLink.transform.localScale = new Vector3(-1, 1, 1);

        } else if(horizontal > 0)
        {
            playerAttachedToChainLink.transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {          
            
            playerAttachedToChainLink.StopSwinging();
            playerAttachedToChainLink = null;
            isPlayerAttached = false;           
            StartCoroutine(DisableChainLinkCollider());
        }
    }

    private void FixedUpdate()
    {
        if (horizontal == 0) return;

        rb2d.AddForce(new Vector2(swingForce * horizontal, swingForce));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.collider.tag == "Player")
        {         
            playerAttachedToChainLink = collision.collider.GetComponent<PlayerController>();
            playerAttachedToChainLink.transform.parent = transform;
            playerAttachedToChainLink.SetSwing();
            playerAttachedToChainLink.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
            playerAttachedToChainLink.transform.localPosition = new Vector3(0f, 0f, 0f);
            isPlayerAttached = true;
            rb2d.AddForce(new Vector2(playerAttachedStartSwingForce * playerAttachedToChainLink.transform.localScale.x, 1));
        }
    }

    IEnumerator DisableChainLinkCollider()
    {
        foreach(Collider2D c2d in collider2Ds)
        {
            c2d.enabled = false;
        }

        yield return new WaitForSeconds(0.6f);

        foreach (Collider2D c2d in collider2Ds)
        {
            c2d.enabled = true;
        }
    }


}
