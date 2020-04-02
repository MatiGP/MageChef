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

    bool canJump;
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
        horizontal = Input.GetAxis("Horizontal");

        ChangeScale();
        UpdateAnimator();

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
            animator.SetTrigger("jump");
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("shoot");
        }

    }

    void Jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
    }

    private void FixedUpdate()
    {
        canJump = Physics2D.OverlapCircle(legsPosition.position, 0.4f, groundMask);
        Debug.Log(canJump);
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
}
