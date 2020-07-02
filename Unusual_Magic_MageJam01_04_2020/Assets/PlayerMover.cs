using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float force;
    Rigidbody2D playerBody;


    private void Update()
    {
        if(playerBody != null)
        {
            playerBody.AddForce(new Vector2(force, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerBody = collision.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerBody = null;
    }

    
}
