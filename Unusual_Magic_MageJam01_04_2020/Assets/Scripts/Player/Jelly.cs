using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : MonoBehaviour
{
    [SerializeField] float jellyUpTime = 6f;
    [SerializeField] AudioSource audioSource;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Invoke("DestroyJelly", jellyUpTime);
    }   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            anim.SetTrigger("bounce");
            audioSource.Play();
            collision.collider.GetComponent<PlayerController>().Bounce();

        }
    }

    void DestroyJelly()
    {
        Destroy(gameObject);
    }
}
