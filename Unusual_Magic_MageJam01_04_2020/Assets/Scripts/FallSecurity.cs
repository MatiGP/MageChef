using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallSecurity : MonoBehaviour
{
    [SerializeField] Transform securePos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.transform.position = securePos.position;
        }
    }
}
