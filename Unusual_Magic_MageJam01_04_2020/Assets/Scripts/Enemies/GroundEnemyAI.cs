using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyAI : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] int pointsReward = 100;
    [SerializeField] float wanderSpeed = 3f;
    [Header("Combat Stats")]
    [SerializeField] float tauntRange = 5f;
    [SerializeField] float chaseSpeed = 6f;
    [SerializeField] float attackRange = 1.2f;
    [Header("Settings")]
    [SerializeField] float timeBetweenAttacks = 1f;
    [SerializeField] bool canMove = true;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform legs;
    [SerializeField] Transform attackPoint;
    [SerializeField] AudioSource attackSource;

    Rigidbody2D rb2d;
    Animator animator;

    GameObject target;
    bool isInAttackRange = false;
    bool playerSpotted = false;
    bool canWalkOnPlatform = true;
    bool hasTouchedTheWall = false;
    bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isInAttackRange = CheckInRange();
        canWalkOnPlatform = Physics2D.Raycast(legs.position, Vector2.down, 0.5f, groundLayer);
        hasTouchedTheWall = Physics2D.Raycast(legs.position, Vector2.right * transform.localScale.x, 0.25f, groundLayer);
        playerSpotted = CheckForPlayerInTauntRange();
        if (playerSpotted)
        {
            target = CheckForPlayerInTauntRange().transform.gameObject;
        }


        if (!playerSpotted && canMove)
        {
            Wander();
        }

        if (playerSpotted && !isInAttackRange && canMove)
        {
            Chase();
        }

        if (isInAttackRange && canAttack)
        {
            StartAttack();
        }
    }

    void Wander()
    {     
        animator.SetBool("isRunning", true);
        rb2d.velocity = new Vector2(wanderSpeed * transform.localScale.x, rb2d.velocity.y);

        if (!canWalkOnPlatform || hasTouchedTheWall)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
        }     

    }

    private RaycastHit2D CheckForPlayerInTauntRange()
    {
        return Physics2D.Raycast(attackPoint.position, Vector2.right * transform.localScale.x, tauntRange, playerLayer);
    }

    private bool CheckInRange()
    {
        if (target == null) return false;

        return Vector2.Distance(transform.position, target.transform.position) <= attackRange;
    }

    void Chase()
    {
        animator.SetBool("isRunning", true);

        if (!canWalkOnPlatform)
        {
            target = null;
            playerSpotted = false;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, 1);
            return;
        }

        if (target == null) return;

        if (target.transform.position.x > transform.position.x)
        {
            rb2d.velocity = new Vector2(chaseSpeed, rb2d.velocity.y);
            transform.localScale = new Vector3(1, transform.localScale.y, 1);
        }
        else
        {
            rb2d.velocity = new Vector2(-chaseSpeed, rb2d.velocity.y);
            transform.localScale = new Vector3(-1, transform.localScale.y, 1);
        }

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
        attackSource.Play();
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack = true;
    }

    public int GetPointsReward()
    {
        return pointsReward;
    }

    IEnumerator SlowDown()
    {
        wanderSpeed = 1.5f;
        chaseSpeed = 2.5f;
        yield return new WaitForSeconds(1.5f);
        wanderSpeed = 3f;
        chaseSpeed = 4f;
    }

    private void OnDestroy()
    {
        PlayerPoints.instance.AddPoints(pointsReward);
    }
}
