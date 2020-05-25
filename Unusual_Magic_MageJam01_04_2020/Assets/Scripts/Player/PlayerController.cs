using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] Transform legsPosition;
    [SerializeField] LayerMask groundMask;
    [SerializeField] int bounceAddForce = 2;
    [SerializeField] int bounceMaxForce = 17;
    [SerializeField] AudioSource jumpSource;
    [SerializeField] GameObject deathMenu;
    

    int bounceForce = 3;
    bool canJump;
    bool canMove = true;
    float horizontal;
    Rigidbody2D rb2d;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            horizontal = Input.GetAxis("Horizontal");
        }
        else
        {
            horizontal = 0;
        }
        

        ChangeScale();
        UpdateAnimator();

        if (canJump) bounceForce = 3;

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
            animator.SetTrigger("jump");
            jumpSource.Play();
        }      
        
    }

    void Jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
    }

    private void FixedUpdate()
    {
        canJump = Physics2D.OverlapCircle(legsPosition.position, 0.4f, groundMask);

        rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);
    }

    void UpdateAnimator()
    {
        bool isRunning = horizontal != 0 ? true : false;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isInAir", !canJump);
    }

    void ChangeScale()
    {

        if (horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        
    }

    public void Bounce()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, bounceForce);

        if (bounceForce < bounceMaxForce) bounceForce += bounceAddForce;             
    }

    public void Bounce(float bounceForce)
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, bounceForce);
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    private void OnDestroy()
    {
       // deathMenu.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Platform")
        {
            transform.parent = collision.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Platform")
        {
            transform.parent = null;
        }
    }
}
