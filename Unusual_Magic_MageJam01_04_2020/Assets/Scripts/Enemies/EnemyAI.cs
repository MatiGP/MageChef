﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] int pointsReward = 100;

    [Header("AI Settings")]
    
    [SerializeField] float wanderSpeed = 3f;
    [SerializeField] float chaseSpeed = 3f;
    [SerializeField] float tauntRange = 3f;
    [SerializeField] bool stationary;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Transform legs;
    [SerializeField] Attack attack;

    Rigidbody2D rb2d;
    Animator animator;

    public Health target {get {return target;} private set{}}

    bool isInAttackRange;
    bool playerSpotted;
    bool canWalkFurther;
    bool hasTouchedWall;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();        
    }

    // Update is called once per frame
    void Update()
    {
        canWalkFurther = Physics2D.Raycast(legs.position, Vector2.down, 0.5f, groundLayer);
        hasTouchedWall = Physics2D.Raycast(legs.position, Vector2.right * transform.localScale.x, 0.25f, groundLayer);      
        
        target = CheckForPlayerInSight();

        if(target == null && !stationary){
            Wander();
            return;
        }

        isInAttackRange = Vector2.Distance(target.transform.position, transform.position) <= attack.attackRange ? true : false;

        if(!isInAttackRange && !stationary){
            Chase();
        }else{
            StopMovement();
            attack.DoAttack();
        }
    }

    void Wander(){
        
        float scaleX = transform.localScale.x;
        if(hasTouchedWall || !canWalkFurther){
            transform.localScale = new Vector3(scaleX * -1, transform.localScale.y, transform.localScale.z);
        }
        animator.SetBool("isRunning", true);
        rb2d.velocity = new Vector2(wanderSpeed * scaleX, rb2d.velocity.y);
    }

    void Chase(){

        float scaleX = transform.localScale.x;
        if(hasTouchedWall || !canWalkFurther){
            transform.localScale = new Vector3(scaleX * -1, transform.localScale.y, transform.localScale.z);
            target = null;
            return;
        }
        animator.SetBool("isRunning", true);
        rb2d.velocity = new Vector2(chaseSpeed * scaleX, rb2d.velocity.y);
    }

    void StopMovement(){
        rb2d.velocity = Vector2.zero;
        animator.SetBool("isRunning", false);
    }

    Health CheckForPlayerInSight(){
        Transform gO = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, tauntRange, playerLayer).transform;

        if(gO == null){
            return null;
        }else{
            return gO.GetComponent<Health>();
        }
    }

    
}