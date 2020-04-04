using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Health), typeof(Rigidbody2D), typeof(CapsuleCollider2D))]
public class EnemyController : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] int pointsReward = 100;
    [SerializeField] float wanderSpeed = 3f;
    [Header("Combat Stats")]
    [SerializeField] float tauntRange = 5;
    [SerializeField] float chaseSpeed = 6f;
    [SerializeField] float attackRange = 1.2f;
    [Header("Settings")]
    [SerializeField] float timeBetweenAttacks = 0.5f;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform legs;

    Rigidbody2D rb2d;
    Animator animator;

    GameObject target;
    bool isInAttackRange = false;
    bool playerSpotted = false;
    bool canWalkOnPlatform = true;
    bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        isInAttackRange = CheckInRange();
        canWalkOnPlatform = Physics2D.Raycast(legs.position, Vector2.down, 0.5f, groundLayer);
        
        if (!playerSpotted && target == null)
        {
            Wander();
        }

        if (playerSpotted && !isInAttackRange)
        {
            if (target == null) target = CheckForPlayerInTauntRange().transform.gameObject;

            Chase();
        }

        if (isInAttackRange && canAttack)
        {
            StartAttack();
        }
    }
    private RaycastHit2D CheckForPlayerInTauntRange()
    {
        return Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, tauntRange, playerLayer);
    }

    private bool CheckInRange()
    {
        if (target == null) return false;

        return Vector2.Distance(transform.position, target.transform.position) <= attackRange;
    }

    

    void Wander()
    {
        playerSpotted = CheckForPlayerInTauntRange();
        animator.SetBool("isRunning", true);
        rb2d.velocity = new Vector2(wanderSpeed * transform.localScale.x, rb2d.velocity.y);
      
        if (!canWalkOnPlatform)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }
        
        
    }

    void Chase()
    {
        animator.SetBool("isRunning", true);
        if (!canWalkOnPlatform)
        {
            target = null;
            playerSpotted = false;
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
            return;
        }

        rb2d.velocity = new Vector2(chaseSpeed * transform.localScale.x, rb2d.velocity.y);
      
    }

    void StartAttack()
    {      
        canAttack = false;
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        animator.SetBool("isRunning", false);     
        rb2d.velocity = Vector2.zero;
        animator.SetTrigger("attack");       
        target.GetComponent<Health>().TakeDamage();
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack = true;
    }

    public int GetPointsReward()
    {
        return pointsReward;
    }

    
    
}
