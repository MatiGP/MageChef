using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] float swingForce;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            rb2d.AddForce(new Vector2(swingForce, swingForce));
        }      
    }

}
